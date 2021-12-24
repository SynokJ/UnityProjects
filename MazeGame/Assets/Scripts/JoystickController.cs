using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour
{
    #region Joystick GFX
    [Header("Joystick's Grafix")]
    public Transform InnerCircle;
    public Transform OuterCircle;
    #endregion

    #region Joystick Parameters
    [Header("Joystick Parameters")]
    public Vector2 vec;
    Vector2 firstTouch;
    #endregion

    #region Radius
    [Header("Radius of Outer Circle")]
    float radius;
    #endregion

    void Start()
    {
        radius = OuterCircle.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    // touched the Screen
    public void PointerDown()
    {
        #region Draw Joystick
        InnerCircle.position = Input.mousePosition;
        OuterCircle.position = Input.mousePosition;
        firstTouch = Input.mousePosition;

        InnerCircle.gameObject.SetActive(true);
        OuterCircle.gameObject.SetActive(true);
        #endregion
    }

    // stop to touch the Screen
    public void PointerUp()
    {
        #region Hide Joystick
        InnerCircle.gameObject.SetActive(false);
        OuterCircle.gameObject.SetActive(false);
        vec = Vector2.zero;
        #endregion
    }

    // drag the finger on Screen 
    public void Drag(BaseEventData bed)
    {
        #region Set the Vector of Joystick
        PointerEventData ped = bed as PointerEventData;
        Vector2 dragPos = ped.position;

        vec = (dragPos - firstTouch).normalized;
        float dist = Vector2.Distance(dragPos, firstTouch);

        if (dist < radius)
            InnerCircle.position = firstTouch + vec * dist;
        else 
            InnerCircle.position = firstTouch + vec * radius;
        #endregion
    }
}
