using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int ID;
    public GameObject ParentNodes;
    public Node[] NeighboarNodes = new Node[5];

    
    private void Awake()
    {
        string nombreCompleto = gameObject.name;

        // Extraer una parte del nombre del objeto
        if (nombreCompleto.Length == 10)
        {
            String m = nombreCompleto.Substring(6, 3);

            ID = int.Parse(m);
        }
        if (nombreCompleto.Length == 9) 
        {
            String m = nombreCompleto.Substring(6, 2);

            ID = int.Parse(m);
        }
        if (nombreCompleto.Length == 8)
        {
            String m = nombreCompleto.Substring(6, 1);

            ID = int.Parse(m);
        }
        if (nombreCompleto.Length <= 7) 
        {
            ID = 0;
        }
    }

}
