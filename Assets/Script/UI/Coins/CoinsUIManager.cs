using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsUIManager : MonoBehaviour
{
    public Transform SpawnCoins;
    public CoinsPool pool;

    public void GetCoins(int Coins) 
    {
        for (int i = 0; i < Coins; i++) 
        {
            float random = Random.Range(0.2f, 0.5f);
            Invoke("SpawnCoin", random);

            if (i == Coins) return;
        }
    }

    private void SpawnCoin() 
    {
        pool.GetCoin().SetActive(true);
        pool.GetCoin().transform.position = SpawnCoins.position;
    }
}
