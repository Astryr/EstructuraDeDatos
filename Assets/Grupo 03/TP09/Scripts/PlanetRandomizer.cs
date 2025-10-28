using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlanetRandomizer : MonoBehaviour
{
    public RectTransform canvasRect;
    public PlanetNode[] planets;
    public RectTransform resultTextRect;
    public RectTransform checkPathButtonRect;

    private List<Vector2> usedPositions = new();
    private float minDistanceBetweenPlanets = 100f;

    void Start()
    {
        foreach (PlanetNode planet in planets)
        {
            Vector2 randomPos = GetSafeRandomPosition();
            planet.GetComponent<RectTransform>().anchoredPosition = randomPos;
            usedPositions.Add(randomPos);
        }
    }

    Vector2 GetSafeRandomPosition()
    {
        float padding = 50f;
        Vector2 pos;
        int attempts = 0;

        Rect safeZone = GetCombinedSafeZone();

        do
        {
            float x = Random.Range(padding, canvasRect.rect.width - padding);
            float y = Random.Range(padding, canvasRect.rect.height - padding);
            pos = new Vector2(x - canvasRect.rect.width / 2f, y - canvasRect.rect.height / 2f);
            attempts++;
        }
        while ((!IsFarEnough(pos) || IsInsideSafeZone(pos, safeZone)) && attempts < 100);

        return pos;
    }

    bool IsFarEnough(Vector2 newPos)
    {
        foreach (Vector2 existing in usedPositions)
        {
            if (Vector2.Distance(newPos, existing) < minDistanceBetweenPlanets)
                return false;
        }
        return true;
    }

    bool IsInsideSafeZone(Vector2 pos, Rect safeZone)
    {
        return safeZone.Contains(pos);
    }

    Rect GetCombinedSafeZone()
    {
        Vector2 textPos = resultTextRect.anchoredPosition;
        Vector2 textSize = resultTextRect.rect.size;

        Vector2 buttonPos = checkPathButtonRect.anchoredPosition;
        Vector2 buttonSize = checkPathButtonRect.rect.size;

        float minX = Mathf.Min(textPos.x - textSize.x / 2f, buttonPos.x - buttonSize.x / 2f);
        float maxX = Mathf.Max(textPos.x + textSize.x / 2f, buttonPos.x + buttonSize.x / 2f);
        float minY = Mathf.Min(textPos.y - textSize.y / 2f, buttonPos.y - buttonSize.y / 2f);
        float maxY = Mathf.Max(textPos.y + textSize.y / 2f, buttonPos.y + buttonSize.y / 2f);

        return new Rect(minX, minY, maxX - minX, maxY - minY);
    }
}
