using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgSound;

    public static float bgVolume = 1;

    void Awake()
    {
        if (GameObject.Find("HomeSoundManager"))
            DontDestroyOnLoad(GameObject.Find("HomeSoundManager"));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void BackgroundSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = bgVolume;
        bgSound.Play();
    }

    public void EffectSoundPlay(string soundName, AudioClip clip)
    {
        GameObject sound = new GameObject(soundName + "Sound");
        AudioSource audioSource = sound.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = bgVolume;
        audioSource.PlayOneShot(audioSource.clip);
        Destroy(sound, clip.length);
    }

    public void SetMusicVolume(float volume)
    {
        bgSound.volume = volume;
        bgVolume = bgSound.volume;
        PlayerPrefs.SetFloat("Volume", bgVolume);
    }
}
