using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartReSpawn : MonoBehaviour
{
    public Transform ReSpawn;

    private void OnTriggerEnter(Collider other)
    {
        KartEntity kart = other.GetComponent<KartEntity>();

        if (kart == null) return;

        kart.gameObject.transform.position = ReSpawn.position;
        kart.gameObject.transform.rotation = Quaternion.LookRotation(ReSpawn.forward);

    }
}


