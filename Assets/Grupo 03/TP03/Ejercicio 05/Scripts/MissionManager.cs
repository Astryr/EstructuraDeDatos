using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionManager : MonoBehaviour
{
    public Text missionText;
    public Button completeButton;
    public Text statusText;

    private MyQueue<string> missions = new MyQueue<string>();

    void Start()
    {
        missions.Enqueue("Recolectar 5 monedas");
        missions.Enqueue("Derrotar al enemigo");
        missions.Enqueue("Llegar al punto de control");

        completeButton.onClick.AddListener(CompleteMission);
        UpdateMissionUI();
    }

    void CompleteMission()
    {
        if (missions.TryDequeue(out string completed))
        {
            Debug.Log($"Misión completada: {completed}");
            UpdateMissionUI();
        }
        else
        {
            statusText.text = "¡Juego completado!";
            missionText.text = "";
            // No se muestra nada en consola si ya no hay misiones
        }
    }

    void UpdateMissionUI()
    {
        if (missions.TryPeek(out string current))
        {
            missionText.text = $"Misión actual: {current}";
            statusText.text = "";
        }
    }
}


