using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSecretScene : MonoBehaviour
{

    [Header("Loading Menu Attributes")]
    public GameObject loadingScene;
    public Slider slider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        StartCoroutine(loadAsynchronously());
    }

    IEnumerator loadAsynchronously()
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync("SecreteScene");
        loadingScene.SetActive(true);

        while (!oper.isDone)
        {
            float progress = Mathf.Clamp01(oper.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
}
