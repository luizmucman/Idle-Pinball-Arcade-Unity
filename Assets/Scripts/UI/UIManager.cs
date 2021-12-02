using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TutorialManager tutorialManager;

    private Camera cam;

    [Header("UI Managers")]
    public UIBallManager uiBallManager;
    public UIUpgradeManager uiUpgradeManager;
    public UITicketManager uiTicketManager;
    public UIMachineManager uiMachineManager;
    public UIAwayCoinPopupManager uiAwayPopupManager;
    public UIBoostsManager uiBoostsManager;
    public UIShopManager uiShopManager;
    public UISeasonPassManager uiSeasonPassManager;
    public UIMenuManager uiMenuManager;
    public UIErrorWindow uiErrorWindow;
    public UIChallengeManager uiChallengeManager;

    [Header("Top Bar UI")]
    // Top Bar
    [SerializeField] Text playerCPSText;
    public Text playerCoinText;
    public Text playerGemText;
    public Image coinImage;
    [SerializeField] private Button gemRewardBtn;
    [SerializeField] private float gemRewardMinuteCountdown;
    [SerializeField] private DateTime gemPopupLastRewarded;

    [Header("Overlay")]
    public GameObject overlay;

    [Header("Windows")]
    [SerializeField] private UIBottomRow bottomRow;
    [SerializeField] private List<UIWindow> windows;

    private void Awake()
    {

        instance = this;

    }

    private void Start()
    {
        cam = Camera.main;
        playerCoinText.text = DoubleFormatter.Format(PlayerManager.instance.playerMachineData.currMachineData.GetCoinCount());
        playerGemText.text = PlayerManager.instance.playerGems.ToString();
        gemRewardBtn.gameObject.SetActive(false);
        gemPopupLastRewarded = ES3.Load("gemPopupLastRewarded", DateTime.Now);
        ES3.Save("gemPopupLastRewarded", gemPopupLastRewarded);
    }

    private void Update()
    {
        TimeSpan gemRewardTimeDiff = DateTime.Now - gemPopupLastRewarded;

        if (gemRewardTimeDiff.TotalMinutes > 4)
        {
            gemRewardBtn.gameObject.SetActive(true);
        }
    }

    public void ResetGemRewardBtn()
    {
        gemPopupLastRewarded = DateTime.Now;
        ES3.Save("gemPopupLastRewarded", gemPopupLastRewarded);
        gemRewardBtn.gameObject.SetActive(false);
    }

    public void SetCPSText(double cps)
    {
        playerCPSText.text = DoubleFormatter.Format(cps) + "/S" ;
    }

    public void ShowOverlay()
    {
        overlay.SetActive(true);
    }

    public void HideOverlay()
    {
        overlay.SetActive(false);
    }

    public void ResetWindows()
    {
        bottomRow.ResetButtonState();
        foreach(UIWindow window in windows)
        {
            if(window.gameObject.activeInHierarchy)
            {
                window.CloseAnim();
            }
        }
    }


}