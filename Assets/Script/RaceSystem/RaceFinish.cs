using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceFinish : MonoBehaviour
{
    public int pointsInRace;

    public int MaxTurning;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<KartEntity>() != null)
        {
            KartEntity kart = other.GetComponent<KartEntity>();


            if (kart.isNextCurrentPoint(pointsInRace))
            {
                if (kart.GetCurrentTurning() == MaxTurning)
                {

                }
                else 
                {
                    kart.SetTurning(kart.GetCurrentTurning() + 1);
                    kart.SetPoint(0);
                }
              
            }
        }
    }
}
