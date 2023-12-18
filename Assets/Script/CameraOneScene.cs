using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraOneScene : MonoBehaviour
{
    public GameObject camera1;
    public KartController KartController;
    private void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            OnCameraActive();
        }
        else if (KartController.currentSpeed <= -5)
        {
            OnCameraDisable();
        }
    }

   public void OnCameraActive()
   {    

            camera1.gameObject.SetActive(true);
        
   }


    public void OnCameraDisable()
    {

            camera1.gameObject.SetActive(false);
       
    }
}
