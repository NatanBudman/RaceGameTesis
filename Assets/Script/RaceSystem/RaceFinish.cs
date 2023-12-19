using UnityEngine;

public class RaceFinish : MonoBehaviour
{
    public GameManager gameManager;

    public int pointsInRace;

    public GameObject WinPanel;
    public Timer timer;
    public int MaxTurning => gameManager.RaceLaps;

    LookUpTable<GameObject, KartEntity> lookUpTable;
    LookUpTable<GameObject, KartUI> lookUpTableUI;

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

                kart.GetPoint();

                if (kart.gameObject.tag == "Player")
                {
                    timer.SaveLapTime();
                    timer.StartLap();
                }
                if (other.gameObject.GetComponent<KartUI>() != null)
                {
                    KartUI kartUI = ActionLookUpTableUI(other.gameObject);
                    kartUI.currentLaps += 1;
                    kartUI.currentTotalLaps = MaxTurning;
                    kartUI.UpdateLaps();
                }

                if (kart.GetCurrentTurning() == MaxTurning)
                {


                    if (kart.gameObject.tag == "Player")
                    {

                        Time.timeScale = 0;

                        WinPanel.SetActive(true);
                        timer.StopTimer();
                        timer.SaveLapTime();
                        timer.ShowLapTimes();

                    }

                }

            }
        }
    }
}
