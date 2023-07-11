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

    private bool isZoomed = false;

    private void Update()
    {
       if (kart.realSpeed >= 80)
            {
                isZoomed = !isZoomed;
            }
        

        if (kart.realSpeed <= 80)
        {

            isZoomed = false;

        }
      
        if (isZoomed)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
        }

        if (!isZoomed)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
        }
    }

}
