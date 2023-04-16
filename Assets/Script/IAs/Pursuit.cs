using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : IIAState
{
    private Transform _origin;
    private KartEntity _target;
    private float _time;
    public Pursuit(Transform origin,KartEntity target, float time)
    {
        _origin = origin;
        _target = target;
        _time = time;
    }
    public Vector3 GetDir()
    {
        float distance = Vector3.Distance(_origin.position, _target.transform.position);
        
        Vector3 point = _target.transform.position + _target.GetForward * 
            Mathf.Clamp(_target.GetVelocity * _time,0,distance);
        return (point - _origin.position).normalized;
    }
}
