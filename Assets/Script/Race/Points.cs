using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public GameObject granPoint;
    public GameObject[] checkPoints;

    private int index = 0;

    public int indexOrder = 0;
    // Start is called before the first frame update
    void Start()
    {
        granPoint.transform.position = checkPoints[index].transform.position;
        
        int order = 0;
        for (int i = 0; i < checkPoints.Length; i++)
        {
           checkPoints[i].GetComponent<Point>().PointOrder = order;
           order++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPoint()
    {
        index++;
        indexOrder++;
        if (index > checkPoints.Length - 1)
        {
            for (int i = 0; i < checkPoints.Length; i++)
            {
                Transform newpos = checkPoints[i].GetComponent<Point>().InitialPos;
                checkPoints[i].transform.position = new Vector3( newpos.position.x + Random.Range(-10, 10), newpos.position.y,
                    newpos.position.z + Random.Range(-10, 10));
            }

            indexOrder = 0;
            index = 0;
        }
        granPoint.transform.position = checkPoints[index].transform.position;

    }
    
}
