using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public int zoom = 80;
    public int normal = 60;
    public float smooth = 5;
    [SerializeField] private TurboManager turboSystem;
    [SerializeField]  KartController kart;
    public GameObject viento;
    private bool isZoomed = false;

    private void Update()
    {
       if (kart.realSpeed >= 70)
            {
                isZoomed = true;
            }
        

        if (kart.realSpeed <= 70)
        {

            isZoomed = false;

        }
      
        if (isZoomed)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
            viento.SetActive(true);
        }

        if (!isZoomed)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
            viento.SetActive(false);
        }
    }

}
