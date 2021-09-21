using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoPaddleButton : MonoBehaviour
{
    public Paddle connectedPaddle;
    private Image buttonImage;

    public Sprite unpressedButton;
    public Sprite pressedButton;

    // Machine Data
    private MachineManager currMachine;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        currMachine = GetComponentInParent<MachineManager>();
    }

    public void AutoPaddleClicked()
    {
        if (connectedPaddle.isAuto)
        {
            connectedPaddle.isAuto = false;
            buttonImage.sprite = unpressedButton;
        }
        else
        {
            connectedPaddle.isAuto = true;
            buttonImage.sprite = pressedButton;
        }
        currMachine.CheckAutoPaddles();
    }
}
