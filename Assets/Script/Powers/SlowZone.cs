using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public float slowDrag = 5f; // Valor de drag para ralentizar los objetos
    public GameObject Owner;
    public float slowTime;
    public float destroyDuration;
    private float currentDestroyDuration;
    private void Start()
    {
        currentDestroyDuration = 0f;

    }
    private  void Update()
    {
        currentDestroyDuration += Time.deltaTime;
        if (currentDestroyDuration >= destroyDuration)
        {
            Destroy(gameObject);
        }
    }
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
        car.SetRealSpeed(slowDrag);
        car.CatchKart(true);
        yield return new WaitForSeconds(slowTime);

        car.SetRealSpeed(car.GetSpeed);
        car.CatchKart(false);
        StopCoroutine(Slower(car));
    }
}
