using CCC.Input;
using CCC.Manager;
using CCC.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBehavior : MonoBehaviour {

    public const string SCENE_NAME = "CharacterOptions";

    //// Affichage
    //public GameObject actionPrefab;
    //public Transform countainer;
    //public GameObject emptyIndicatior;
    //public CanvasGroup canvasGroup;
    //public GameObject selectionNotif;

    //public GameObject pinPrefab;

    //// Character Behavior
    //[HideInInspector]
    //public CharacterBehavior behavior;

    //public CharacterAction.CharacterActionType currentType;

    //private List<SingleActionButton> actionButtons = new List<SingleActionButton>();
    //private List<GameObject> pins = new List<GameObject>();

    //[HideInInspector]
    //public GameObject currentPin;
    //public Color pickupPinColor;
    //public Color dropPinColor;

    public void Init (HeroBehavior behavior)
    {
    //    if(behavior== null)
    //        Debug.LogError("CharacterBehavior NULL Error");
    //    this.behavior = behavior;
    //    behavior.onAddAction += Add;
    //    Game.HeroManager.onActiveHeroChanged += ResetDisplay;
    //    selectionNotif.SetActive(false);
    //    DisplayAll();
    }

    //void Add(CharacterAction newAction)
    //{
    //    if (emptyIndicatior.activeSelf)
    //        emptyIndicatior.SetActive(false);

    //    SingleActionButton newButton = Instantiate(actionPrefab, countainer).GetComponent<SingleActionButton>();

    //    if(pins.Count > 0)
    //    {
    //        switch (behavior.characterActions[behavior.characterActions.Count - 1].actionType)
    //        {
    //            case CharacterAction.CharacterActionType.GoNPickup:
    //                pins[pins.Count - 1].GetComponent<RawImage>().GetComponent<RawImage>().color = pickupPinColor;
    //                break;
    //            case CharacterAction.CharacterActionType.GoNDrop:
    //                pins[pins.Count - 1].GetComponent<RawImage>().GetComponent<RawImage>().color = dropPinColor;
    //                break;
    //            default:
    //                break;
    //        }
    //    }


    //    if (newButton != null)
    //    {
    //        actionButtons.Add(newButton);
    //        newButton.Init(newAction.actionType, actionButtons.Count - 1, this);
    //    }
    //}

    //void RemoveAll()
    //{
    //    foreach (Transform child in countainer)
    //    {
    //        Destroy(child.gameObject);
    //    }
    //    actionButtons.Clear();
    //}

    //public void OnActionDeleted(int index)
    //{
    //    behavior.characterActions.RemoveAt(index);
    //    actionButtons.RemoveAt(index);

    //    DeletePin(index);

    //    for (int i = 0; i < actionButtons.Count; i++)
    //    {
    //        actionButtons[i].Init(actionButtons[i].currentType, i, this);
    //    }
    //}

    //void ResetDisplay(Hero hero)
    //{
    //    behavior.onAddAction = null;
    //    RemoveAll();
    //    behavior = hero.behavior;
    //    behavior.onAddAction += Add;
    //    DisplayAll();
    //    DisplayAllPin();
    //}

    //void DisplayAll()
    //{
    //    for (int i = 0; i < behavior.characterActions.Count; i++)
    //    {
    //        Add(behavior.characterActions[i]);
    //    }
    //}

    //public void Exit()
    //{
    //    GetComponent<WindowAnimation>().Close(delegate ()
    //    {
    //        behavior.onAddAction = null;
    //        Scenes.UnloadAsync(SCENE_NAME);
    //    });
    //    DeleteAllPin();
    //}

    //public void Hide()
    //{
    //    canvasGroup.alpha = 0;
    //    canvasGroup.interactable = false;
    //}

    //public void Show()
    //{
    //    canvasGroup.alpha = 1;
    //    canvasGroup.interactable = true;
    //}

    //public void AskForChoice(Action<Node> onDestinationChoosen)
    //{
    //    WaitForChoice();
    //    MouseInputs inputs = selectionNotif.GetComponent<MouseInputs>();
    //    inputs.Init();
    //    inputs.screenClicked.AddListener(delegate(Vector2 pos) {

    //        Node closestNode = Game.Fastar.GetClosestNode(pos);

    //        currentPin.GetComponent<PinToMouse>().enabled = false;
    //        currentPin.GetComponent<PinToWorld>().enabled = true;
    //        currentPin.GetComponent<PinToWorld>().worldTransform = closestNode.transform;

    //        onDestinationChoosen(closestNode);
    //        ChoiceMade();
    //        inputs.screenClicked.RemoveAllListeners();
    //    });
    //}

    //public void WaitForChoice()
    //{
    //    Hide();
    //    selectionNotif.SetActive(true);

    //    currentPin = Instantiate(pinPrefab, Game.GameUI.transform);
    //    currentPin.GetComponent<PinToMouse>().enabled = true;
    //    pins.Add(currentPin);

    //    selectionNotif.GetComponent<FadeFlash>().Play();
    //}

    //public void ChoiceMade()
    //{
    //    selectionNotif.GetComponent<FadeFlash>().Stop();
    //    selectionNotif.SetActive(false);
    //    Show();
    //}

    //void DeletePin(int index)
    //{
    //    GameObject pinToDelete = pins[index];
    //    pins.RemoveAt(index);
    //    Destroy(pinToDelete);
    //}

    //void DeleteAllPin()
    //{
    //    int amountOfLoop = pins.Count;
    //    for (int i = 0; i < amountOfLoop; i++)
    //    {
    //        DeletePin(0);
    //    }
    //}

    //void DisplayAllPin()
    //{
    //    DeleteAllPin();
    //    for (int i = 0; i < behavior.characterActions.Count; i++)
    //    {
    //        pins.Add(Instantiate(pinPrefab, Game.GameUI.transform));
    //        pins[pins.Count-1].GetComponent<PinToWorld>().enabled = true;
    //        pins[pins.Count - 1].GetComponent<PinToWorld>().worldTransform = behavior.characterActions[i].destination.transform;
    //        switch (behavior.characterActions[i].actionType)
    //        {
    //            case CharacterAction.CharacterActionType.GoNPickup:
    //                pins[pins.Count - 1].GetComponent<RawImage>().color = pickupPinColor;
    //                break;
    //            case CharacterAction.CharacterActionType.GoNDrop:
    //                pins[pins.Count - 1].GetComponent<RawImage>().color = dropPinColor;
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}
}
