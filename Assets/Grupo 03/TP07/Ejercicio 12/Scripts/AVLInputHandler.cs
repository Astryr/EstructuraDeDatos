using UnityEngine;
using TMPro;

public class AVLInputHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    public AVLVisualizer visualizer; // Cambiado: Referencia al visualizador

    public void OnAddNodeButton()
    {
        if (int.TryParse(inputField.text, out int value))
        {
            visualizer.Insert(value); // Llama al método Insert del visualizador
            inputField.text = "";
        }
    }
}
