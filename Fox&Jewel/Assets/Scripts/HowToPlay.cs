using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    public Transform howToPlayUI;
    public Transform titleUI;
    public Text titleText;

    public void HowToPlayGame()
    {
        titleText.gameObject.SetActive(true);
        titleUI.gameObject.SetActive(false);
        howToPlayUI.gameObject.SetActive(true);
    }

    public void CloseHowToPlayGame()
    {
        howToPlayUI.gameObject.SetActive(false);
        titleUI.gameObject.SetActive(true);
        titleText.gameObject.SetActive(true);
    }
}
