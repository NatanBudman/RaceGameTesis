using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour,IOptimizatedUpdate
{
    public GameManager gameManager;

    public GameObject BoxModel;

    public float Rotatespeed;

    public float CooldownBoxSpawned;
    private float _currentBoxSpawned;

    #region Events

    public delegate void BoxEvent();

    public event BoxEvent OnBoxFuncion;

    #endregion

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        OnBoxFuncion += Box;
    }
    private void OnBox()
    {
        if (OnBoxFuncion != null)
        {
            OnBoxFuncion();
        }
        else if(OnBoxFuncion == null)
        {
            _currentBoxSpawned += Time.deltaTime;
            
            if (_currentBoxSpawned >= CooldownBoxSpawned) 
            {
                Debug.Log("entre");
                OnBoxFuncion += Box;
                BoxModel.SetActive(true);
                _currentBoxSpawned = 0;
            }
        }
    }

    void Box()
    {
        BoxModel.transform.Rotate(0, transform.rotation.y + Rotatespeed * Time.fixedDeltaTime * 0.5f, 0);


        Collider boxCollider = BoxModel.GetComponent<Collider>();


        foreach (GameObject kart in gameManager.KartsInGame) 
        {
            Collider kartCollider = kart.GetComponent<Collider>();

            if (kartCollider.bounds.Intersects(boxCollider.bounds)) 
            {

                Invoke("DeactivateBoxModel", 1f);
                OnBoxFuncion -= Box;
            }
        }
       
    }
    void DeactivateBoxModel()
    {
        BoxModel.SetActive(false);
    }

    public void Op_UpdateGameplay()
    {
        OnBox();
    }

    public void Op_UpdateUX()
    {
    }


}
