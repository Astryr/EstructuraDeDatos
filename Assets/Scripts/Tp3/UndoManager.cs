using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoManager : MonoBehaviour
{
    public Transform player;
    public Button moveButton;
    public Button undoButton;
    public Text positionText;

    private MyStack<Vector3> positions = new MyStack<Vector3>();

    void Start()
    {
        moveButton.onClick.AddListener(MovePlayer);
        undoButton.onClick.AddListener(UndoMove);
        SavePosition();
    }

    void MovePlayer()
    {
        SavePosition();
        player.position += new Vector3(1, 0, 0);
        Debug.Log($"Jugador movido a: {player.position}");
        UpdateUI();
    }

    void UndoMove()
    {
        if (positions.TryPop(out Vector3 lastPos))
        {
            player.position = lastPos;
            Debug.Log($"Movimiento deshecho. Nueva posición: {player.position}");
            UpdateUI();
        }
        // No se muestra nada en consola si no hay movimientos para deshacer
    }

    void SavePosition()
    {
        positions.Push(player.position);
    }

    void UpdateUI()
    {
        positionText.text = $"Posición: {player.position}";
    }
}


