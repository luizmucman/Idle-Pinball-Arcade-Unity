using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITabButton : MonoBehaviour
{
    [SerializeField] private UITabManager manager;
    [SerializeField] private GameObject tabContent;
    [SerializeField] private Button tabButton;

    //Button Images
    [SerializeField] private Sprite btnSelectedImage;
    [SerializeField] private Sprite btnUnselectedImage;

    public void OpenTab()
    {
        manager.ResetTabs();
        tabContent.SetActive(true);
        if(btnSelectedImage != null)
        {
            tabButton.GetComponent<Image>().sprite = btnSelectedImage;
        }
        else
        {
            tabButton.GetComponent<Image>().color = Color.white;
        }
    }

    public void ResetTab()
    {
        tabContent.SetActive(false);

        if(btnUnselectedImage != null)
        {
            tabButton.GetComponent<Image>().sprite = btnUnselectedImage;
        }
        else
        {
            tabButton.GetComponent<Image>().color = Color.grey;
        }
    }
}
