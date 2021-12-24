using UnityEngine;
using System.Collections;

public class TriggerController : MonoBehaviour
{

    #region Statue 
    [Header("Statue Obj and it Attributes")]
    int statueLim = 0;
    public GameObject statuePref;
    #endregion

    private void Start()
    {
        statueLim = getRandomEnemyNum();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        #region Spawn Statue
        //Spawn Enemy if player get into the spawn area
        if (collision.tag == "Player" && !GameObject.FindGameObjectWithTag("Statue"))
            StartCoroutine("spawnEnemy");
        #endregion
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        #region Delete Statue
        // Delete all enemys
        if (collision.tag == "Player")
            StartCoroutine("deleteEnemys");
        else if (collision.tag == "Statue")
            Destroy(collision);
        #endregion
    }

    IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(3);

        if (GameObject.FindGameObjectsWithTag("Statue").Length < statueLim)
        {
            float pointX = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
            float pointY = Random.Range(-transform.localScale.y / 2, transform.localScale.y / 2);
            Instantiate(statuePref, transform.position + new Vector3(pointX, pointY, 0), Quaternion.identity);
        }
    }

    IEnumerator deleteEnemys()
    {

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Statue");

        foreach (GameObject obj in enemys)
        {
            Animator anim = obj.transform.GetChild(0).GetComponent<Animator>();
            anim.enabled = true;
            anim.SetTrigger("isDead");
            obj.GetComponent<StatueController>().isStatueDead();
        }

        yield return new WaitForSeconds(1);

        foreach (GameObject obj in enemys)
            Destroy(obj);

        statueLim = getRandomEnemyNum();
    }

    int getRandomEnemyNum()
    {
        return Random.Range(3, 6);
    }
}
