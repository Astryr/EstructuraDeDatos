using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanetNode : MonoBehaviour
{
    public string planetName;
    public Button selectButton;
    public TextMeshProUGUI nameText;

    private bool isSelected = false;

    private void Start()
    {
        selectButton.onClick.AddListener(() => PlanetSelector.Instance.SelectPlanet(this));
        UpdateVisual();
    }

    public void MarkSelected()
    {
        isSelected = true;
        UpdateVisual();
    }

    public void Unmark()
    {
        isSelected = false;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (nameText != null)
        {
            nameText.fontStyle = isSelected ? FontStyles.Bold : FontStyles.Normal;
        }
    }
}

