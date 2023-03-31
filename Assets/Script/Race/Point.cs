using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Point : MonoBehaviour
{
   public Points Points;
   public Transform InitialPos;
   public int PointOrder;

   private void Start()
   {
      InitialPos = transform;
   }

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
