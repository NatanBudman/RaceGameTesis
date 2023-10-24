using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
[RequireComponent(typeof(OptimizatedUpdateGameplay))]
public class IANav : MonoBehaviour, IOptimizatedUpdate
{
    public KartController _KartController;
    public KartEntity KartEntity;
    public KartEntity playerEntiti;
    public pathfinding Pathfinding;
    public Node from;
    public Node to;
    public List<Node> path;
    public PointNodes Nodes;
    TurboManager turbo;
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
    public LayerMask IgnoreLayer;
    public Transform pivot;
    int index = 0;
    void Inicialize()
    {
        var Obstacle = new ObstacleAvoidance(transform, Mask, maxObstacleDetected, radius, angle);
        var pursuit = new Pursuit(transform, playerEntiti, time);
        _obstacleAvoidance = Obstacle;
        _pursuit = pursuit;
    }

    private void Awake()
    {

        Inicialize();
    }

    private void Start()
    {
        turbo = GetComponent<TurboManager>();
        P();
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
        else if (_distanceToPursuit < diffDist)
        {
            dir = (point.transform.position - transform.position).normalized;
        }

        return dir;
    }

    private int currentWaypointIndex = 0;

    void P() 
    {
        int lenghNodes = Nodes.Nodes.Length;
        if (index >= lenghNodes) index = 0;
        int lenght = Nodes.Nodes[index].CheckPOintNodes.Length;
        int random = Random.Range(0, lenght);
        to = Nodes.Nodes[index].CheckPOintNodes[random];
        path = Pathfinding.Path(from, to);
        index++;
    }
    void NextRoad()
    {
        Debug.Log("1");
        int lenghNodes = Nodes.Nodes.Length;
        if (index >= lenghNodes) index = 0;
        int lenght = Nodes.Nodes[index].CheckPOintNodes.Length;
        int random = Random.Range(0, lenght);
        to = Nodes.Nodes[index].CheckPOintNodes[random];
        path = Pathfinding.Path(from, to);
        from = to;
        index++;
    }

    public void Op_UpdateGameplay()
    {



        if (path.Count > 0)
        {
            List<Node> copy = new List<Node>(path);

            float dis = Vector2.Distance(copy[currentWaypointIndex].transform.position, transform.position);


            Vector3 dir = (copy[currentWaypointIndex].transform.position - transform.position).normalized;
            KartEntity.LookRotate(dir);
            if (dis < 10)
            {
                if (copy[currentWaypointIndex + 1] != null)
                {
                    if (isSeeNode(copy[currentWaypointIndex + 1].transform, pivot))
                        currentWaypointIndex = (currentWaypointIndex + 1) % path.Count;
                }
                else 
                {
                    if (isSeeNode(copy[currentWaypointIndex].transform, pivot))
                        currentWaypointIndex = (currentWaypointIndex + 1) % path.Count;
                }
            }
            if (currentWaypointIndex >= path.Count - 1)
            {
                NextRoad();
                StartCoroutine(Turbo());
                currentWaypointIndex = 0;
            }
                copy = path;
        }

        float dist = Vector3.Distance(transform.position, playerEntiti.transform.position);

        if (dist < 10 || playerEntiti.currentPoint > KartEntity.currentPoint || playerEntiti.currentTurning > KartEntity.currentTurning)
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



