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
    }

}
