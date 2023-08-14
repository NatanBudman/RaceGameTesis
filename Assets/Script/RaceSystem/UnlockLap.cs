using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLap : MonoBehaviour
{
    public bool unlock = false;
    Timer timer;
    void Start()
    {
        
    }

  

    public void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {

            unlock = true;
        }
    }
}
