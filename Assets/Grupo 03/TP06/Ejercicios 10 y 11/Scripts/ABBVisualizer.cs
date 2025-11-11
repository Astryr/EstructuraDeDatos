using UnityEngine;
using TMPro; 
using System.Collections.Generic;

public class ABBVisualizer : MonoBehaviour
{
    public GameObject nodePrefab;

    private MyABBTree tree;

    void Start()
    {
        // CREA ABB
        tree = new MyABBTree(nodePrefab);

        int[] myArray = { 20, 10, 1, 26, 35, 40, 18, 12, 15, 14, 30, 23 };
        foreach (int v in myArray)
            tree.Insert(v);

        // NODOS
        float xMin = -10f;
        float xMax = 10f;
        float yStart = 4f;
        PositionNode(tree.Root, xMin, xMax, yStart);

        // LINEAS
        DrawLines(tree.Root);

        // MUESTRA CONSOLA
        Debug.Log("Altura del árbol: " + tree.GetHeight(tree.Root));
        Debug.Log("Factor de balance (raíz): " + tree.GetBalanceFactor(tree.Root));

        tree.InOrder(tree.Root);
        tree.PreOrder(tree.Root);
        tree.PostOrder(tree.Root);
        tree.LevelOrder(tree.Root);
    }

    // POSICION ABB
    private void PositionNode(MyABBNode node, float xMin, float xMax, float y)
    {
        if (node == null) return;

        float x = (xMin + xMax) / 2f;

        if (tree.nodeVisuals.ContainsKey(node))
            tree.nodeVisuals[node].transform.position = new Vector2(x, y);

        float yOffset = -1.5f;

        PositionNode(node.Left, xMin, x, y + yOffset);
        PositionNode(node.Right, x, xMax, y + yOffset);
    }

    // DIBUJA LINEAS
    private void DrawLines(MyABBNode node)
    {
        if (node == null) return;

        if (node.Left != null) CreateLine(node, node.Left);
        if (node.Right != null) CreateLine(node, node.Right);

        DrawLines(node.Left);
        DrawLines(node.Right);
    }

    private void CreateLine(MyABBNode parent, MyABBNode child)
    {
        GameObject lineObj = new GameObject("Line_" + parent.Value + "_" + child.Value);
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, tree.nodeVisuals[parent].transform.position);
        lr.SetPosition(1, tree.nodeVisuals[child].transform.position);
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.white;
        lr.endColor = Color.white;
    }
}
