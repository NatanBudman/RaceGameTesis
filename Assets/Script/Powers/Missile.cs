using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

   public float vel = 40;

    [SerializeField] private float TimeLife;
    private float currentLifeTime;
    public GameObject Owner;
    public float slowDrag = 5f;
    public float slowTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentLifeTime += Time.deltaTime;

        if (currentLifeTime >= TimeLife)
        {
            Destroy(this.gameObject);
        }

        transform.position += transform.forward * vel * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        KartController kart = other.GetComponent<KartController>();

        if (kart != null)
        {
            other.GetComponent<KartPowerPickUp>().Slowed(true, 1.5f, 0);
            Destroy(this.gameObject);
            return;

        }
    }


   
}
