using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainNavButton : MonoBehaviour
{
    [SerializeField] private UIWindow connectedWindow;

    public void BtnClicked()
    {
        UIManager.instance.ResetWindows();
        connectedWindow.OpenAnim();
    }
}
