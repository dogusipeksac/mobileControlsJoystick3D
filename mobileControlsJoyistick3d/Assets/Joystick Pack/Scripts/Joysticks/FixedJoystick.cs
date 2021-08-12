using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick, IPointerUpHandler, IPointerDownHandler
{
    public Vector2 inputVector = Vector2.zero;
    protected RectTransform Background;
    protected bool Pressed;
    protected int PointerId;
    public RectTransform Handle;
    [Range(0f, 2f)]
    public float HandleRange = 1f;

    [HideInInspector]
    public Vector2 AxisNormalized { get { return inputVector.magnitude > 0.25f ? inputVector.normalized : (inputVector.magnitude < 0.01f ? Vector2.zero : inputVector * 4f); } }

    void Start()
    {
        if (Handle == null)
            Handle = transform.GetChild(0).GetComponent<RectTransform>();
        Background = GetComponent<RectTransform>();
        Background.pivot = new Vector2(0.5f, 0.5f);
        Pressed = false;
    }

    void Update()
    {
        if (Pressed)
        {
            Vector2 direction = (PointerId >= 0 && PointerId < Input.touches.Length) ? Input.touches[PointerId].position - new Vector2(Background.position.x, Background.position.y) : new Vector2(Input.mousePosition.x, Input.mousePosition.y) - new Vector2(Background.position.x, Background.position.y);
            inputVector = (direction.magnitude > Background.sizeDelta.x / 2f) ? direction.normalized : direction / (Background.sizeDelta.x / 2f);
            Handle.anchoredPosition = (inputVector * Background.sizeDelta.x / 2f) * HandleRange;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerId = eventData.pointerId;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        inputVector = Vector2.zero;
        Handle.anchoredPosition = Vector2.zero;
    }
}