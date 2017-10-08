using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleActionButton : MonoBehaviour {

    public Text actionName;
    public Button exitButton;
    public int index;
    public CharacterAction.CharacterActionType currentType;
    private DisplayBehavior display;

    void Start()
    {
        exitButton.onClick.AddListener(DeleteAction);
    }

    public void Init(CharacterAction.CharacterActionType actionType, int index, DisplayBehavior display)
    {
        this.index = index;
        this.display = display;
        currentType = actionType;
        switch (actionType)
        {
            case CharacterAction.CharacterActionType.GoNPickup:
                actionName.text = (index + 1) + ". Go and Pickup";
                break;
            case CharacterAction.CharacterActionType.GoNDrop:
                actionName.text = (index + 1) + ". Go and Drop";
                break;
            default:
                break;
        }
    }

    public void DeleteAction()
    {
        display.OnActionDeleted(index);
        Destroy(gameObject);
    }
}
