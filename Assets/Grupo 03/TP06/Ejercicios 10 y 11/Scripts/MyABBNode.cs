using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// NODO
public class MyABBNode
{
    public int Value;
    public MyABBNode Left;
    public MyABBNode Right;

    public MyABBNode(int value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

// ARBOL
public class MyABBTree
{
    public MyABBNode Root { get; private set; }

    private GameObject nodePrefab;
    public Dictionary<MyABBNode, GameObject> nodeVisuals = new Dictionary<MyABBNode, GameObject>();

    public MyABBTree(GameObject prefab)
    {
        nodePrefab = prefab;
    }

    // INSERT
    public void Insert(int value)
    {
        Root = InsertRecursive(Root, value);
    }

    private MyABBNode InsertRecursive(MyABBNode node, int value)
    {
        if (node == null)
        {
            MyABBNode newNode = new MyABBNode(value);
            CreateVisualNode(newNode);
            return newNode;
        }

        if (value < node.Value)
            node.Left = InsertRecursive(node.Left, value);
        else if (value > node.Value)
            node.Right = InsertRecursive(node.Right, value);

        return node;
    }

    
    protected void CreateVisualNode(MyABBNode node)
    {
        if (nodePrefab == null) return;

        GameObject obj = GameObject.Instantiate(nodePrefab);
        obj.name = "Node_" + node.Value;

        TextMeshPro tmp = obj.GetComponentInChildren<TextMeshPro>();
        if (tmp != null) tmp.text = node.Value.ToString();

        nodeVisuals[node] = obj;
    }

    // ALTURA
    public int GetHeight(MyABBNode node)
    {
        if (node == null) return 0;
        return 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
    }

    // BALANCE
    public int GetBalanceFactor(MyABBNode node)
    {
        if (node == null) return 0;
        return GetHeight(node.Left) - GetHeight(node.Right);
    }

    // RECORRIDOS
    public void InOrder(MyABBNode node)
    {
        if (node == null) return;
        InOrder(node.Left);
        Debug.Log("InOrder: " + node.Value);
        InOrder(node.Right);
    }

    public void PreOrder(MyABBNode node)
    {
        if (node == null) return;
        Debug.Log("PreOrder: " + node.Value);
        PreOrder(node.Left);
        PreOrder(node.Right);
    }

    public void PostOrder(MyABBNode node)
    {
        if (node == null) return;
        PostOrder(node.Left);
        PostOrder(node.Right);
        Debug.Log("PostOrder: " + node.Value);
    }

    public void LevelOrder(MyABBNode root)
    {
        if (root == null) return;

        Queue<MyABBNode> queue = new Queue<MyABBNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            MyABBNode current = queue.Dequeue();
            Debug.Log("LevelOrder: " + current.Value);

            if (current.Left != null) queue.Enqueue(current.Left);
            if (current.Right != null) queue.Enqueue(current.Right);
        }
    }
}
