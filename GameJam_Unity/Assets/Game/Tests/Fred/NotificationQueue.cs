using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class NotificationQueue : Singleton<NotificationQueue>
{
    public Vector2 hiddenPosition;
    public Vector2 shownPosition;
    public float comeInDuration = 0.25f;
    public float goOutDuration = 0.25f;
    public float stayDuration = 3;
    public Text text;
    [ReadOnly]
    public bool isAnimating = false;


    private Queue<Notification> queue = new Queue<Notification>();

    public static void PushNotification(Notification notif)
    {
        instance.queue.Enqueue(notif);
        instance.CheckNotif();
    }

    protected override void Awake()
    {
        base.Awake();
        RectTransform rt = GetComponent<RectTransform>();
        rt.anchoredPosition = hiddenPosition;
        print("hide");
    }

    protected void CheckNotif()
    {
        if (queue.Count > 0 && !isAnimating)
            DisplayNotif(queue.Dequeue());
    }
    protected void DisplayNotif(Notification notif)
    {
        text.text = notif.text;
        RectTransform rt = GetComponent<RectTransform>();
        isAnimating = true;
        rt.DOKill();
        rt.DOAnchorPos(shownPosition, comeInDuration).SetEase(Ease.OutSine);
        rt.DOAnchorPos(hiddenPosition, goOutDuration).SetDelay(stayDuration).SetEase(Ease.InSine).OnComplete(
            () =>
            {
                isAnimating = false;
                CheckNotif();
            });
    }
}