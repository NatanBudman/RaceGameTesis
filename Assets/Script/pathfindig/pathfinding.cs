using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class pathfinding : MonoBehaviour
{
    
    public List<Node> Path(Node from , Node to)
    {
        List<Node> visited = new List<Node>();
        List<Node> parent = new List<Node>();
        List<Node> path =new List<Node>();
        List<Node> currentList = new List<Node>();
        parent.Add(from);
        Node currentNode = from;
        
        int maxIterations = 0;
        while (currentNode.ID != to.ID && maxIterations < 2500)
        {
            maxIterations++;

            var result = parent.Except(visited);
            
            foreach (Node parentNode in result)
            {
                currentNode = parentNode;
                visited.Add(parentNode);
            }

            for (int i = 0; i < currentNode.NeighboarNodes.Length; i++)
            {
                Node node = currentNode.NeighboarNodes[i];
                
                if (node.ID == to.ID)
                {
                    currentNode = node;
                    break;
                }
                else
                {
                    currentList.Add(node);
                }
            }

            if (!result.Any())
            {
                visited.Add(currentNode);
                parent.Clear();
                parent = currentList;
                currentList.Clear();
            }
           

        }
      
        maxIterations = 0;
        Node scurrentNode = to;
        while (scurrentNode.ID != from.ID && maxIterations < 1000)
        {
            maxIterations++;
            
            foreach (var nodeVisited in visited)
            {
                if (nodeVisited.NeighboarNodes.Contains(scurrentNode))
                {
                    path.Add(nodeVisited);
                    scurrentNode = nodeVisited;
                  
                }
            }
        }
        path.Reverse();
        path.Add(to);
        
        return path;
    }
}
