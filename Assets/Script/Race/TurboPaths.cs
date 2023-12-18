using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurboPaths : MonoBehaviour
{
    public int TurboaAmount;

    private void OnTriggerEnter(Collider other)
    {
        TurboManager turbo = other.GetComponent<TurboManager>();
        if (turbo != null) 
        {
            turbo.GetTurbo(TurboaAmount);
        }
    }
}
