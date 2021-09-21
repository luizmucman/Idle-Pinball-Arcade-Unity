using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuButton : MonoBehaviour
{
    private UIBottomRow rowManager;

    public int windowID;
    public bool isClicked;

    private void Start()
    {
        rowManager = GetComponentInParent<UIBottomRow>();
    }

    public virtual void ClickWindow()
    {
        rowManager.ButtonClicked(this);
    }
}
