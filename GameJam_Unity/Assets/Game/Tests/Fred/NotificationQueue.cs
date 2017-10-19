using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using CCC.Manager;

public class NotificationQueue : Singleton<NotificationQueue>
{
    public Vector2 hiddenPosition;
    public Vector2 shownPosition;
    public float comeInDuration = 0.25f;
    public float goOutDuration = 0.25f;
    public float stayDuration = 3;
    public Text text;
    public AudioClip sfx_notif;
    [ReadOnly]
    public bool isAnimating = false;


    private Queue<Notification> queue = new Queue<Notification>();

    public static void PushNotification(Notification notif)
    {
        instance.queue.Enqueue(notif);
        instance.CheckNotif();
    }
    public static void PushNotification(string notif)
    {
        PushNotification(new Notification() { text = notif });
    }

    protected override void Awake()
    {
        base.Awake();
        RectTransform rt = GetComponent<RectTransform>();
        rt.anchoredPosition = hiddenPosition;
    }

    protected void CheckNotif()
    {
        if (queue.Count > 0 && !isAnimating)
            DisplayNotif(queue.Dequeue());
    }
    protected void DisplayNotif(Notification notif)
    {
        if (notif.onShow != null)
            notif.onShow();

        text.text = notif.text;
        RectTransform rt = GetComponent<RectTransform>();
        isAnimating = true;
        SoundManager.PlayStaticSFX(sfx_notif);
        rt.DOKill();
        rt.DOAnchorPos(shownPosition, comeInDuration).SetEase(Ease.OutSine);
        rt.DOAnchorPos(hiddenPosition, goOutDuration).SetDelay(stayDuration).SetEase(Ease.InSine).OnComplete(
            () =>
            {
                if (notif.onHide != null)
                    notif.onHide();

                isAnimating = false;
                CheckNotif();
            });
    }
}
