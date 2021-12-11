using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Button stageOneButton;
    public Button stageTwoButton;
    public Button stageThreeButton;
    public Text stageOneText;
    public Text stageTwoText;
    public Text stageThreeText;
    
    void Start()
    {
        ColorSetting();
    }

    void ColorSetting()
    {
        Color disabledButtonColor = new Color(12 / 255f, 51 / 255f, 0 / 255f);
        Color disabledTextColor = new Color(50 / 255f, 50 / 255f, 50 / 255f);

        Color enabledButtonColor = new Color(60 / 255f, 255 / 255f, 0 / 255f);
        Color enabledTextColor = new Color(255 / 255f, 255 / 255f, 255 / 255f);

        switch (StageData.clearStage)
        {
            case 0:
            case 1:
                stageOneText.color = enabledTextColor;
                stageOneButton.interactable = true;
                stageTwoText.color = disabledTextColor;
                stageTwoButton.interactable = false;
                stageThreeText.color = disabledTextColor;
                stageThreeButton.interactable = false;
                break;
            case 2:
                stageOneText.color = enabledTextColor;
                stageOneButton.interactable = true;
                stageTwoText.color = enabledTextColor;
                stageTwoButton.interactable = true;
                stageThreeText.color = disabledTextColor;
                stageThreeButton.interactable = false;
                break;
            case 3:
                stageOneText.color = enabledTextColor;
                stageOneButton.interactable = true;
                stageTwoText.color = enabledTextColor;
                stageTwoButton.interactable = true;
                stageThreeText.color = enabledTextColor;
                stageThreeButton.interactable = true;
                break;
        }
    }
}
