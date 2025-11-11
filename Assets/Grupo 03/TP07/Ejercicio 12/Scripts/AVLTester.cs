using UnityEngine;

public class AVLTester : MonoBehaviour
{
    // 1. La hacemos pública
    public AVLVisualizer visualizer;

    void Start()
    {
        // 2. Ya no buscamos el objeto aquí, se asigna en el Inspector

        // Insertar nodos de prueba
        int[] testValues = { 10, 20, 5, 15, 25 };

        if (visualizer != null)
        {
            visualizer.InsertTestValues(testValues);
        }
        else
        {
            Debug.LogError("Error: No se ha asignado el AVLVisualizer en el Inspector del AVLTester.");
        }
    }
}