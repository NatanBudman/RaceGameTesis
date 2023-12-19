using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class campForce : MonoBehaviour
{
    public string[] tagsToCheck;
    public int flickerAmount = 5;
    public float movementSpeed = 0.5f;
    public float rotationSpeed = 5; 
    public float radius = 10; 
    public float stopRange = 1.5f; 
    public float flickerDuration = 2; 
    public float flickerRestDuration = 3; 
    public float idleFloatDistance = 0.5f; 
    private bool flickering = false, isAlive; 
    public float flickerDurationCounter; 
    private float flickerRestDurationCounter;

    private void Start()
    {
        

        flickerRestDurationCounter = flickerDurationCounter ;
        isAlive = true;
    }

    void Update()
    {
        if (flickerRestDurationCounter > 0) 
        {
           

            flickerRestDurationCounter -= Time.deltaTime;

            Flicker(transform.position); 
               
            
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Flicker(Vector3 destination)
    {
     

            Collider[] objectsInRange = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider col in objectsInRange)
            {
                if (tagsToCheck.Contains(col.tag))
                {
                    var distance = Vector3.Distance(col.transform.position, transform.position);
                    if (distance > stopRange)
                    {
                        
                        col.transform.position = Vector3.MoveTowards(col.transform.position, destination, movementSpeed * Time.deltaTime);
                    }

                    Vector3 dir = transform.position - col.transform.position;
                    dir.y = 0;
                    Quaternion rot = Quaternion.LookRotation(dir);
                    col.transform.rotation = Quaternion.Slerp(col.transform.rotation, rot, rotationSpeed * Time.deltaTime);
                }
            }
       
    }

    private void OnDrawGizmos() //Visualize our area of effect using gizmos
    {
        if (flickering)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
