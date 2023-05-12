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



    // Update is called once per frame
    void Update()
    {
        var gameplayLenght = GameplayUpdates.Count;
        var UXLenght = UXUpdates.Count;

        GameplaynextTime += Time.deltaTime;
        UInextTime += Time.deltaTime;


        if (GameplaytimePerFrame < GameplaynextTime)
        {
            for (int i = 0; i < gameplayLenght; i++)
            {
                if ( GameplayUpdates[i].isActiveAndEnabled)
                {
                    GameplayUpdates[i].UpdateGameplay();
                }
                else
                {
                    RemoveUpdate(GameplayUpdates[i]);
                }
            }

            GameplaynextTime = 0;
        }
      
        if (UItimePerFrame < UInextTime)
        {
            for (int i = 0; i < UXLenght; i++)
            {
                if ( UXUpdates[i].isActiveAndEnabled)
                {
                    UXUpdates[i].UpdateUX();
                }
                else
                {
                    RemoveUpdate(UXUpdates[i]);
                }
            }

            UInextTime = 0;
        }
     
    }


    #region Method

    public void AddUpdate(OptimizatedUpdateUX UX)
    {
        UXUpdates.Add(UX);
    }

    public void AddUpdate(OptimizatedUpdateGameplay Gameplay)
    {
        GameplayUpdates.Add(Gameplay);
    }
    public void RemoveUpdate(OptimizatedUpdateUX UX)
    {
        UXUpdates.Remove(UX);
    }

    public void RemoveUpdate(OptimizatedUpdateGameplay Gameplay)
    {
        GameplayUpdates.Remove(Gameplay);
    }

    #endregion
}
