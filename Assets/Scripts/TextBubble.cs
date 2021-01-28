using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBubble : MonoBehaviour
{
    public Vector2 padding;
    private SpriteRenderer backgroundSprite;
    private TextMeshPro textContent;

    public void Setup(string text) {
        textContent.SetText(text);
        textContent.ForceMeshUpdate();
        Vector2 textSize = textContent.GetRenderedValues(false);
        textSize.x *= .3f;
        backgroundSprite.size = textSize + padding;
    }

    void Awake()
    {
        backgroundSprite = transform.Find("Background").GetComponent<SpriteRenderer>();
        textContent = transform.Find("Text").GetComponent<TextMeshPro>();
    }
}
