using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITopRow : MonoBehaviour
{
    [SerializeField] private List<UIWindow> topRowWindows;

    public void ResetWindows()
    {
        foreach(UIWindow window in topRowWindows)
        {
            if(window.gameObject.activeInHierarchy)
            {
                window.CloseAnim();
            }
        }
    }
}
