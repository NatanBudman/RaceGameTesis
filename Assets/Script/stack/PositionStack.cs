using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionStack : MonoBehaviour
{
    // Crea la lista de objetos
    public List<GameObject> Runners = new List<GameObject>();

    public GameObject[] Points;
    public Text[] RunnersList;
    private Transform[] OrderList;

    [Header("Table Position")]
    public Transform SpawnTabletPositions;
    public GameObject TextPrefab;
    
    private void Start()
    {
        KartEntity[] kartEntities = FindObjectsOfType<KartEntity>();
        int lenght = kartEntities.Length;
        RunnersList = new Text[lenght];
        OrderList = new Transform[lenght];

        int y = 0;
        for (int i = 0; i < lenght; i++) 
        {
            Runners.Add(kartEntities[i].gameObject);

            y += 20;
            Vector2 pos = new Vector2(SpawnTabletPositions.position.x , SpawnTabletPositions.position.y + y);
            GameObject text = Instantiate(TextPrefab, pos, Quaternion.identity, SpawnTabletPositions);
            OrderList[i] = text.transform;
            RunnersList[i] = text.GetComponent<Text>();
        }
    }
    public void AddRunners(GameObject runner)
    {
        Runners.Add(runner);
    }
    private void Update()
    {
        OrganizePos();
    }

    public int GetPos(GameObject key) 
    {
        for (int i = 0; i < Runners.Count; i++) 
        {
            if (Runners[i].gameObject == key) 
            {
                return i + 1;
            }
        }
        return 0;
    }
    public void OrganizePos()
    {
        for (int i = 0; i < Runners.Count - 1; i++)
        {
            for (int j = i + 1; j < Runners.Count; j++)
            {
                int valorObjetoI_1 = Runners[i].GetComponent<KartEntity>().currentTurning;
                int valorObjetoJ_1 = Runners[j].GetComponent<KartEntity>().currentTurning;
                int valorObjetoI_2 = Runners[i].GetComponent<KartEntity>().currentPoint;
                int valorObjetoJ_2 = Runners[j].GetComponent<KartEntity>().currentPoint;
                float valorObjetoI_3 = Vector3.Distance(Points[Runners[i].GetComponent<KartEntity>().currentPoint].transform.position, Runners[i].transform.position) ;
                float valorObjetoJ_3 = Vector3.Distance(Points[Runners[j].GetComponent<KartEntity>().currentPoint].transform.position, Runners[j].transform.position);

                // Si el primer valor de j es mayor que el de i, o si es igual y el segundo valor de j es mayor que el de i,
                // o si es igual y el tercer valor de j es mayor que el de i, se intercambian los objetos
                if ((valorObjetoJ_1 > valorObjetoI_1) ||
                    (valorObjetoJ_1 == valorObjetoI_1 && valorObjetoJ_2 > valorObjetoI_2) ||
                    (valorObjetoJ_1 == valorObjetoI_1 && valorObjetoJ_2 == valorObjetoI_2 && valorObjetoJ_3 < valorObjetoI_3))
                {
                    GameObject objetoTemp = Runners[i];
                    Runners[i] = Runners[j];
               
                    Runners[j] = objetoTemp;

                }
            }
     
        }

        for (int i = 0; i < Runners.Count; i++)
        {
            RunnersList[i].text = $"{Runners[i].name }  :   {1 + i }";
        }
    }
}
