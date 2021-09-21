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

    [Header("Top Bar UI")]
    // Top Bar
    public Text playerCoinText;
    public Text playerGemText;
    public Image coinImage;
    public Vector2 coinPos;

    [Header("Overlay")]
    public GameObject overlay;

    private void Awake()
    {

        instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
        playerCoinText.text = PlayerManager.instance.numFormat.Format(PlayerManager.instance.playerCoins);
        playerGemText.text = PlayerManager.instance.playerGems.ToString();
        coinPos = cam.ScreenToWorldPoint(coinImage.rectTransform.position);
    }

    public void ShowOverlay()
    {
        overlay.SetActive(true);
    }

    public void HideOverlay()
    {
        overlay.SetActive(false);
    }
}