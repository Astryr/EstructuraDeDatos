using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int X { get; }
    public int Y { get; }

    public Node(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object obj)
    {
        return obj is Node other && X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return (X, Y).GetHashCode();
    }
}
