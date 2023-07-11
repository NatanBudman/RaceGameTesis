using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accelerate : MonoBehaviour
{
    public float speedDrag = 5f; 
    public GameObject Owner;
    public float speedTime;

   
    private void OnTriggerEnter(Collider other)
    {
        KartEntity kart = other.GetComponent<KartEntity>();

        if (kart != null)
        {
            StartCoroutine(Slower(kart));
        }
    }


    IEnumerator Slower(KartEntity car)
    {
        car.SetRealSpeed(speedDrag);
        car.CatchKart(true);
        yield return new WaitForSeconds(speedTime);

        car.SetRealSpeed(car.GetSpeed);
        car.CatchKart(false);
        StopCoroutine(Slower(car));
    }
}
