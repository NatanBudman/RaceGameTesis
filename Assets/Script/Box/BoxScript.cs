using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour,IOptimizatedUpdate
{
    public float Rotatespeed;


    public void Op_UpdateGameplay()
    {
        transform.Rotate(0,transform.rotation.y + Rotatespeed * Time.fixedTime,0);
        Debug.Log("entre");
    }

    public void Op_UpdateUX()
    {
    }
}
