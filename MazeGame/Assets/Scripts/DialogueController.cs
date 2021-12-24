using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    Queue<string> sentences;
    public Animator anim;
    public Text message;
    public Dialogue dialog;

    void Start()
    {
        sentences = new Queue<string>();
        sentences.Clear();

        foreach (string sentence in dialog.sentences)
            sentences.Enqueue(sentence);

        //Time.timeScale = 0f;
        anim.SetBool("isRun", true);
    }

    public void StartNewMessage()
    {

        if(sentences.Count == 0)
        {
            anim.SetBool("isRun", false);
            StartCoroutine(destroyPanel());
            //Time.timeScale = 1;
            return;
        }

        StopAllCoroutines();
        StartCoroutine(typeSentence(sentences.Dequeue()));
    }

    IEnumerator destroyPanel()
    {
        yield return new WaitForSeconds(1);
        transform.parent.gameObject.SetActive(false);
    }

    IEnumerator typeSentence(string sentence)
    {
        message.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return null;
        }
    }
}
