using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBottomRow : MonoBehaviour
{
    public List<UIMenuButton> menuButtons;
    public List<GameObject> windows;

    public Sprite menuButtonUnselected;
    public Sprite menuButtonSelected;

    public void ResetButtonState() 
    {
        PlayerManager.instance.currentMachine.DisableUpgradeWindow();
        UIManager.instance.HideOverlay();
        foreach(GameObject window in windows)
        {
            window.SetActive(false);
        }

        foreach(UIMenuButton menuButton in menuButtons)
        {
            menuButton.GetComponent<Image>().sprite = menuButtonUnselected;
            menuButton.isClicked = false;
        }
    }

    public void ButtonClicked(UIMenuButton button)
    {
        if(!button.isClicked)
        {
            ResetButtonState();
            UIManager.instance.ShowOverlay();
            windows[button.windowID].SetActive(true);
            button.isClicked = true;
            button.GetComponent<Image>().sprite = menuButtonSelected;
        }
        else
        {
            ResetButtonState();
        }
    }
}
