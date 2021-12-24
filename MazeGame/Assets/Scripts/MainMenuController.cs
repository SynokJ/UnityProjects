using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    #region Loading Menu
    [Header("Loading Menu Attributes")]
    public GameObject loadingScene;
    public Slider slider;
    #endregion

    // load level scene with LoadSceneAsync
    public void onPlayButtonClicked(int sceneIndex)
    {
        StartCoroutine(loadAsynchronously(sceneIndex));
    }

    IEnumerator loadAsynchronously(int sceneIndex)
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScene.SetActive(true);

        while (!oper.isDone)
        {
            float progress = Mathf.Clamp01(oper.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
}
