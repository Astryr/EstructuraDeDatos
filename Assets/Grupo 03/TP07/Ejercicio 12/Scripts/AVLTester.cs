using UnityEngine;

public class AVLTester : MonoBehaviour
{
    
    public AVLVisualizer visualizer;

    void Start()
    {
       

        // NODO DE PRUEBA (CONSIGNA)
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