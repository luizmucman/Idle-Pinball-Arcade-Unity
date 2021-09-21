using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UILeftPaddleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private PlayerManager playerManager;

    public bool isLeft;

    private void Start()
    {
        playerManager = PlayerManager.instance;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(isLeft)
        {
            playerManager.currentMachine.leftPaddle.EnablePaddle();
        }
        else
        {
            playerManager.currentMachine.rightPaddle.EnablePaddle();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (isLeft)
        {
            playerManager.currentMachine.leftPaddle.DisablePaddle();
        }
        else
        {
            playerManager.currentMachine.rightPaddle.DisablePaddle();
        }
    }
}
