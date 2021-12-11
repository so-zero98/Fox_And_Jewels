using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorSentencesSetting : MonoBehaviour
{
    public Queue<string> sentences;
    public string currentSentence;
    public TextMeshPro text;
    public GameObject backGroundImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDialogue(string[] lines, Transform chatPoint)
    {
        transform.position = chatPoint.position;
        sentences = new Queue<string>();
        sentences.Clear();
        foreach (var line in lines)
        {
            sentences.Enqueue(line);
        }
        StartCoroutine(DialogueFlow(chatPoint));
    }

    IEnumerator DialogueFlow(Transform chatPoint)
    {
        yield return null;
        while(sentences.Count>0)
        {
            currentSentence = sentences.Dequeue();
            text.text = currentSentence;
            float x = text.preferredWidth;
            x = (x >= 4) ? 4 : x + 0.1f;
            backGroundImage.transform.localScale = new Vector2(x, text.preferredHeight + 0.3f);
            transform.position = new Vector2(chatPoint.position.x, chatPoint.position.y+text.preferredHeight/2);
            yield return new WaitForSeconds(3f);
        }
        Destroy(gameObject);
    }
}
