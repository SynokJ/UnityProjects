using UnityEngine;
using Pathfinding;

public class MinotaurController : MonoBehaviour
{
    #region Minotaur Walk
    [Header("Minotaur's Move")]
    Vector2 pos;
    public Animator anim;
    AIDestinationSetter destSetter;
    #endregion

    #region Minotaur Target
    [Header("Minotaur's Destination")]
    PlayerController player;
    GameObject[] destPoitns;
    #endregion

    void Start()
    {
        #region Initialization
        destSetter = GetComponent<AIDestinationSetter>();
        destPoitns = GameObject.FindGameObjectsWithTag("DestPoint");
        player = FindObjectOfType<PlayerController>();
        #endregion
    }

    void FixedUpdate()
    {
        #region Rotate MinotaurGFX
        if (pos.x > transform.position.x)
        {
            anim.SetBool("toRight", false);
            anim.SetBool("toLeft", true);
        }
        else if (pos.x < transform.position.x)
        {
            anim.SetBool("toRight", true);
            anim.SetBool("toLeft", false);
        }

        pos = transform.position;
        #endregion

        #region Choose The Target
        if (Vector2.Distance(player.gameObject.GetComponent<Transform>().position, transform.position) < 20 && !player.lightIsOff())
            destSetter.target = player.gameObject.GetComponent<Transform>();
        else if (destSetter.target == player.gameObject.GetComponent<Transform>() && player.lightIsOff())
            destSetter.target = destPoitns[Random.Range(0, destPoitns.Length)].transform;
        else if (destSetter.target == player.gameObject.GetComponent<Transform>() && Vector2.Distance(player.gameObject.GetComponent<Transform>().position, transform.position) > 20)
            destSetter.target = destPoitns[Random.Range(0, destPoitns.Length)].transform;
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        #region Change Destpoint After Getting It
        if (collision.tag == "DestPoint")
        {
            Transform temp;
            do
            {
                temp = destPoitns[Random.Range(0, destPoitns.Length)].transform;
            } while (temp.position == collision.transform.position);

            destSetter.target = temp;
        }
        #endregion
    }
}
