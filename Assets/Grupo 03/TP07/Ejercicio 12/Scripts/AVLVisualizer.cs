using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVLVisualizer : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject nodePrefab;
    public Material lineMaterial; 

    [Header("Configuración del Árbol")]
    public float yOffset = -2.0f;
    public float xMin = -15f;
    public float xMax = 15f;

    // HEREDA MyABBTree
    private MyAVLTree tree;
    private List<GameObject> lineObjects = new List<GameObject>();

    void Start()
    {
        
        tree = new MyAVLTree(nodePrefab);
    }

    
    public void Insert(int value)
    {
        tree.InsertAVL(value);
        UpdateVisualTree();
    }

    
    public void InsertTestValues(int[] values)
    {
        foreach (int v in values)
        {
            tree.InsertAVL(v);
        }
        UpdateVisualTree();
    }

    private void UpdateVisualTree()
    {
        
        foreach (GameObject lineObj in lineObjects)
        {
            Destroy(lineObj);
        }
        lineObjects.Clear();

        
        if (tree.Root != null)
        {
            PositionNodeRecursive(tree.Root, xMin, xMax, 0);
            DrawLinesRecursive(tree.Root);
        }
    }

    
    private void PositionNodeRecursive(MyAVLNode node, float min, float max, float y)
    {
        if (node == null) return;

        float x = (min + max) / 2f;

        
        if (tree.nodeVisuals.ContainsKey(node))
        {
            tree.nodeVisuals[node].transform.position = new Vector2(x, y);
        }

        PositionNodeRecursive((MyAVLNode)node.Left, min, x, y + yOffset);
        PositionNodeRecursive((MyAVLNode)node.Right, x, max, y + yOffset);
    }

    // DIBUJAR LINEAS
    private void DrawLinesRecursive(MyAVLNode node)
    {
        if (node == null) return;

        if (node.Left != null)
            CreateLine(node, (MyAVLNode)node.Left);
        if (node.Right != null)
            CreateLine(node, (MyAVLNode)node.Right);

        DrawLinesRecursive((MyAVLNode)node.Left);
        DrawLinesRecursive((MyAVLNode)node.Right);
    }

    private void CreateLine(MyAVLNode parent, MyAVLNode child)
    {
        GameObject lineObj = new GameObject("Line_" + parent.Value + "_" + child.Value);
        lineObj.transform.SetParent(this.transform);
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();

        lr.positionCount = 2;
        lr.SetPosition(0, tree.nodeVisuals[parent].transform.position);
        lr.SetPosition(1, tree.nodeVisuals[child].transform.position);

        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.material = lineMaterial; 
        lr.startColor = Color.white;
        lr.endColor = Color.white;

        lineObjects.Add(lineObj);
    }
}
