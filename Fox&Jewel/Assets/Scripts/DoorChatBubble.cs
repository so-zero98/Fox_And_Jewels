using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorChatBubble : MonoBehaviour
{
    SpriteRenderer backGroundImage;
    SpriteRenderer gemImage;
    TextMeshPro textMeshPro;

    // Start is called before the first frame update
    void Awake()
    {
        backGroundImage = transform.Find("BackGroundImage").GetComponent<SpriteRenderer>();
        gemImage = transform.Find("GemImage").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    void Start()
    {
        SetUp(gemImage, "Hello World! Say hello to my little friend!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUp(SpriteRenderer gem, string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);

        Vector2 padding = new Vector2(-7f, .3f);
        backGroundImage.size = textSize + padding;

        Vector3 offset = new Vector2(-3f, 0f);
        backGroundImage.transform.localPosition = new Vector3(backGroundImage.size.x / 2f, 0f) + offset;
    }
}
