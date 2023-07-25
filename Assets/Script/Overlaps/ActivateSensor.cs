using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivateSensor : MonoBehaviour,IOptimizatedUpdate
{
    public Transform SensorTransform;
    public LayerMask UpdatesLayers;
    public int radius;
    public int maxUpdates;
    public Collider[] _colliders;
    private List<Collider> _collidersBack;

    LookUpTable<GameObject,OptimizatedUpdateGameplay> lookUpTable;

    private void Start()
    {
        _colliders = new Collider[maxUpdates];
        _collidersBack = new List<Collider>();

        lookUpTable = new LookUpTable<GameObject, OptimizatedUpdateGameplay>(ActionLookUpTable);
    }

    OptimizatedUpdateGameplay ActionLookUpTable(GameObject key) 
    {
        return key.GetComponent<OptimizatedUpdateGameplay>();
    }
    public void Op_UpdateGameplay()
    {
        int count = Physics.OverlapSphereNonAlloc(SensorTransform.position, radius, _colliders, UpdatesLayers);

        for (int i = 0; i < count; i++)
        {
            if (_colliders[i].GetComponent<OptimizatedUpdateGameplay>() == null) continue;

            OptimizatedUpdateGameplay optimizatedUpdateGameplay =
                lookUpTable.GetValue(_colliders[i].gameObject);
            
            if (_collidersBack.Count > 0)
                if (_collidersBack.Contains(_colliders[i]) ) continue;
                

            optimizatedUpdateGameplay.AddUpdateManger();
            _collidersBack.Add(_colliders[i]);

        }

        var diferencia = _collidersBack != null ? _collidersBack.Except(_colliders) : null;
        if (diferencia != null && diferencia.Any())
        {
            for (int i =  0; i < _collidersBack.Count; i++)
            {
                var collider = _collidersBack[i];
                if (collider != null && !diferencia.Contains(collider))
                {
                    lookUpTable.GetValue(_colliders[i].gameObject).RemoveUpdateManager();
                    _collidersBack.RemoveAt(i);
                }
            }
        }
        if (count == 0 && _colliders.Length != 0)
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                _colliders[i] = null;
            }
        }
    }

    public void Op_UpdateUX()
        {
        }
}
