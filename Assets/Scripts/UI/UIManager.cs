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
    [SerializeField] private float gemCountdown;

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
        playerCoinText.text = DoubleFormatter.Format(PlayerManager.instance.playerCoins);
        playerGemText.text = PlayerManager.instance.playerGems.ToString();
        ResetGemRewardBtn();
    }

    private void Update()
    {
        gemCountdown -= Time.deltaTime;

        if (gemCountdown <= 0)
        {
            gemRewardBtn.gameObject.SetActive(true);
        }
    }

    public void ResetGemRewardBtn()
    {
        gemCountdown = 60 * gemRewardMinuteCountdown;
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