using UnityEngine;
using System.Collections.Generic;

public class CoinSpawnMeneger : MonoBehaviour
{

    // have a bug with spawning
    #region Coin
    [Header("Coin Attributes")]
    public GameObject coinPref;
    #endregion

    #region Spawn Coins
    [Header("Spawn Parameters")]
    int coinNum;
    Transform[] coinPos;
    #endregion

    void Awake()
    {
        #region Initialization
        coinNum = Random.Range(6, GameObject.FindGameObjectsWithTag("CoinSpawn").Length);
        coinPos = GetComponentsInChildren<Transform>();
        #endregion

        #region Check For Errors
        if (coinPos == null)
            return;
        #endregion

        #region Spawn Random Number of Coins        
        List<Vector3> usedPos = new List<Vector3>();
      
        for (int i = 0; i < coinNum; i++)
        {
            Vector3 pos;

            do
            {
                pos = coinPos[Random.Range(1, coinPos.Length - 1)].position;
            } while (usedPos.Contains(pos));

            Instantiate(coinPref, pos, Quaternion.identity);
            usedPos.Add(pos);
        }
        #endregion
    }

    public int getCoinNum()
    {
        return coinNum;
    }
}
