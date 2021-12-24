using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class StatueController : MonoBehaviour
{
    #region Statue Parameters
    [Header("Statue Physics and Logic")]
    float stopDistance = 3;
    float speed = 5.0f;
    bool isDead = false;
    bool speedUp = false;
    #endregion

    #region Statue Attributes
    [Header("Physics Components and Animation Attribute")]
    Transform player;
    Rigidbody2D rb;
    Animator anim;
    #endregion

    void Start()
    {
        #region Initialization
        anim = transform.GetChild(0).GetComponent<Animator>(); // get anim from StatueGFX
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        #endregion

        #region Set Walk Parameters
        // stop distance => half of the light radius
        float tempRadius = player.transform.GetChild(2).GetComponent<Light2D>().pointLightOuterRadius;
        stopDistance = tempRadius / 2;
        #endregion
    }

    void FixedUpdate()
    {

        #region Check For Errors
        // if no player found or dead
        if (player == null || isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        #endregion

        #region Rotate Statue GFX
        // rotate statue by its moving direction 
        if (transform.position.x < player.position.x && transform.rotation.y != 180)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (transform.position.x > player.position.x && transform.rotation.y != 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        #endregion

        #region Statue Logic and Animation
        // that statue is far from player allows statue to move 
        if (Vector2.Distance(player.position, transform.position) > stopDistance)
        {
            anim.enabled = true;
            rb.velocity = (player.position - transform.position).normalized * speed;

            if (speedUp)
                speed += 0.25f;

            speedUp = false;
        }
        else
        {
            // if light is off statue stops to move and no anim 
            if (!player.GetComponent<PlayerController>().lightIsOff())
            {
                rb.velocity = Vector2.zero;
                speedUp = true;
                anim.enabled = false;
            }
            else
            {
                anim.enabled = true;
                rb.velocity = (player.position - transform.position).normalized * speed;
            }
        }
        #endregion
    }

    // check whether statue is dead
    public void isStatueDead()
    {
        isDead = true;
    }
}
