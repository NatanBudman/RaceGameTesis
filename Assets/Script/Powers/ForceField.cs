using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    [SerializeField] private float timeLife = 10f; // Tiempo de vida del escudo en segundos
    private float currentLifeTime;

    [SerializeField] private float rotateVel = 50f; // Velocidad de rotación del escudo

    [SerializeField] private GameObject ownerGameObject;

    float hitTime;

    

    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            currentLifeTime += Time.deltaTime;

            if (currentLifeTime >= timeLife)
            {
                currentLifeTime = 0;
                this.gameObject.SetActive(false);
            }

            transform.Rotate(Vector3.up * rotateVel * Time.deltaTime);

            transform.position = ownerGameObject.transform.position;
        }
        else
        {
            currentLifeTime = 0;
        }

        if (hitTime > 0)
        {
            float myTime = Time.fixedDeltaTime * 1000;
            hitTime -= myTime;
            if (hitTime < 0)
            {
                hitTime = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("PowerBox") && other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Debug.Log("shield entre");
            other.gameObject.SetActive(false);

            currentLifeTime = 0;
            this.gameObject.SetActive(false);
        }
    }
}
