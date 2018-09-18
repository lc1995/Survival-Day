using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ClickableJoyStick : Joystick {


	Vector2 joystickPosition = Vector2.zero;
    private Camera cam = new Camera();
	private float holdTime;

    void Start()
    {
        joystickPosition = RectTransformUtility.WorldToScreenPoint(cam, background.position);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - joystickPosition;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        ClampJoystick();
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
		if(!CheckInCircle(eventData.position))
			return;

        holdTime = Time.time;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
		// Check if hold time is enough small
		if(Time.time - holdTime < 0.2f)
            UIManager.instance.AddInfoInBoard(Time.time.ToString() + " : Click!");

		// Reset
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

	private bool CheckInCircle(Vector2 touchPos){
		if((touchPos - (Vector2)handle.position).magnitude > handle.sizeDelta.x / 2f)
			return false;
		else
			return true;
	}
}
