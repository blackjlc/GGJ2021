using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBubble : MonoBehaviour
{
    public Vector2 padding;
    private SpriteRenderer backgroundSprite;
    private TextMeshPro textContent;
    private bool showing;

    public void Setup(string text) {
        if (!showing) {
            showing = true;
            textContent.SetText(text);
            textContent.ForceMeshUpdate();
            Vector2 textSize = textContent.GetRenderedValues(false);
            textSize.x *= .3f;
            backgroundSprite.size = textSize + padding;
            StartCoroutine(PopText(text));
        }
    }

    IEnumerator PopText(string text) {
        string stringBuffer = "";
        foreach (char letter in text.ToCharArray()) {
            stringBuffer += letter;
            textContent.SetText(stringBuffer);
            yield return new WaitForSeconds(0.07f);
        }
    }

    void Awake()
    {
        backgroundSprite = transform.Find("Background").GetComponent<SpriteRenderer>();
        textContent = transform.Find("Text").GetComponent<TextMeshPro>();
        showing = false;
    }
}
