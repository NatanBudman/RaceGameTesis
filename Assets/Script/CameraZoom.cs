using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
   /* public int zoom = 80;
    public int normal = 60;
    public float smooth = 5;
    [SerializeField] private TurboSystem turboSystem;
    private bool isZoomed = false;

    private void Update()
    {
        if (turboSystem._currentTurboAmount >= 1)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isZoomed = !isZoomed;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            isZoomed = false;

        }
        if (turboSystem._currentTurboAmount <= 0)
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
*/
}
