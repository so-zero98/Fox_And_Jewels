using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    public Transform titleUI;
    public Transform howToPlayUI;
    public Transform settingUI;
    public Transform resetSureUI;
    public Transform resetCompleteUI;
    public Text titleText;
    public Text volumeSize;
    public Slider volumeSlider;
    public bool isClicked;
    public SoundManager soundManager;

    void Awake()
    {
        SoundManager.bgVolume = PlayerPrefs.GetFloat("Volume");
        StageData.clearStage = PlayerPrefs.GetInt("StageNumber");
        PlayerPrefs.SetFloat("Volume", SoundManager.bgVolume);
        Destroy(GameObject.Find("SoundManager"));
        soundManager.SetMusicVolume(SoundManager.bgVolume);
    }

    void Start()
    {
        titleUI.gameObject.SetActive(true);
        howToPlayUI.gameObject.SetActive(false);
        settingUI.gameObject.SetActive(false);
        resetSureUI.gameObject.SetActive(false);
        resetCompleteUI.gameObject.SetActive(false);
    }

    void Update()
    {
        volumeSize.text = ((int)(SoundManager.bgVolume * 100)).ToString();
    }

    public void OpenSettingUI()
    {
        titleUI.gameObject.SetActive(false);
        settingUI.gameObject.SetActive(true);
        volumeSlider.value = SoundManager.bgVolume;
    }

    public void CloseSettingUI()
    {
        settingUI.gameObject.SetActive(false);
        titleText.gameObject.SetActive(true);
        titleUI.gameObject.SetActive(true);
    }

    public void OpenResetSureUI()
    {
        resetSureUI.gameObject.SetActive(true);
    }

    public void CloseResetSureUI()
    {
        resetSureUI.gameObject.SetActive(false);
    }

    public void OpenResetCompleteUI()
    {
        resetSureUI.gameObject.SetActive(false);
        resetCompleteUI.gameObject.SetActive(true);
        StartCoroutine(Disabled(1.5f));
        StageData.ClearStage(0);
        PlayerPrefs.SetInt("StageNumber", StageData.clearStage);
    }

    IEnumerator Disabled(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        resetCompleteUI.gameObject.SetActive(false);
    }
}

