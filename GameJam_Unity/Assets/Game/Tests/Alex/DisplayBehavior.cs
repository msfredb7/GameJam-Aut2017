using CCC.Manager;
using CCC.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBehavior : MonoBehaviour {

    public const string SCENE_NAME = "CharacterOptions";

    // Affichage
    public GameObject actionPrefab;
    public Transform countainer;
    public GameObject emptyIndicatior;
    public CanvasGroup canvasGroup;

    // Character Behavior
    [HideInInspector]
    public CharacterBehavior behavior;

    private List<SingleActionButton> actionButtons = new List<SingleActionButton>();

    public void Init (CharacterBehavior behavior)
    {
        if(behavior== null)
            Debug.LogError("CharacterBehavior NULL Error");
        this.behavior = behavior;
        behavior.onAddAction += Add;
        DisplayAll();
    }

    void Add(CharacterAction newAction)
    {
        if (emptyIndicatior.activeSelf)
            emptyIndicatior.SetActive(false);

        SingleActionButton newButton = Instantiate(actionPrefab, countainer).GetComponent<SingleActionButton>();

        if(newButton != null)
        {
            actionButtons.Add(newButton);
            newButton.Init(newAction.actionType, actionButtons.Count - 1, this);
        }
    }

    public void Delete(int index)
    {
        behavior.characterActions.RemoveAt(index);
        actionButtons.RemoveAt(index);
        for (int i = 0; i < actionButtons.Count; i++)
        {
            actionButtons[i].Init(actionButtons[i].currentType, i, this);
        }
    }

    void DisplayAll()
    {
        for (int i = 0; i < behavior.characterActions.Count; i++)
        {
            Add(behavior.characterActions[i]);
        }
    }

    public void Exit()
    {
        GetComponent<WindowAnimation>().Close(delegate ()
        {
            behavior.onAddAction = null;
            Scenes.UnloadAsync(SCENE_NAME);
        });
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
    }
}
