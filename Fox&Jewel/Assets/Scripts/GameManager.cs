using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SoundManager soundManager;
    public Transform stageStart;
    public Transform gameOver;
    public Transform pause;
    public Transform setting;
    public Text totalGems;
    public Text gameOverTotalGems;
    public Text nowGems;
    public Text volumeSize;
    public Slider volumeSlider;
    public Image[] cherryImages;
    public int gem;
    public int life = 3;
    public int stage = 1;
    public PlayerController player;
    public Text resultGetGems;
    [SerializeField] Transform totalGemCheck = null;
    public bool isGameOver = false;
    public Animator animator;

    public AudioClip bgSound;
    public AudioClip gameOverSound;

    private void Awake()
    {
        Destroy(GameObject.Find("HomeSoundManager"));
        volumeSlider.value = SoundManager.bgVolume;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadGame();
        soundManager.BackgroundSoundPlay(bgSound);
        stageStart.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);
        setting.gameObject.SetActive(false);
        totalGems.text = totalGemCheck.childCount.ToString();
        gameOverTotalGems.text = totalGemCheck.childCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        nowGems.text = gem.ToString();
        volumeSize.text = ((int)(volumeSlider.value * 100)).ToString();
        soundManager.SetMusicVolume(volumeSlider.value);
    }

    public void PlayerDamaged() // 플레이어 피격 시
    {
        if (life > 0)   // 라이프 감소
        {
            life--;
            cherryImages[life].color = new Color(0, 0, 0);
        }
        else
        {
            animator.SetBool("isDead", true);
            player.gameObject.layer = 15;
            isGameOver = true;
            player.circleCollider.enabled = false;
            gameOver.gameObject.SetActive(true);
            resultGetGems.text = gem.ToString();
            StartCoroutine(Disabled(3.5f));
            soundManager.bgSound.Stop();
            soundManager.BackgroundSoundPlay(gameOverSound);
        }
    }

    public void GetLife()
    {
        cherryImages[life].color = new Color(1, 1, 1);
        life++;
    }

    IEnumerator Disabled(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        player.gameObject.SetActive(false);
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("StageNumber", StageData.clearStage);
        PlayerPrefs.SetFloat("Volume", SoundManager.bgVolume);
    }

    public void LoadGame()
    {
        StageData.clearStage = PlayerPrefs.GetInt("StageNumber");
        SoundManager.bgVolume = PlayerPrefs.GetFloat("Volume");
    }

    public void OpenSettingUI()
    {
        pause.gameObject.SetActive(false);
        setting.gameObject.SetActive(true);
        volumeSlider.value = SoundManager.bgVolume;
    }

    public void CloseSettingUI()
    {
        setting.gameObject.SetActive(false);
        pause.gameObject.SetActive(true);
    }
}
