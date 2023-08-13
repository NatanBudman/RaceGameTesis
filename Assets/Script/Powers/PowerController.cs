using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIController
{
    void DecideAction(GameObject kart, RuletaPoderes powerRoulette);
}


public class PowerController : MonoBehaviour, IAIController
{
        public float range;
        public float angle = 120;
        public float rangeBack;
        public float angleBack = 120;
        public LayerMask mask;
        public Transform target;
   
    public void DecideAction(GameObject kart, RuletaPoderes powerRoulette)
        {
            KartPowerPickUp powerPickUp = kart.GetComponent<KartPowerPickUp>();

            // Simulamos una acción aleatoria solo si el kart de la IA no tiene un poder
            if (!powerPickUp.HasPower)
            {
                powerPickUp.SelectedPower = powerRoulette.GirarRuleta();
                powerPickUp.HasPower = true;
                Debug.Log(powerPickUp.SelectedPower);
               
                  
                
            }
        }
        public bool CheckRange(Transform target)
        {
            //b-a
            //float distance = (target.position - transform.position).magnitude
            float distance = Vector3.Distance(transform.position, target.position);
            return distance < range;
        }
        public bool CheckAngle(Transform target)
        {
            Vector3 foward = transform.forward;
            //b-a
            Vector3 dirToTarget = (target.position - transform.position);
            float angleToTarget = Vector3.Angle(foward, dirToTarget);
            return angle / 2 > angleToTarget;
        }

        public bool CheckView(Transform target)
        {
            Vector3 diff = (target.position - transform.position);
            Vector3 dirToTarget = diff.normalized;
            float distanceToTarget = diff.magnitude;

            RaycastHit hit;

            return !Physics.Raycast(transform.position, dirToTarget, out hit, distanceToTarget, mask);
        }
        public bool CheckRangeBack(Transform target)
        {
            //b-a
            //float distance = (target.position - transform.position).magnitude
            float distance = Vector3.Distance(transform.position, target.position);
            return distance < rangeBack;
        }
        public bool CheckAngleBack(Transform target)
        {
        Vector3 backward = -transform.forward; // Cambio de forward a backward
        Vector3 dirToTarget = (target.position - transform.position);
        float angleToTarget = Vector3.Angle(backward, dirToTarget); // Usar backward en lugar de forward
        return angleToTarget < angleBack;
    }

        public bool CheckViewBack(Transform target)
        {
            Vector3 diff = (target.position - transform.position);
            Vector3 dirToTarget = diff.normalized;
            float distanceToTarget = diff.magnitude;

            RaycastHit hit;

            return !Physics.Raycast(transform.position, dirToTarget, out hit, distanceToTarget, mask);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, range);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangeBack);


            Gizmos.color = Color.red;

            Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * range);
            Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * range);
        Gizmos.color = Color.cyan;

        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angleBack / 2, 0) * transform.forward * rangeBack);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angleBack / 2, 0) * transform.forward * rangeBack);
    }
}
