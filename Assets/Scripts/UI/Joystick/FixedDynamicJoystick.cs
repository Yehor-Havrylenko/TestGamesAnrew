using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Joystick
{
    public class FixedDynamicJoystick : JoystickHandler
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            backGround.transform.position = Input.mousePosition;
        }
    }
}