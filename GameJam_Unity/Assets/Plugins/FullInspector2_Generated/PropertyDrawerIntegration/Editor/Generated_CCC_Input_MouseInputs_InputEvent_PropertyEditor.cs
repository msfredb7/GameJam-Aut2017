using System;
using FullInspector.Internal;

namespace FullInspector.Generated {
    [CustomPropertyEditor(typeof(CCC.Input.MouseInputs.InputEvent))]
    public class Generated_CCC_Input_MouseInputs_InputEvent_PropertyEditor : fiGenericPropertyDrawerPropertyEditor<Generated_CCC_Input_MouseInputs_InputEvent_MonoBehaviourStorage, CCC.Input.MouseInputs.InputEvent> {
        public override bool CanEdit(Type type) {
            return typeof(CCC.Input.MouseInputs.InputEvent).IsAssignableFrom(type);
        }
    }
}
