using UnityEngine;

public class Point : MonoBehaviour
{
   public Points Points;
   public int PointOrder;
    
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         if (Points.indexOrder == PointOrder)
         {
            Points.NextPoint();
         }
      }
   }
   
}
