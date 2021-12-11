using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public GameManager manager;
    public SoundManager soundManager;
    [SerializeField] Transform totalGemCheck = null;
    public string[] sentences;
    public Transform ChatBubblePoint;
    public GameObject ChatBubble;
    public Animator animator;
    public Transform stageClearUI;

    public AudioClip bgSound;
    
    int totalGems;
    int stageClear;

    // Start is called before the first frame update
    void Start()
    {
        stageClearUI.gameObject.SetActive(false);
        totalGems = totalGemCheck.childCount;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (manager.gem == totalGems)
                {
                    if (SceneManager.GetActiveScene().buildIndex == 2)
                        StageData.ClearStage(2);
                    else if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4)
                        StageData.ClearStage(3);
                    manager.SaveGame();
                    soundManager.BackgroundSoundPlay(bgSound);
                    stageClearUI.gameObject.SetActive(true);
                }
                else
                    DoorChat();
            }
        }
    }

    public void DoorChat()
    {
        GameObject door = Instantiate(ChatBubble);
        door.GetComponent<DoorSentencesSetting>().OnDialogue(sentences, ChatBubblePoint);
    }
}
