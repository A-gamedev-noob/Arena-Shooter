using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public enum JoyStickDirection {Horizontal, Vertical,Both}
public class FixedJS : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform background;
    public RectTransform handle;
    public JoyStickDirection joyStickDirection = JoyStickDirection.Both;
    public Camera cam;

    public float Horizontal { get { return input.x; } }
    public float Vertical {get {return input.y;}}

    [Range(0,2f)]
    public float handleLimit = 1f;
    
    Vector2 input = Vector2.zero;

    public void OnPointerDown(PointerEventData eventData){
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData){
        Vector2 JoyDirection = eventData.position - RectTransformUtility.WorldToScreenPoint(new Camera(), background.position);
        input = (JoyDirection.magnitude>background.sizeDelta.x/2f) ? JoyDirection.normalized : JoyDirection /(background.sizeDelta.x/2f);

        if(joyStickDirection == JoyStickDirection.Horizontal)
            input = new Vector2(input.x,0f);
        else if(joyStickDirection == JoyStickDirection.Vertical)
            input = new Vector2(0f, input.y);

        handle.anchoredPosition = (input * background.sizeDelta.x/2f) * handleLimit;
    }

    public void OnPointerUp(PointerEventData eventData){
        input = Vector2.zero;
        handle.anchoredPosition = input;

    }
  
}
