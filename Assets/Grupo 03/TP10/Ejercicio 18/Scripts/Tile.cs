using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Entrance,
    Exit,
    Floor,
    Wall
}

public class Tile : MonoBehaviour
{
    public int x, y;
    public TileType type;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(int x, int y, TileType type)
    {
        this.x = x;
        this.y = y;
    }

    // Modificado para aceptar un Color
    public void SetType(TileType newType, Color newColor)
    {
        this.type = newType;
        spriteRenderer.color = newColor; // Asigna el color
    }
}