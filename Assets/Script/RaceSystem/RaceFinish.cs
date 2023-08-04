using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceFinish : MonoBehaviour
{
    public GameManager gameManager;

    public int pointsInRace;

    public int MaxTurning => gameManager.RaceLaps;

    LookUpTable<GameObject,KartEntity> lookUpTable ;
    LookUpTable<GameObject,KartUI> lookUpTableUI ;

    private void Start()
    {
        lookUpTable = new LookUpTable<GameObject, KartEntity>(ActionLookUpTable);
        lookUpTableUI = new LookUpTable<GameObject, KartUI>(ActionLookUpTableUI);
    }
    KartEntity ActionLookUpTable(GameObject key)
    {
        return key.GetComponent<KartEntity>();
    }
    KartUI ActionLookUpTableUI(GameObject key)
    {
        return key.GetComponent<KartUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ActionLookUpTable(other.gameObject) != null)
        {
            KartEntity kart = ActionLookUpTable(other.gameObject);
       


            if (kart.isNextCurrentPoint(pointsInRace))
            {
                if (kart.GetCurrentTurning() == MaxTurning)
                {

                }
                else 
                {
                    kart.GetPoint();

                    if (other.gameObject.GetComponent<KartUI>() != null)
                    {
                        KartUI kartUI = ActionLookUpTableUI(other.gameObject);
                        kartUI.currentLaps += 1;
                        kartUI.currentTotalLaps = MaxTurning;
                        kartUI.UpdateLaps();
                    }

                }
              
            }
        }
    }
}
