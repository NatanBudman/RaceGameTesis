using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionStack : MonoBehaviour
{
    // Crea la lista de objetos
    List<GameObject> Runners = new List<GameObject>();

    public GameManager gameManager;

    private void Start()
    {
        KartEntity[] kartEntities = FindObjectsOfType<KartEntity>();

        for (int i = 0; i < kartEntities.Length; i++) 
        {
            Runners.Add(kartEntities[i].gameObject);
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
                return i;
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
                float valorObjetoI_3 = Vector3.Distance(gameManager.Points[Runners[i].GetComponent<KartEntity>().currentPoint].transform.position, Runners[i].transform.position) ;
                float valorObjetoJ_3 = Vector3.Distance(gameManager.Points[Runners[j].GetComponent<KartEntity>().currentPoint].transform.position, Runners[j].transform.position);

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
    }
}
