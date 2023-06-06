using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
[RequireComponent(typeof(OptimizatedUpdateGameplay))]
public class IANav : MonoBehaviour,IOptimizatedUpdate
{
    public KartController _KartController;
    public KartEntity KartEntity;
    public KartEntity playerEntiti;

    private float Speed => KartEntity.GetSpeed;

    [Space] [Header("Others")] [Space] public Transform point;

    [Header("Persuit")] 
    public int time;

    public float _distanceToPursuit;

    [Header("Obstacles")] 
    [SerializeField] private float radius;
    public LayerMask Mask;
    public int maxObstacleDetected;
    public float angle;
    public int mutliplier;

    private ObstacleAvoidance _obstacleAvoidance;
    private Pursuit _pursuit;
    void Inicialize()
    {
        var Obstacle = new ObstacleAvoidance(transform,Mask,maxObstacleDetected,radius,angle);
        var pursuit = new Pursuit(transform,playerEntiti,time);
        _obstacleAvoidance = Obstacle;
        _pursuit = pursuit;
    }

    private void Awake()
    {
        Inicialize();
    }

    private Vector3 GetDir()
    {
        Vector3 dir = Vector3.zero;
        
        float diffDist = Vector3.Distance(playerEntiti.transform.position, transform.position);
        
        if (_distanceToPursuit > diffDist && transform.position.z < playerEntiti.transform.position.z) 
        {
            dir = (playerEntiti.transform.position - transform.position).normalized;
            Debug.Log("entre");
        }
        else if(_distanceToPursuit < diffDist)
        {
            dir = (point.transform.position - transform.position).normalized;
        }

        return dir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,radius);
        Gizmos.color = Color.red;
        
        Gizmos.DrawRay(transform.position,Quaternion.Euler(0,angle / 2,0)*transform.forward * radius);
        Gizmos.DrawRay(transform.position,Quaternion.Euler(0,-angle / 2,0)*transform.forward * radius);
    }

    public void Op_UpdateGameplay()
    {
        Vector3 obstacleAvoid = _obstacleAvoidance.GetDir();
        Vector3 direc = (GetDir() + obstacleAvoid * mutliplier).normalized;
        
        KartEntity.LookRotate(direc);
    }

    public void Op_UpdateUX()
    {
    }
}



