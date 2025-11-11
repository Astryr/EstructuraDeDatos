using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// NODO
public class MyAVLNode : MyABBNode
{
    public int Height;

    public MyAVLNode(int value) : base(value)
    {
        Height = 1; // Un nodo nuevo tiene altura 1
    }
}

// ARBOL
public class MyAVLTree : MyABBTree
{
    public new MyAVLNode Root { get; private set; }

    public MyAVLTree(GameObject prefab) : base(prefab) { }

    // INSERT
    public void InsertAVL(int value)
    {
        Root = InsertRecursiveAVL(Root, value);
    }

    private MyAVLNode InsertRecursiveAVL(MyAVLNode node, int value)
    {
        if (node == null)
        {
            MyAVLNode newNode = new MyAVLNode(value);
            CreateVisualNode(newNode);
            return newNode;
        }

        if (value < node.Value)
            node.Left = InsertRecursiveAVL((MyAVLNode)node.Left, value);
        else if (value > node.Value)
            node.Right = InsertRecursiveAVL((MyAVLNode)node.Right, value);
        else
            return node; // NO DUPLICADOS

        // ALTURA
        node.Height = 1 + Math.Max(GetHeight((MyAVLNode)node.Left), GetHeight((MyAVLNode)node.Right));

        // BALANCE
        int balance = GetBalanceFactor(node);

        
        // ROTA DERECHA
        if (balance > 1 && value < node.Left.Value)
            return RightRotate(node);

        // ROTA IZQUIERDA
        if (balance < -1 && value > node.Right.Value)
            return LeftRotate(node);

        // ROTA IZQ - DER
        if (balance > 1 && value > node.Left.Value)
        {
            node.Left = LeftRotate((MyAVLNode)node.Left);
            return RightRotate(node);
        }

        // ROTA DER - IZQ
        if (balance < -1 && value < node.Right.Value)
        {
            node.Right = RightRotate((MyAVLNode)node.Right);
            return LeftRotate(node);
        }

        return node;
    }


    private MyAVLNode RightRotate(MyAVLNode y)
    {
        MyAVLNode x = (MyAVLNode)y.Left;
        MyAVLNode T2 = (MyAVLNode)x.Right;

        // ROTACION
        x.Right = y;
        y.Left = T2;

        // ALTURAS
        y.Height = 1 + Math.Max(GetHeight((MyAVLNode)y.Left), GetHeight((MyAVLNode)y.Right));
        x.Height = 1 + Math.Max(GetHeight((MyAVLNode)x.Left), GetHeight((MyAVLNode)x.Right));

        return x;
    }

    private MyAVLNode LeftRotate(MyAVLNode x)
    {
        MyAVLNode y = (MyAVLNode)x.Right;
        MyAVLNode T2 = (MyAVLNode)y.Left;

        // ROTACION
        y.Left = x;
        x.Right = T2;

        // ALTURAS
        x.Height = 1 + Math.Max(GetHeight((MyAVLNode)x.Left), GetHeight((MyAVLNode)x.Right));
        y.Height = 1 + Math.Max(GetHeight((MyAVLNode)y.Left), GetHeight((MyAVLNode)y.Right));

        return y;
    }

   
    private int GetHeight(MyAVLNode node)
    {
        return node != null ? node.Height : 0;
    }

    private int GetBalanceFactor(MyAVLNode node)
    {
        if (node == null) return 0;
        return GetHeight((MyAVLNode)node.Left) - GetHeight((MyAVLNode)node.Right);
    }

    // INORDER
    public void InOrderAVL(MyAVLNode node)
    {
        if (node == null) return;
        InOrderAVL((MyAVLNode)node.Left);
        Debug.Log("InOrderAVL: " + node.Value);
        InOrderAVL((MyAVLNode)node.Right);
    }
}
