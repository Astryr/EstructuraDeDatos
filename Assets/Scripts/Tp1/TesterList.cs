using TMPro;
using UnityEngine;

public class TesterList : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI displayText;
    private SimpleList<string> myList;

    private void Start()
    {
        myList = new SimpleList<string>();
        UpdateDisplay();
    }

    public void AddItem()
    {
        if (!string.IsNullOrWhiteSpace(inputField.text))
        {
            myList.Add(inputField.text);
            inputField.text = "";
            UpdateDisplay();
        }
    }

    public void AddRangeItems()
    {
        if (!string.IsNullOrWhiteSpace(inputField.text))
        {
            string[] parts = inputField.text.Split(',');
            for (int i = 0; i < parts.Length; i++)
                parts[i] = parts[i].Trim();

            myList.AddRange(parts);
            inputField.text = "";
            UpdateDisplay();
        }
    }

    public void RemoveItem()
    {
        if (!string.IsNullOrWhiteSpace(inputField.text))
        {
            bool removed = myList.Remove(inputField.text);
            if (!removed)
                Debug.Log("Elemento no encontrado en la lista.");

            inputField.text = "";
            UpdateDisplay();
        }
    }

    public void ClearList()
    {
        myList.Clear();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        displayText.text = $"Lista ({myList.Count}): {myList}";
    }
}
