using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyLinkedList;

public class TesterMyList : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI displayText;
    private MyList<string> myList;

    private void Start()
    {
        myList = new MyList<string>();
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

    public void RemoveAtIndex()
    {
        if (int.TryParse(inputField.text, out int index))
        {
            try
            {
                myList.RemoveAt(index);
            }
            catch
            {
                Debug.Log("Índice inválido.");
            }
            inputField.text = "";
            UpdateDisplay();
        }
    }

    public void InsertAtIndex()
    {
        string[] parts = inputField.text.Split(',');
        if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int index))
        {
            string value = parts[1].Trim();
            try
            {
                myList.Insert(index, value);
            }
            catch
            {
                Debug.Log("Índice inválido.");
            }
            inputField.text = "";
            UpdateDisplay();
        }
    }

    public void ClearList()
    {
        myList.Clear();
        UpdateDisplay();
    }

    public void SortBubble()
    {
        myList.BubbleSort();
        UpdateDisplay();
    }

    public void SortSelection()
    {
        myList.SelectionSort();
        UpdateDisplay();
    }

    public void SortInsertion()
    {
        myList.InsertionSort();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        displayText.text = $"Lista ({myList.Count}): {myList}";
    }
}
