using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePoint : MonoBehaviour
{
    public int Point;
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<KartEntity>() != null) 
        {
            KartEntity kart = other.GetComponent<KartEntity>();

           
            if (kart.isNextCurrentPoint(Point)) 
            {
                Debug.Log("entre");
                kart.SetPoint(Point + 1);
            }
        }
    }
}
