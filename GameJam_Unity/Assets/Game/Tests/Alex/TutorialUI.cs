using CCC.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using Tutorial;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public Spotlight spotlight;
    public TutorialIndications tutorialMessage;
    public GameObject inputBlocker;

    public RectTransform heroPanel;
    public RectTransform heroPanelSide;
    public RectTransform statsPanelLeft;
    public RectTransform statsPanelRight;
    public RectTransform statsPanelMid;
    public RectTransform hireHeroesButton;

    public bool activateTutorial = false;

    public float textReadTime = 5;

    void Start()
    {
        inputBlocker.SetActive(false);
        spotlight.gameObject.SetActive(false);
        tutorialMessage.gameObject.SetActive(false);
        Game.OnGameStart += DoTutorial;
    }

    void ShowUI(Vector2 position, string title, string description, Action onComplete)
    {
        tutorialMessage.Hide(delegate ()
        {
            spotlight.On(position, delegate ()
            {
                tutorialMessage.Show(delegate ()
                {
                    DelayManager.LocalCallTo(onComplete, textReadTime, this, true);
                }, title, description);
            });
        });
    }

    void ShowMessage(string description, string title, float time, Action onComplete)
    {
        tutorialMessage.Hide(delegate ()
        {
            spotlight.On(tutorialMessage.transform.position, delegate ()
            {
                tutorialMessage.Show(delegate ()
                {
                    DelayManager.LocalCallTo(onComplete, time, this, true);
                }, description, title);
            });

        });
    }

    void DoTutorial()
    {

        if (activateTutorial)
        {
            DelayManager.LocalCallTo(delegate ()
            {
                inputBlocker.SetActive(true);
                spotlight.gameObject.SetActive(true);
                tutorialMessage.gameObject.SetActive(true);
                Time.timeScale = 0;
                inputBlocker.SetActive(true);
                tutorialMessage.Show(delegate ()
                {
                    DelayManager.LocalCallTo(delegate () {
                        tutorialMessage.Hide(delegate ()
                        {
                            ShowUI(heroPanel.position, "SECTION HÉRO", "Ceci est le héro que vous avez sélectionné en se moment. Cliquer sur Next pour " +
                        " sélectionner le prochain que vous possédez ou sélectionnez le directement dans le map ou dans la liste de vos héros. ", delegate ()
                        {
                            ShowUI(heroPanelSide.position, "CONTRÔLE DU HÉRO", " Ici, vous pouvez intéragir avec le héro sélectionner. Vous pouvez zoom " +
                        " sur lui et le suivre automatiquement avec la caméro ou même ouvrir son panneau de contrôle et lui donner des actions à faire.", delegate ()
                        {
                            ShowUI(statsPanelLeft.position, "VOTRE ARGENT", "Voici votre montant d'argent que vous possédez. Utilisez la pour" +
                            " acheter de nouveaux héros ou conservez la pour approcher plus rapidement de votre objectif", delegate ()
                            {
                                ShowUI(statsPanelMid.position, "TEMP RESTANT", "Vous avez un certain pour complété votre objectif monétaire." +
                                " Garder un oeil sur ce compteur pour réussir le niveau!", delegate ()
                                {
                                    ShowUI(statsPanelRight.position, "VOTRE OBJECTIF", "Votre objectif courrant est indiqué ici. Il s'agit habituellement d'un " +
                                    " montant d'argent à atteindre. Optimisé votre efficacité pour l'atteindre rapidement!", delegate ()
                                    {
                                        ShowUI(hireHeroesButton.position, "RECRUTEMENT DE HÉROS", "Pour être plus efficace dans vos livraisons, vous pouvez " +
                                        " recruter des héros et les placer dans la carte pour les rendre immédiatements actifs.", delegate ()
                                        {
                                            ShowMessage("DÉPLACEMENT","Utilisé la souris ou WASD pour vous déplacez dans la carte. Appuyer sur la roulette pour drag la carte. "+
                                                " Vous pouvez aussi Zoom-in Zoom-out avec la roulette. Utiliser le clique droit lorsque vous avez un héro de sélectionné pour " +
                                                "automatiquement lui ajouter une action déplacement vers un endroit.", 10, delegate () {
                                                spotlight.Off();
                                                tutorialMessage.Hide(null);
                                                Time.timeScale = 1;
                                                inputBlocker.SetActive(false);
                                            });
                                        });
                                    });
                                });
                            });
                        });
                        });
                        });
                    }, 10, this, true);
                }, "PIZZ-HERO", "Vous êtes le gestionnaire des livreurs de pizza de la pizzeria Pizz-Hero. Tous vos livreurs sont des super-héros " +
                "mais ils ont besoin de vous pour leur dire quoi faire. Donner leur des ordres de tel sorte d'être la meilleur pizzeria " +
                "de toute la ville.");
            }, 1, this);
        }
    }
}
