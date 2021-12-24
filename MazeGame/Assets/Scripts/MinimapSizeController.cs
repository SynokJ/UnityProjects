using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapSizeController : MonoBehaviour
{
    [Header("Minimap Objects")]
    Rect canvasScale;
    Vector3 standScale;
    Vector3 standPos;

    void Start()
    {
        canvasScale = transform.parent.parent.GetComponent<RectTransform>().rect;
        standScale = transform.parent.localScale;
        standPos = transform.parent.position;
    }

    // maximize or minimize map
    public void onMinimapclicked()
    {

        if (GameObject.FindGameObjectWithTag("DIalogueInstruction") != null)
            return;

        // if map is big minimize map and vice versa 
        if(transform.parent.localScale.x > standScale.x)
        {
            transform.parent.localScale = transform.parent.localScale / 4;
            transform.parent.position = standPos;
            Time.timeScale = 1;
            return;
        }

        transform.parent.position = new Vector2(canvasScale.width, canvasScale.height) * 1.5f;
        transform.parent.localScale = transform.parent.localScale * 4;
        Time.timeScale = 0;
    }
}
