using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampBullet : MonoBehaviour
{


    public float vel = 40;

    [SerializeField] private float TimeLife;
    private float currentLifeTime;
    public GameObject Owner;
    public GameObject campForce;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentLifeTime += Time.deltaTime;

        if (currentLifeTime >= TimeLife)
        {
            Instantiate(campForce,this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }

        transform.position += transform.forward * vel * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        KartController kart = other.GetComponent<KartController>();
       
        if (kart != null)
        {
            Instantiate(campForce, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
            return;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            Instantiate(campForce, this.transform.position, this.transform.rotation);

            Destroy(this.gameObject);
            return;
        }

    }
    
}
