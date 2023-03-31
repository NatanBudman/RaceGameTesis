using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAModel : MonoBehaviour
{
    [HideInInspector] public float RayDist;
    public bool isCollisionObstacle(Transform Start,float angle)
    {
        float angulo = angle;
        Vector3 direction = Quaternion.AngleAxis(angulo, Vector3.up) * Start.forward;
        RaycastHit hit;
        
            if (Physics.Raycast(Start.position,direction,out hit,RayDist,LayerMask.GetMask("Obstacle")))
            {
                return true;
            }
            
                
            return false;
            

    }
    
      public GameObject GetObstacle(Transform StartPos,float maxDist,float angle)
        {
            GameObject obstacle = null;
            
            float angulo = angle;
            Vector3 direction = Quaternion.AngleAxis(angulo, Vector3.up) * transform.forward;
            
            RaycastHit hit;
    
            if (Physics.Raycast(StartPos.position,direction,out hit,maxDist,LayerMask.GetMask("Obstacle")))
            {
                obstacle = hit.collider.gameObject;
                return obstacle;
            }
    
            return obstacle;
        }
     
}
