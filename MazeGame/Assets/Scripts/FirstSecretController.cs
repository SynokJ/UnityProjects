using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSecretController : MonoBehaviour
{
    private void Start()
    {
        GetComponent<AudioSource>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponent<AudioSource>().enabled = true;
        }
    }

}
