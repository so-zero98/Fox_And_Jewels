using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SelectStage()
    {
        SceneManager.LoadScene("SelectStage");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToHome()
    {
        Time.timeScale = 1f;
        Destroy(GameObject.Find("HomeSoundManager"));
        SceneManager.LoadScene("Home");
    }
}
