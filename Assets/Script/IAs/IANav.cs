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
    public pathfinding Pathfinding;
    public Node from;
    public Node to;
    public List<Node> path;

    private float Speed => KartEntity.GetMaxRealSpeed;

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

    private void Start()
    {
        road();
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
    
    private int currentWaypointIndex = 0;

    void road()
    {
        path = Pathfinding.Path(from,to);
    }

   
    public void Op_UpdateGameplay()
    {
       // Vector3 obstacleAvoid = _obstacleAvoidance.GetDir();
      //  Vector3 direc = (GetDir() + obstacleAvoid * mutliplier).normalized;
        
      //  KartEntity.LookRotate(direc);
      //Mueve el objeto
      
      if (Vector3.Distance( path[currentWaypointIndex].transform.position,transform.position) > 1)
      {
          Vector3 dir = (path[currentWaypointIndex].transform.position - transform.position).normalized;
          Debug.Log(currentWaypointIndex);
          KartEntity.LookRotate(dir);
      } else {
          currentWaypointIndex = (currentWaypointIndex + 1) % path.Count;
      }
      
    }

    public void Op_UpdateUX()
    {
    }
}



