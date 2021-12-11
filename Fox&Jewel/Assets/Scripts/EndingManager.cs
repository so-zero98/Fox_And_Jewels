using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public SoundManager soundManager;
    public Transform EndingUI;
    public AudioClip bgSound;

    // Start is called before the first frame update
    void Start()
    {
        soundManager.BackgroundSoundPlay(bgSound);
        EndingUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
