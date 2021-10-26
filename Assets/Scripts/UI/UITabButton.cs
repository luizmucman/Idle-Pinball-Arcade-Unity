using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITabButton : MonoBehaviour
{
    [SerializeField] private UITabManager manager;
    [SerializeField] private GameObject tabContent;
    [SerializeField] private Button tabButton; 

    public void OpenTab()
    {
        manager.ResetTabs();
        tabContent.SetActive(true);
        tabButton.GetComponent<Image>().color = Color.white;
    }

    public void ResetTab()
    {
        tabContent.SetActive(false);
        tabButton.GetComponent<Image>().color = Color.grey;
    }
}
