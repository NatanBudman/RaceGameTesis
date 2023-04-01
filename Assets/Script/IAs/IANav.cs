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


    [Space] [Header("Velocity")] [Space] private float Speed;
    public float SpeedToRotate;


    [Space] [Header("Others")] [Space] public Transform point;
    public IAChecks[] checks;
    [HideInInspector] public IAChecks auxiliar;

    public GameObject IAmodel;
    private void Awake()
    {
        _model.RayDist = RangeObstacleDetection;
        
    }

    private void Start()
    {
        Speed = _KartController.maxSpeed;
    }

    private void Update()
    {
        Nav3();
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
// DIOS SE APIADE EL QUE TENGA QUE ENTENDER O LEER ESTO,AMEN.
    public void Nav3()
    {
        
        if (_KartController.realSpeed >= Speed - 0.5f)
        {
            _KartController.maxSpeed = Speed / 2;
        }
        else
        {
            _KartController.maxSpeed = Speed;
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
                    if (IAmodel.transform.rotation.y > 90 ||IAmodel.transform.rotation.y < -90 )
                    {
                        transform.rotation = new Quaternion(0,0,0,0);
                    }
                    for (int i = 0; i < AngleRayCast.Length; i++)
                    {
                        Vector3 dir = Vector3.Normalize(checks[i].transform.position - transform.position);
                        float angulo = Vector3.Angle(transform.forward, dir);
                        
                        // la verdad no se como explicar la chota esta asi que dejala como esta y ya
                        if (IAmodel.transform.position.z > checks[i].transform.position.z) 
                        {
                            checks[i]._NegativeDir = true;
                        }
                        else
                        {
                            checks[i]._NegativeDir = false;
                        }
                        if (IAmodel.transform.position.z == checks[i].transform.position.z 
                            && IAmodel.transform.position.x > checks[i].transform.position.x)
                        {
                            checks[i]._NegativeDir = true;
                        }
                        else if(IAmodel.transform.position.z == checks[i].transform.position.z 
                                && IAmodel.transform.position.x < checks[i].transform.position.x)
                        {
                            checks[i]._NegativeDir = false;
                        }
                        
                        
                        
                        if (checks[i]._NegativeDir)
                        {
                            angulo = -angulo;
                        }
                        AngleRayCast[i] = angulo;
                    }
                    
                    checks[j].RayCastChocando = _model.isCollisionObstacle(checks[j].transform, AngleRayCast[j]);

                    if (AuxDist < AuxilairDist || auxiliar.RayCastChocando || auxiliar.Chocando
                        && !checks[j].Chocando && !checks[j].RayCastChocando)
                    {
                        auxiliar = checks[j];
                    }
               

                    break;
                }
            }

        }

    }

   
}



