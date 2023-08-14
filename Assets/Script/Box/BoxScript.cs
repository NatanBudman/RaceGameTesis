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

    public bool isHaveCoin;
    public int MinCoins;
    public int MaxCoins;

    private Collider _sefltColl;

    #region Events

    public delegate void BoxEvent();

    public event BoxEvent OnBoxFuncion;

    #endregion



    LookUpTable<GameObject, KartEntity> lookUpTable;
    LookUpTable<GameObject, Collider> lookUpTableColl;

    private void Start()
    {
        _sefltColl = this.gameObject.GetComponent<Collider>();
        lookUpTable = new LookUpTable<GameObject, KartEntity>(ActionLookUpTable);
        lookUpTableColl = new LookUpTable<GameObject, Collider>(ActionLookUpTableCollider);

        gameManager = FindObjectOfType<GameManager>();

        OnBoxFuncion += Box;
    }
    KartEntity ActionLookUpTable(GameObject key)
    {
        return key.GetComponent<KartEntity>();
    }
    Collider ActionLookUpTableCollider(GameObject key)
    {
        return key.GetComponent<Collider>();
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
                OnBoxFuncion += Box;
                BoxModel.SetActive(true);
                _sefltColl.enabled = true;
                _currentBoxSpawned = 0;
            }
        }
    }

    void Box()
    {
        BoxModel.transform.Rotate(0, transform.rotation.y + Rotatespeed * Time.deltaTime, 0);

        foreach (GameObject kart in gameManager.KartsInGame) 
        {
            Collider kartCollider = ActionLookUpTableCollider(kart);

            if (kartCollider.bounds.Intersects(_sefltColl.bounds)) 
            {

                Invoke("DeactivateBoxModel", 0.5f);

                if (isHaveCoin) ActionLookUpTable(kartCollider.gameObject).Coins += Random.Range(MinCoins, MaxCoins);

                _sefltColl.enabled = false;

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
