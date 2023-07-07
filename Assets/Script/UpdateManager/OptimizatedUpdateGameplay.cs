using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizatedUpdateGameplay : MonoBehaviour
{
    [SerializeField] private UpdateManager _updateManager;

    private IOptimizatedUpdate[] _optimizatedUpdate;

    public bool isAddManager;

    private void Start()
    {
        _updateManager = FindObjectOfType<UpdateManager>();
        
        if (isAddManager)
            AddUpdateManger();
        
        _optimizatedUpdate = GetComponents<IOptimizatedUpdate>();

    }

    public void AddUpdateManger()
    {
        _updateManager.AddUpdate(this);
    }

    public void RemoveUpdateManager()
    {
        _updateManager.RemoveUpdate(this);
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
