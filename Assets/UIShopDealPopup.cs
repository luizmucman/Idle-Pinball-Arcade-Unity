using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIShopDealPopup : MonoBehaviour
{
    private float currTimer;
    [SerializeField] private float dealTimerMinutes;
    [SerializeField] private TextMeshProUGUI timerText;



    // Update is called once per frame
    void Update()
    {
        currTimer -= Time.deltaTime;

        if(currTimer <= 0)
        {
            GetComponent<UIWindow>().CloseAnim();
        }
        else
        {
            float minutes = Mathf.FloorToInt(currTimer / 60);
            float seconds = Mathf.FloorToInt(currTimer % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void SetTimer()
    {
        currTimer = dealTimerMinutes * 60;
    }
}
