using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CCC.Manager;

public class Objectives : MonoBehaviour {

    public int m_Cash, m_CashTarget;
    public AudioClip sfx_cashIn, sfx_cashOut, sfx_win, sfx_fail;

    public float minutes = 12;
    public float seconds = 30;
    public int OrderBasePrice = 10;
    public int PricePerPizza = 12; 

    private float remainingSeconds;

    void Start()
    {
        enabled = false;

        Game.OnGameReady += () =>
        {
            AfficheCash();
            enabled = true;
            Game.GameUI.objectiveDisplay.SetObjectiveAmount(m_CashTarget);
            Game.Map.cash = this;
            DelayedNotifications();
        };
    }

    void Update()
    {
        float dt = Time.deltaTime;

        seconds -= dt;

        if(seconds <= 0)
        {
            minutes--;
            if(minutes <= -1)
            {
                SoundManager.PlayStaticSFX(sfx_fail, 0, 0.1f);
                //SoundManager.StopMusic();
                Game.instance.Lose();
            }
            else
            {
                seconds += 60;
            }
        }

        minutes = minutes.Raised(0);
        seconds = seconds.Raised(0);

        AfficheTimer();
    }


    //  Depense d'argent du joueur
    //  dep : montant depenser
    public void OutcomeCash(int dep)
    {
        m_Cash -= dep;
        SoundManager.PlayStaticSFX(sfx_cashOut, 0, 0.3f);
        AfficheCash();
        Game.GameUI.FeedbackDisplay.PlayFeedbackAnimation(-dep);
    }

    //  Revenus d'argent du joueur
    //  Rev : montant de revenus
    public void IncomeCash(int rev)
    {
        m_Cash += rev;
        SoundManager.PlayStaticSFX(sfx_cashIn, 0, 0.3f);
        AfficheCash();

        if (m_Cash >= m_CashTarget)
        {
            SoundManager.PlayStaticSFX(sfx_win, 3);
            SoundManager.StopMusic();
            Game.instance.Win();

            //Call fin de mission, objectif atteint
        }
        Game.GameUI.FeedbackDisplay.PlayFeedbackAnimation(rev);
    }

    //  Methode qui va set l'affiche de l'UI du montant d'argent
    public void AfficheCash()
    {
        Game.GameUI.objectiveDisplay.SetCashAmount(m_Cash);
    }


    public void AfficheTimer()
    {
        Game.GameUI.objectiveDisplay.SetObjectiveDuration(minutes, seconds);
    }

    private float ConvertMinutesToSeconds(float minutes, float seconds)
    {
        return (minutes * 60) + seconds;
    }

    private void DelayedNotifications()
    {
        remainingSeconds = ConvertMinutesToSeconds(minutes, seconds);

        float timeToLastMinute = remainingSeconds - 60;
        Game.DelayedEvents.AddDelayedAction(delegate
        {
            NotificationQueue.PushNotification("Il reste une minute.");
        }, timeToLastMinute);


        float timeToLastFiveMinutes = remainingSeconds - 300;
        if (timeToLastFiveMinutes > 0)
        {
            Game.DelayedEvents.AddDelayedAction(delegate
            {
                NotificationQueue.PushNotification("Il reste 5 minutes.");
            }, timeToLastFiveMinutes);
        }
        

        float timetoLastTenMinutes = remainingSeconds - 600;
        if (timetoLastTenMinutes > 0)
        {
            Game.DelayedEvents.AddDelayedAction(delegate
            {
                NotificationQueue.PushNotification("Il reste 10 minutes.");
            }, timetoLastTenMinutes);
        }
    }
}
