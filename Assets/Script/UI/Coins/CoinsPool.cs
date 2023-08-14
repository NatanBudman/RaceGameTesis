using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsPool : MonoBehaviour
{
    private List<GameObject> CoinsSprite = new List<GameObject>();
    public GameObject CoinPrefab;
    public Transform Target;
    public Transform CoinSpawn;

    public int CoinsInstance;

    // Start is called before the first frame update
    void Start()
    {
        CoinsSprite = new List<GameObject>(CoinsInstance);
        int coinsLentgh = CoinsSprite.Count;
        for (int i = 0; i < coinsLentgh; i++) 
        {
            GameObject Instancia = Instantiate(CoinPrefab, CoinSpawn.position, Quaternion.identity, CoinSpawn);
            Instancia.gameObject.SetActive(false);
            Instancia.GetComponent<Coin>().Target = Target;
            CoinsSprite.Add(Instancia);

        }
    }

    public GameObject GetCoin() 
    {
        foreach (GameObject Coin in CoinsSprite) 
        {
            if (Coin.activeInHierarchy == false) 
            {
                Coin.gameObject.SetActive(true);
                return Coin;
            }
        }

        GameObject Instancia = Instantiate(CoinPrefab, CoinSpawn.position, Quaternion.identity, CoinSpawn);
        Instancia.GetComponent<Coin>().Target = Target;
        CoinsSprite.Add(Instancia);
        return Instancia;
    }
    public static void ReturnPool(GameObject coin) 
    {
        coin.gameObject.SetActive(true);
    }
}
