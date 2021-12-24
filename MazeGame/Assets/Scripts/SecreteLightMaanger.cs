using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SecreteLightMaanger : MonoBehaviour
{

    Light2D[] lightBox;
    public AudioSource soundTurnOff;

    void Start()
    {
        lightBox = gameObject.GetComponentsInChildren<Light2D>();
        StartCoroutine(turnLightsOff());
    }

    IEnumerator turnLightsOff()
    {

        for (int i = 0; i < lightBox.Length; i++)
        {
            yield return new WaitForSeconds(1.25f);
            soundTurnOff.Play();
            lightBox[i].enabled = false;
        }
    }
}
