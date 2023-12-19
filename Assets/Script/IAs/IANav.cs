using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
[RequireComponent(typeof(OptimizatedUpdateGameplay))]
public class IANav : MonoBehaviour, IOptimizatedUpdate
{
    public KartEntity KartEntity;
    public KartEntity playerEntiti;
    public Node from;
    TurboManager turbo;
    public LayerMask IgnoreLayer;
    public Transform pivot;
    private void Awake()
    {
        turbo = GetComponent<TurboManager>();
    }

    public void Op_UpdateGameplay()
    {

        float dis = Vector2.Distance(from.transform.position, transform.position);

        Vector3 dir = (from.transform.position - transform.position).normalized;
        KartEntity.LookRotate(dir);

        if (dis < 9.25f && isSeeNode(from.transform, pivot))
        {
            var random = Random.Range(0, from.NeighboarNodes.Length);
            from = from.NeighboarNodes[random];
        }

        float dist = Vector3.Distance(transform.position, playerEntiti.transform.position);

        if ((dist > 10 && playerEntiti.currentPoint > KartEntity.currentPoint || playerEntiti.currentTurning > KartEntity.currentTurning) || turbo.turboAmount > 50)
        {
            turbo.Turbo(true);
        }

    }
    bool isSeeNode(Transform target, Transform from)
    {
        Vector3 direccion = target.position - from.position;
        RaycastHit hit;
        float sphereRadius = 0.5f; // Set the desired radius for the sphere cast

        if (Physics.SphereCast(from.position, sphereRadius, direccion, out hit, Mathf.Infinity, IgnoreLayer))
        {
            // Verificar si el objeto observado está en línea de visión directa
            if (hit.transform != target)
            {
                return false;
            }
        }

        return true;
    }
    IEnumerator Turbo()
    {
        turbo.GetTurbo();
        yield return new WaitForSeconds(1.5f);
        StopCoroutine(Turbo());
    }
    public void Op_UpdateUX()
    {
    }
}



