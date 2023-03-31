using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IAChecks : MonoBehaviour
{
    public bool Chocando = false;
    public bool RayCastChocando = false;
    public bool _NegativeDir = false;
    public Material[] mats;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer.Equals(6))
        {
            Chocando = true;
            this.gameObject.GetComponent<Renderer>().material = mats[0];
        }
    }

    

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(wait());
        this.gameObject.GetComponent<Renderer>().material = mats[1];

    }

    IEnumerator wait()
    {
        
        yield return new WaitForSeconds(0.2f);
        Chocando = false;
        StopCoroutine(wait());
    }

}
