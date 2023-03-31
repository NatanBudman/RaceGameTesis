using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IANav : MonoBehaviour
{
    public IAModel _model;
    public KartController _KartController;

    [Header("NAV3")] [Space] [Header("RayCast")] [Space] [SerializeField]
    private float[] AngleRayCast = { 0, 40, 90, 140, 180, -40, -90, -140 };

    [SerializeField] private float RangeObstacleDetection;
    public bool isCheckRaycast = true;


    [Space] [Header("Velocity")] [Space] public float Speed;
    public float SpeedToRotate;


    [Space] [Header("Others")] [Space] public Transform point;
    public IAChecks[] checks;
    [HideInInspector] public IAChecks auxiliar;

    public GameObject IAmodel;
    private void Awake()
    {
        _model.RayDist = RangeObstacleDetection;
        //Nav3();
        // Nav2();
    }

    private void Start()
    {
    }

    private float cooldownNavUpdate = 2;
    private float TimecooldownNavUpdate;

    private void Update()
    {
        Nav3();
        /*
        TimecooldownNavUpdate += Time.deltaTime;
        if (isCheckRaycast && auxiliar.RayCastChocando || auxiliar.Chocando ||
            TimecooldownNavUpdate >= cooldownNavUpdate)
        {
            Nav3();
        }

        if (isCheckRaycast == false && auxiliar.Chocando || TimecooldownNavUpdate >= cooldownNavUpdate)
        {
            Nav3();
        }
        */
        if (auxiliar != null)
        {
         
            LookRotate(IAmodel, auxiliar.gameObject);
            LookRotate(this.gameObject, auxiliar.gameObject);
        }
    }

    void LookRotate(GameObject gameObjectRotate, GameObject Target)
    {
        Vector3 dir = Target.transform.position - gameObjectRotate.transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(dir);

        gameObjectRotate.transform.rotation = Quaternion.RotateTowards(gameObjectRotate.transform.rotation,
            targetRotation, SpeedToRotate * Time.deltaTime);
    }

    public void Nav3()
    {
        for (int i = 0; i < AngleRayCast.Length; i++)
        {
            Vector3 dir = Vector3.Normalize(checks[i].transform.position - transform.position);
            
            float angulo = Vector3.Angle(transform.forward, dir);
            if (checks[i]._NegativeDir)
            {
                angulo = -angulo;
            }
            AngleRayCast[i] = angulo;
        }
        
        for (int j = 0; j < checks.Length; j++)
        {
            if (auxiliar == null)
            {
                auxiliar = checks[j];
            }

            float AuxilairDist = Vector3.Distance(point.transform.position, auxiliar.transform.position);
            float AuxDist = Vector3.Distance(point.transform.position, checks[j].transform.position);

            switch (isCheckRaycast)
            {
                case false:
                {
                    if (AuxDist < AuxilairDist || auxiliar.Chocando
                        && !checks[j].Chocando)
                    {
                        auxiliar = checks[j];
                    }

                    break;
                }
                case true:
                {

                    checks[j].RayCastChocando = _model.isCollisionObstacle(checks[j].transform, AngleRayCast[j]);

                    if (AuxDist < AuxilairDist || auxiliar.RayCastChocando || auxiliar.Chocando
                        && !checks[j].Chocando && !checks[j].RayCastChocando)
                    {
                        auxiliar = checks[j];
                    }

                    break;
                }
            }

            TimecooldownNavUpdate = Random.Range(-1, 0);
        }



    }
}



