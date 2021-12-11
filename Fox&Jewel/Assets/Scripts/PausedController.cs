using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedController : MonoBehaviour
{
    public SoundManager soundManager;
    public GameObject pauseMenu;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                soundManager.bgSound.Pause();
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
            }
            else
            {
                soundManager.bgSound.Play();
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
            }
        }
    }
}
