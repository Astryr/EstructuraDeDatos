using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// Como base usamos Dijkstra [cite: 28]
public class Pathfinder
{
    public List<Node> FindPath(Tile[,] grid, Node startNode, Node endNode)
    {
        // El diccionario "table" guarda para cada nodo, su nodo previo y el costo acumulado [cite: 46, 101]
        Dictionary<Node, (Node previous, float cost)> table = new Dictionary<Node, (Node, float)>();
        List<Node> unvisited = new List<Node>();

        // 1. Asignar valores iniciales [cite: 49]
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                // Una pared es un nodo no transitable [cite: 23]
                if (grid[x, y].type != TileType.Wall)
                {
                    Node node = new Node(x, y);
                    // Para los demás nodos, la distancia es infinita y el previo es null [cite: 56]
                    table[node] = (null, float.PositiveInfinity);
                    unvisited.Add(node);
                }
            }
        }

        // Si el nodo de inicio no está en la tabla (es una pared), no hay camino.
        if (!table.ContainsKey(startNode)) return null;

        // Para el nodo inicial, su distancia es 0 [cite: 56]
        table[startNode] = (null, 0);

        while (unvisited.Count > 0)
        {
            // 2. Seleccionar el nodo no visitado más cercano al origen [cite: 50]
            unvisited.Sort((a, b) => table[a].cost.CompareTo(table[b].cost));
            Node currentNode = unvisited[0];

            // Si llegamos al final, reconstruimos el camino
            if (currentNode.Equals(endNode))
            {
                return ReconstructPath(table, endNode);
            }

            // 4. Marcar el nodo como visitado (removiéndolo de la lista de no visitados) [cite: 52, 67]
            unvisited.Remove(currentNode);

            // 3. Actualizar valores de los vecinos [cite: 51]
            foreach (Node neighbor in GetNeighbors(currentNode, grid))
            {
                if (unvisited.Contains(neighbor))
                {
                    // Sumamos el costo total del nodo actual + el costo al vecino [cite: 63]
                    float newCost = table[currentNode].cost + 1; // Costo uniforme de 1 entre tiles adyacentes

                    // Si la suma es menor al costo total del vecino, lo actualizamos [cite: 64]
                    if (newCost < table[neighbor].cost)
                    {
                        table[neighbor] = (currentNode, newCost);
                    }
                }
            }
        }

        return null; // No se encontró un camino
    }

    private List<Node> GetNeighbors(Node node, Tile[,] grid)
    {
        List<Node> neighbors = new List<Node>();
        int[] dx = { 0, 0, 1, -1 }; // Movimiento en x (derecha, izquierda)
        int[] dy = { 1, -1, 0, 0 }; // Movimiento en y (arriba, abajo)

        for (int i = 0; i < 4; i++)
        {
            int newX = node.X + dx[i];
            int newY = node.Y + dy[i];

            if (newX >= 0 && newX < grid.GetLength(0) && newY >= 0 && newY < grid.GetLength(1) && grid[newX, newY].type != TileType.Wall)
            {
                neighbors.Add(new Node(newX, newY));
            }
        }
        return neighbors;
    }

    // Una vez que termina el proceso, podemos reconstruir los caminos [cite: 75]
    private List<Node> ReconstructPath(Dictionary<Node, (Node previous, float cost)> table, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode = table[currentNode].previous; // Vamos al previo, luego al previo del previo, etc. [cite: 76]
        }
        path.Reverse(); // Invertimos para tener el camino desde el inicio hasta el fin
        return path;
    }
}
