using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITabManager : MonoBehaviour
{
    [SerializeField] private List<UITabButton> tabButtons;

    public void ResetTabs()
    {
        foreach(UITabButton currButton in tabButtons)
        {
            currButton.ResetTab();
        }
    }

}
