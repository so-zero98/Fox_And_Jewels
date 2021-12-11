using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
    public Text loadingText;
    float time;
    int num;

    void Update()
    {
        time += Time.deltaTime;

        num = (int)time % 4;

        switch (num)
        {
            case 0:
                loadingText.text = "Now Loading";
                break;
            case 1:
                loadingText.text = "Now Loading.";
                break;
            case 2:
                loadingText.text = "Now Loading..";
                break;
            case 3:
                loadingText.text = "Now Loading...";
                break;
        }
    }
}
