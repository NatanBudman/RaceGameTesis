using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour,IOptimizatedUpdate
{
    public Transform Target;
    public float speed;

    public void Op_UpdateGameplay()
    {
    }
    private void Update()
    {
        if (transform.position != Target.position)
        {
            // Calculamos la dirección hacia el objetivo
            Vector3 direction = Target.position - transform.position;

            // Normalizamos la dirección para que tenga una longitud de 1
            direction.Normalize();

            // Movemos el objeto utilizando Translate
            transform.Translate(direction * speed * Time.deltaTime);
        }
        if(Vector2.Distance(Target.position,transform.position) < 10f)
        {
            Debug.Log("entre");
            this.gameObject.SetActive(false);
        }
    }
    public void Op_UpdateUX()
    {
      
    }
}
