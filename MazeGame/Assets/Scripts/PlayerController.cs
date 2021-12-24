using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
    #region Time
    [Header("Time Variables")]
    float curTime = 0;
    float timeLim = 0;
    #endregion

    #region Player
    [Header("Player Physics")]
    Animator anim;
    Rigidbody2D rb;
    JoystickController jc;
    float moveScale = 5f;
    #endregion

    #region Lights
    [Header("Lights")]
    Light2D torch01;
    Light2D torch02;
    #endregion

    #region Sound Effects
    public GameObject soundWalk;
    public AudioSource soundTorch;
    public AudioSource soundDie;
    #endregion

    #region Score and Level
    [Header("Score and Level Logic")]
    int coinNum = 0;
    int scoreNum = 0;
    public Text scoreText;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public GameObject statueKill;
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

        // coins and score
        coinNum = FindObjectOfType<CoinSpawnMeneger>().getCoinNum();
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        statueKill.GetComponent<VideoPlayer>().Stop();
        #endregion

        #region Set Score Text and Timer
        scoreText.text = scoreNum.ToString() + " / " + coinNum.ToString();
        timeLim = getRandomNumber();
        #endregion
    }

    void FixedUpdate()
    {
        #region Player Move and Player Animation Controller 
        if (jc.vec.y != 0)
        {
            rb.velocity = jc.vec * moveScale;
            soundWalk.GetComponent<AudioSource>().enabled = true;

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
        }
        else
        {
            rb.velocity = Vector2.zero;
            soundWalk.GetComponent<AudioSource>().enabled = false;
        }

        // Return to Player_IdleAnim
        if (!jc.InnerCircle.gameObject.activeSelf)
        {
            anim.SetBool("toRight", false);
            anim.SetBool("toLeft", false);
            return;
        }
        #endregion

        #region Automatic Torch Extinguishing (3-6 minutes)
        if (curTime > timeLim)
        {
            curTime = 0;
            timeLim = getRandomNumber();
            turnTorchOff();
        }
        else if (!lightIsOff())
            curTime += Time.deltaTime;
        #endregion

        #region Player Win
        if (scoreNum == coinNum)
            gameWinPanel.SetActive(true);
        #endregion
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        #region Touch Coin 
        if (collision.tag == "Coin")
        {
            scoreNum++;
            scoreText.text = scoreNum.ToString() + " / " + coinNum.ToString();
            Destroy(collision.gameObject);
        }
        #endregion

        #region Touch Enemies
        if (collision.tag == "Minotaur")
        {
            gameOverPanel.SetActive(true);
            playDieSound();
        }
        else if (collision.tag == "Statue" && collision.gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            if (statueKill != null)
            {
                statueKill.GetComponent<RawImage>().enabled = true;
                statueKill.GetComponent<VideoPlayer>().Play();
                StartCoroutine(showGameOverPanel());

                playDieSound();
            }
        }
        #endregion
    }

    IEnumerator showGameOverPanel()
    {
        yield return new WaitForSeconds(2);
        gameOverPanel.SetActive(true);
        statueKill.GetComponent<RawImage>().enabled = false;
    }


    public void turnTorchOn()
    {

        if (rb.velocity != Vector2.zero || torch01.intensity == 1)
            return;

        anim.SetTrigger("turnOn");

        soundTorch.Play();

        torch01.intensity = 1;
        torch02.intensity = 0.5f;
    }

    public void turnTorchOff()
    {

        if (torch01.intensity == 0)
            return;

        soundTorch.Stop();

        torch01.intensity = 0;
        torch02.intensity = 0;
    }

    public Light2D getTorch()
    {
        return torch01;
    }

    public bool lightIsOff()
    {
        return torch01.intensity == 0;
    }

    int getRandomNumber()
    {
        return Random.Range(20, 60);
    }

    void playDieSound()
    {
        if (!soundDie.isPlaying)
        {
            soundDie.Play();
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }
}
