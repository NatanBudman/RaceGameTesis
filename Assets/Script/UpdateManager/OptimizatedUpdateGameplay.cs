using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizatedUpdateGameplay : MonoBehaviour
{
    [SerializeField] private UpdateManager _updateManager;

    private IOptimizatedUpdate[] _optimizatedUpdate;

    private void Start()
    {
        _updateManager.AddUpdate(this);
        
        _optimizatedUpdate = GetComponents<IOptimizatedUpdate>();

    }

    public void UpdateGameplay()
    {
        int _optimizatedUpdates = _optimizatedUpdate.Length;
        
        for (int i = 0; i < _optimizatedUpdates; i++)
        {
            _optimizatedUpdate[i].Op_UpdateGameplay();
        }
    }
}
