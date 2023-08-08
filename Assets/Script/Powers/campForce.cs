using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class campForce : MonoBehaviour
{
    public string[] tagsToCheck; //The tag to check, example; Enemy, Environment, Default, etc.
    public int flickerAmount = 5; //The times it will flicker
    public float movementSpeed = 0.5f; //Affected unit movement speed towards wisp
    public float rotationSpeed = 5; //Affected unit rotation speed towards wisp
    public float radius = 10; //Area of effect, can be visualize using gizmos
    public float stopRange = 1.5f; //How close to the wisp the affected unity will stop
    public float flickerDuration = 2; //Duration of wisp flicker
    public float flickerRestDuration = 3; //Duration between flickers
    public float idleFloatDistance = 0.5f; //Floating; Y distance for the wisp to move up and down
    private bool flickering = false, isAlive; //These bools control our loops
    public float flickerDurationCounter;  //More hidden counter/timer
    private float flickerRestDurationCounter;

    private void Start()
    {
        

        //Making sure our values are set correctly
        flickerRestDurationCounter = flickerDurationCounter ;
        isAlive = true;
    }

    void Update()
    {
        if (flickerRestDurationCounter > 0) //If our flicker amount is bigger than 0 then we are alive
        {
            //Loop that moves the wisp transform up and down

            flickerRestDurationCounter -= Time.deltaTime;

            Flicker(transform.position); //Call our function that moves affected units towards wisp
               
            
        }
        else
        {
            Destroy(gameObject); //If we are not alive then we destroy ourselves
        }
    }

    void Flicker(Vector3 destination)
    {
     

            //Create a sphere around location using our radius
            Collider[] objectsInRange = Physics.OverlapSphere(transform.position, radius);

            //We get all colliders that overlap our sphere cast
            foreach (Collider col in objectsInRange)
            {
                if (tagsToCheck.Contains(col.tag)) //Check our tags before moving and rotating affected units
                {
                    //Save the distance between our wisp and the affected unity in a temporary variable
                    var distance = Vector3.Distance(col.transform.position, transform.position);
                    if (distance > stopRange)
                    {
                        //Movement
                        col.transform.position = Vector3.MoveTowards(col.transform.position, destination, movementSpeed * Time.deltaTime);
                    }

                    //Rotation
                    Vector3 dir = transform.position - col.transform.position;
                    dir.y = 0; // keep the direction strictly horizontal
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
