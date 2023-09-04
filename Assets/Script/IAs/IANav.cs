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
    public Node to2;
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
        isTo = true;
    }
    bool isTo = true;
    void road2()
    {
        path = Pathfinding.Path(to, to2);
        isTo = false;
    }
    public void Op_UpdateGameplay()
    {

        
       
        if (path.Count > 0)
        {
            List<Node> copy = new List<Node>(path);

            if (Vector2.Distance(copy[currentWaypointIndex].transform.position, transform.position) > 8)
            {
                Vector3 dir = (copy[currentWaypointIndex].transform.position - transform.position).normalized;
               // Debug.Log(copy[currentWaypointIndex].name);
                KartEntity.LookRotate(dir);
            }
            else
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % path.Count;
            }
            if (currentWaypointIndex >= path.Count - 1) 
            {
                roads();
                currentWaypointIndex = 0;
            }
            copy = path;
        }

    }

    void roads() 
    {
        if (isTo)
        {
            road2();
            return;
        }
        else 
        {
            road();
            return;
        }
      
    }
    public void Op_UpdateUX()
    {
    }
}



