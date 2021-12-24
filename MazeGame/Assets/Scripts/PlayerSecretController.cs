using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerSecretController : MonoBehaviour
{

    #region Player
    [Header("Player Physics")]
    Animator anim;
    Rigidbody2D rb;
    JoystickController jc;
    float moveScale = 5.0f;
    #endregion

    #region Lights
    [Header("Lights")]
    Light2D torch01;
    Light2D torch02;
    #endregion

    #region Sound Effects
    public AudioSource soundWalk;
    #endregion

    void Start()
    {
        #region Initialization
        // Player Move
        rb = GetComponent<Rigidbody2D>();
        jc = FindObjectOfType<JoystickController>();

        // get anim and torch from PlayerGFX
        anim = transform.GetChild(1).GetComponent<Animator>();
        torch01 = transform.GetChild(2).GetComponent<Light2D>();
        torch02 = transform.GetChild(3).GetComponent<Light2D>();
        #endregion
    }

    void FixedUpdate()
    {
        #region Player Move and Player Animation Controller 
        if (jc.vec.y != 0)
        {
            rb.velocity = jc.vec * moveScale;

            // Play Animation depending on Player direction
            if (jc.vec.x > 0.01)
            {
                anim.SetBool("toRight", true);
                anim.SetBool("toLeft", false);
            }
            else if (jc.vec.x < -0.01)
            {
                anim.SetBool("toRight", false);
                anim.SetBool("toLeft", true);
            }

            if (!soundWalk.isPlaying)
                soundWalk.Play();
        }
        else
        {
            rb.velocity = Vector2.zero;
            soundWalk.Stop();
        }

        // Return to Player_IdleAnim
        if (!jc.InnerCircle.gameObject.activeSelf)
        {
            anim.SetBool("toRight", false);
            anim.SetBool("toLeft", false);
            return;
        }
        #endregion
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SecreteEnemy")
            SceneManager.LoadScene("MainMenu");
    }

    int getRandomNumber()
    {
        return Random.Range(20, 60);
    }
}
