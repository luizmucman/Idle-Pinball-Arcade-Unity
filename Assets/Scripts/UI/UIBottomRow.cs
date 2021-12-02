using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBottomRow : MonoBehaviour
{
    public List<UIMenuButton> menuButtons;
    public List<UIWindow> windows;

    public Sprite menuButtonUnselected;
    public Sprite menuButtonSelected;

    public void ResetButtonState() 
    {
        PlayerManager.instance.playerMachineData.currMachine.DisableUpgradeWindow();
        UIManager.instance.uiMenuManager.CloseSettingsWindow();
        UIManager.instance.HideOverlay();
        foreach(UIWindow window in windows)
        {
            if(window.gameObject.activeSelf)
            {
                window.CloseAnim();
            }
        }

        foreach(UIMenuButton menuButton in menuButtons)
        {
            menuButton.GetComponent<Image>().sprite = menuButton.notSelected;
            menuButton.isClicked = false;
        }
    }

    public void ButtonClicked(UIMenuButton button)
    {
        if(!button.isClicked)
        {
            UIManager.instance.ResetWindows();
            ResetButtonState();

            UIManager.instance.ShowOverlay();
            windows[button.windowID].OpenAnim();
            button.isClicked = true;
            button.GetComponent<Image>().sprite = button.isSelected;
        }
        else
        {
            ResetButtonState();
        }
    }
}
