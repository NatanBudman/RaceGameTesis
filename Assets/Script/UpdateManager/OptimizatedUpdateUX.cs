using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizatedUpdateUX : MonoBehaviour
{
    [SerializeField] private UpdateManager _updateManager;

    private IOptimizatedUpdate[] _optimizatedUpdate;

    private void Start()
    {
        _updateManager = FindObjectOfType<UpdateManager>();
        
        _updateManager.AddUpdate(this);

        _optimizatedUpdate = GetComponents<IOptimizatedUpdate>();
    }
    
    public void UpdateUX()
    {
        int _optimizatedUpdates = _optimizatedUpdate.Length;
        
        for (int i = 0; i < _optimizatedUpdates; i++)
        {
            _optimizatedUpdate[i].Op_UpdateUX();
        }
    }
}
