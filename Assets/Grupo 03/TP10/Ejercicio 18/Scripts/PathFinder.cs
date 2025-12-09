using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class Pathfinder
{
    public List<Node> FindPath(Tile[,] grid, Node startNode, Node endNode)
    { 
        Dictionary<Node, (Node previous, float cost)> table = new Dictionary<Node, (Node, float)>();
        List<Node> unvisited = new List<Node>();

        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                
                if (grid[x, y].type != TileType.Wall)
                {
                    Node node = new Node(x, y);
                    table[node] = (null, float.PositiveInfinity);
                    unvisited.Add(node);
                }
            }
        }

        if (!table.ContainsKey(startNode) || !table.ContainsKey(endNode)) return null;

        table[startNode] = (null, 0);

        while (unvisited.Count > 0)
        {
     
            unvisited.Sort((a, b) => table[a].cost.CompareTo(table[b].cost));
            Node currentNode = unvisited[0];
            unvisited.RemoveAt(0); 

            if (float.IsPositiveInfinity(table[currentNode].cost)) break;

            if (currentNode.Equals(endNode))
            {
                return ReconstructPath(table, endNode);
            }
 
            foreach (Node neighbor in GetNeighbors(currentNode, grid, width, height))
            {
               
                if (unvisited.Contains(neighbor))
                {
                    float newCost = table[currentNode].cost + 1; 
                    if (newCost < table[neighbor].cost)
                    {
                        table[neighbor] = (currentNode, newCost);
                    }
                }
            }
        }

        return null; 
    }

    private List<Node> GetNeighbors(Node node, Tile[,] grid, int width, int height)
    {
        List<Node> neighbors = new List<Node>();
       
        int[] dx = { 0, 0, 1, -1 };
        int[] dy = { 1, -1, 0, 0 };

        for (int i = 0; i < 4; i++)
        {
            int newX = node.X + dx[i];
            int newY = node.Y + dy[i];

            if (newX >= 0 && newX < width && newY >= 0 && newY < height && grid[newX, newY].type != TileType.Wall)
            {
                neighbors.Add(new Node(newX, newY));
            }
        }
        return neighbors;
    }

    private List<Node> ReconstructPath(Dictionary<Node, (Node previous, float cost)> table, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode = table[currentNode].previous;
        }
        path.Reverse(); 
        return path;
    }
}