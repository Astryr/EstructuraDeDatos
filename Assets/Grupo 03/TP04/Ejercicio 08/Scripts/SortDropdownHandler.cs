using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SortDropdownHandler : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public StoreManager storeManager;

    void Start()
    {
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    void OnDropdownValueChanged(int index)
    {
        string criterio = dropdown.options[index].text;
        storeManager.SortItems(criterio);
    }
}

