using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetNode : MonoBehaviour
{
    public string planetName;
    public Button selectButton;

    private void Start()
    {
        selectButton.onClick.AddListener(() => PlanetSelector.Instance.SelectPlanet(this));
    }
}
