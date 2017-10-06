using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullInspector;
using UnityEngine.Events;

namespace CCC.Input
{
    public class MouseInputs : BaseBehavior
    {
        [InspectorHeader("MOUSE INPUTS COMPONENT")]
        public bool detectsClickOnStart = true;
        private bool detectClick = false;

        [System.Serializable]
        public class InputEvent : UnityEvent<Vector2> { }

        [InspectorHeader("Add Functions Called Left Clicking")]
        public InputEvent screenClicked = new InputEvent();
        public UnityEvent clickInput = new UnityEvent();

        [InspectorHeader("Add Functions Called Right Clicking")]
        public UnityEvent rightClickInput = new UnityEvent();
        public InputEvent screenRightClicked = new InputEvent();

        [InspectorHeader("Link Script with IClickInputs")]
        public MonoBehaviour externalScriptMouse;

        void Start()
        {
            if (detectsClickOnStart)
                Init();
        }

        public void Init()
        {
            detectClick = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (detectClick)
            {
                // Left Click
                if (UnityEngine.Input.GetMouseButtonDown(0))
                {
                    Vector2 clickDeltaPosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

                    clickInput.Invoke();
                    screenClicked.Invoke(clickDeltaPosition);

                    if (externalScriptMouse == null)
                        return;

                    // Send Position to a script and trigger the event
                    if (externalScriptMouse.GetComponent<IClickInputs>() != null)
                        externalScriptMouse.GetComponent<IClickInputs>().OnClick(clickDeltaPosition);
                    else if (gameObject.GetComponent<IClickInputs>() != null)
                        GetComponent<IClickInputs>().OnClick(clickDeltaPosition);
                }

                if (UnityEngine.Input.GetMouseButtonDown(1))
                {
                    Vector2 clickDeltaPosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

                    rightClickInput.Invoke();
                    screenRightClicked.Invoke(clickDeltaPosition);

                    if (externalScriptMouse == null)
                        return;

                    // Send Position to a script and trigger the event
                    if (externalScriptMouse.GetComponent<IClickInputs>() != null)
                        externalScriptMouse.GetComponent<IClickInputs>().OnClick(clickDeltaPosition);
                    else if (gameObject.GetComponent<IClickInputs>() != null)
                        GetComponent<IClickInputs>().OnClick(clickDeltaPosition);
                }
            }
        }
    }
}
