using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private GameObject _explosionPrefab;

    [Header("MOVEMENT")]
    [SerializeField] private float _speed = 15;
    [SerializeField] private float _rotateSpeed = 95;

    [Header("PREDICTION")]
    [SerializeField] private float _maxDistancePredict = 100;
    [SerializeField] private float _minDistancePredict = 5;
    [SerializeField] private float _maxTimePrediction = 5;
    private Vector3 _standardPrediction, _deviatedPrediction;

    [Header("DEVIATION")]
    [SerializeField] private float _deviationAmount = 50;
    [SerializeField] private float _deviationSpeed = 2;

    [Header("TARGET")]
    [SerializeField] private LayerMask _targetLayer;

    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _maxDistancePredict, _targetLayer);

        if (hitColliders.Length > 0)
        {
            Transform closestTarget = FindClosestTarget(hitColliders);

            if (closestTarget != null)
            {
                Target target = closestTarget.GetComponent<Target>();
                if (target != null)
                {
                    PredictMovement(target);
                    AddDeviation();
                    RotateRocket();
                }
            }
        }
    }

    private Transform FindClosestTarget(Collider[] targets)
    {
        Transform closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (var targetCollider in targets)
        {
            float distance = Vector3.Distance(transform.position, targetCollider.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = targetCollider.transform;
            }
        }

        return closestTarget;
    }

    private void PredictMovement(Target target)
    {
        var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, Vector3.Distance(transform.position, target.transform.position));
        var predictionTime = Mathf.Lerp(0, _maxTimePrediction, leadTimePercentage);
        _standardPrediction = target.Rb.position + target.Rb.velocity * predictionTime;
    }

    private void AddDeviation()
    {
        var deviation = new Vector3(Mathf.Cos(Time.time * _deviationSpeed), 0, 0);
        var predictionOffset = transform.TransformDirection(deviation) * _deviationAmount;
        _deviatedPrediction = _standardPrediction + predictionOffset;
    }

    private void RotateRocket()
    {
        var heading = _deviatedPrediction - transform.position;
        var rotation = Quaternion.LookRotation(heading);
        _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_explosionPrefab) Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        if (collision.transform.TryGetComponent<IExplode>(out var ex)) ex.Explode();

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _standardPrediction);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_standardPrediction, _deviatedPrediction);
    }
}

