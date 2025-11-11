using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVLVisualizer : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject nodePrefab;
    public Material lineMaterial; // Material para las líneas

    [Header("Configuración del Árbol")]
    public float yOffset = -2.0f;
    public float xMin = -15f;
    public float xMax = 15f;

    // 1. Usamos la clase TDA correcta que SÍ hereda de MyABBTree
    private MyAVLTree tree;
    private List<GameObject> lineObjects = new List<GameObject>();

    void Start()
    {
        // 2. Creamos la instancia del árbol y le pasamos el prefab
        tree = new MyAVLTree(nodePrefab);
    }

    // Método para ser llamado por la UI (InputHandler)
    public void Insert(int value)
    {
        tree.InsertAVL(value);
        UpdateVisualTree();
    }

    // Método para ser llamado por el Tester
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
        // Limpiar líneas anteriores
        foreach (GameObject lineObj in lineObjects)
        {
            Destroy(lineObj);
        }
        lineObjects.Clear();

        // 3. Llamamos a los métodos de posicionamiento y dibujo
        if (tree.Root != null)
        {
            PositionNodeRecursive(tree.Root, xMin, xMax, 0);
            DrawLinesRecursive(tree.Root);
        }
    }

    // 4. Posiciona los nodos visuales (que están guardados en el Dictionary del árbol base)
    private void PositionNodeRecursive(MyAVLNode node, float min, float max, float y)
    {
        if (node == null) return;

        float x = (min + max) / 2f;

        // El Dictionary 'nodeVisuals' es heredado de MyABBTree
        if (tree.nodeVisuals.ContainsKey(node))
        {
            tree.nodeVisuals[node].transform.position = new Vector2(x, y);
        }

        PositionNodeRecursive((MyAVLNode)node.Left, min, x, y + yOffset);
        PositionNodeRecursive((MyAVLNode)node.Right, x, max, y + yOffset);
    }

    // 5. Dibuja las líneas
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
        lr.material = lineMaterial; // Asignar un material
        lr.startColor = Color.white;
        lr.endColor = Color.white;

        lineObjects.Add(lineObj);
    }
}
