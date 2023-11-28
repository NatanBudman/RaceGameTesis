using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public GameManager _Manager;
    
    public List<OptimizatedUpdateGameplay> GameplayUpdates;
    [Space]

    public List<OptimizatedUpdateUX> UXUpdates;
    
    #region FPS

        private int GameplayFPS => _Manager.GameplayFPS;
        private int UXFPS => _Manager.UXFPS;
        
        private float GameplaytimePerFrame;
        private float UItimePerFrame;
        
        private float GameplaynextTime = 0;
        private float UInextTime = 0;


    #endregion

    private void Awake()
    {
        GameplayUpdates = new List<OptimizatedUpdateGameplay>(1000);
        UXUpdates = new List<OptimizatedUpdateUX>(1000);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set Limited FPS
        GameplaytimePerFrame = 1f / GameplayFPS;
        UItimePerFrame = 1f / UXFPS;

    }
    void Update()
    {
        var gameplayLenght = GameplayUpdates.Count;
        var UXLenght = UXUpdates.Count;

        GameplaynextTime += Time.deltaTime;
        UInextTime += Time.deltaTime;

        if (GameplaynextTime >= GameplaytimePerFrame)
        {
            List<OptimizatedUpdateGameplay> updatesCopy = new List<OptimizatedUpdateGameplay>(GameplayUpdates);

            foreach (var updateGameplay in updatesCopy)
            {
                if (updateGameplay != null)
                {
                    updateGameplay.UpdateGameplay();
                }
            }
            updatesCopy = GameplayUpdates;

            GameplaynextTime = 0;

        }
      
        if (UInextTime >= UItimePerFrame)
        {
            for (int i = 0; i < UXLenght; i++)
            {
                    UXUpdates[i].UpdateUX();
            }

            UInextTime = 0;
        }
     
    }


    #region Method
// Agregar objeto a actualizar a la lista
   public void AddUpdate(OptimizatedUpdateGameplay obj)
    {
        if (!GameplayUpdates.Contains(obj))
        {
            GameplayUpdates.Add(obj);
        }
    }

// Quitar objeto de la lista
  public  void RemoveUpdate(OptimizatedUpdateGameplay obj)
    {
        if (GameplayUpdates.Contains(obj))
        {
            GameplayUpdates.Remove(obj);
        }
    }
    public void AddUpdate(OptimizatedUpdateUX UX)
    {
        UXUpdates.Add(UX);
    }

    public void RemoveUpdate(OptimizatedUpdateUX UX)
    {
        UXUpdates.Remove(UX);
    }


    #endregion
}
