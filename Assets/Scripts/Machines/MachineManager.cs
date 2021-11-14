using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MachineManager : MonoBehaviour
{
    private ItemDatabase ballDatabase;

    //UI Manager
    private UIManager uiManager;

    // Ball Info
    [HideInInspector] public int normalBallCount;
    [HideInInspector] public int currentBallCount;
    public int maxEquippedBalls;
    [HideInInspector] public List<Ball> instantiatedBalls;
    private List<Ball> instantiatedNormalBalls;

    // Machine Info
    public bool testBalance;
    [HideInInspector] public string machineSceneName;
    [HideInInspector] public float cpsMinuteCounter;
    [HideInInspector] public double coinsPerSecondCounter;
    [HideInInspector] public MachineData machineData;

    // Connected Objects

    [HideInInspector] public ObjectManager[] objectManagers;
    [HideInInspector] public List<UpgradeData> upgradeDatas;
    private Canvas[] objectCanvases;

    // Paddles Data
    [HideInInspector] public double paddleMultiplier;


    [Header("Set In Inspector")]
    //public bool isEvent;
    public Shooter shooter;
    public Paddle rightPaddle;
    public Paddle leftPaddle;


    private void Awake()
    {
        objectManagers = GetComponentsInChildren<ObjectManager>();
        objectCanvases = GetComponentsInChildren<Canvas>();
        machineSceneName = SceneManager.GetActiveScene().name;
        ballDatabase = PlayerManager.instance.ballDatabase;
        PlayerManager.instance.currentMachine = this;

        LoadMachine();
    }

    private void Start()
    {

        if(testBalance)
        {
            Time.timeScale = 25;
        }

 
        uiManager = UIManager.instance;
        CheckAutoPaddles();

        DisableUpgradeWindow();

        instantiatedNormalBalls = new List<Ball>();

        currentBallCount = 0;
        normalBallCount = 0;

        uiManager.uiBallManager.NewMachine();

        // Instantiate the normal balls until max balls for machine is reached
        while (normalBallCount + currentBallCount < maxEquippedBalls + PlayerManager.instance.playerTicketBuffs.maxBalls)
        {
            ShootNormalBall();
        }

        machineData = PlayerManager.instance.currMachineData;

        machineData.isPlaying = true;
        if (machineData.isCurrentEvent)
        {
            UIManager.instance.uiSeasonPassManager.ShowSeasonPassButton();
            UIManager.instance.playerCoinText.text = DoubleFormatter.Format(PlayerManager.instance.eventCoins);
            if(!PlayerManager.instance.seasonPassData.isPremium)
            {
                UIManager.instance.uiShopManager.seasonPassPopup.OpenWindow();
            }
        }
        else
        {
            UIManager.instance.uiSeasonPassManager.HideSeasonPassButton();
            UIManager.instance.playerCoinText.text = DoubleFormatter.Format(PlayerManager.instance.playerCoins);
        }

        foreach(ObjectManager manager in objectManagers)
        {
            UIManager.instance.uiUpgradeManager.AddUpgradeRow(manager);
        }

        RewardAway();
    }

    private void Update()
    {
        cpsMinuteCounter += Time.deltaTime;

        if(cpsMinuteCounter >= 60)
        {
            SetCPS();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            SaveMachine();
        }
    }

    private void OnApplicationQuit()
    {
        SaveMachine();
    }

    private void SetCPS()
    {
        if(coinsPerSecondCounter > machineData.coinsPerSecond)
        {
            machineData.coinsPerSecond = (double) (coinsPerSecondCounter / 60 * PlayerManager.instance.playerTicketBuffs.cpsBuff);
            coinsPerSecondCounter = 0;
            cpsMinuteCounter = 0;
        }
    }

    // Makes sure balls don't collide with each other.
    private void IgnoreCollisions(Ball currBall)
    {
        foreach (Ball ball in instantiatedBalls)
        {
            Physics2D.IgnoreCollision(currBall.GetComponent<Collider2D>(), ball.GetComponent<Collider2D>());
        }
    }

    // Add a ball already on equipped list when resetting machine.
    public Ball AddResetBall(Ball ball)
    {
        currentBallCount++;
        Ball newBall = Instantiate(ball, transform.parent);
        instantiatedBalls.Add(newBall);
        shooter.ShootBall(newBall);
        newBall.machine = this;
        IgnoreCollisions(newBall);

        return newBall;
    }

    // Add a completely new special ball to the game.
    public Ball AddBall(Ball ball)
    {
        // Add ball count to the special ball in array
        Ball chosenBall = (Ball) ballDatabase.GetItem(ball.GUID);
        chosenBall.currMachineBallCount++;

        // Increase special ball count and remove normal ball count
        currentBallCount++;
        normalBallCount--;

        // Remove a normal ball from game and from list
        Ball normalBall = instantiatedNormalBalls[0];
        instantiatedNormalBalls.Remove(normalBall);
        Destroy(normalBall.gameObject);

        // Instantiate new special ball into the machine and add to instantiated ball list
        Ball newBall = Instantiate(ball, transform.parent);
        newBall.machine = this;
        instantiatedBalls.Add(newBall);
        shooter.ShootBall(newBall);

        // Change ball UI count
        uiManager.uiBallManager.equippedBallValueText.text = currentBallCount.ToString();
        UIManager.instance.uiBallManager.SetNormalBallCount(normalBallCount);

        IgnoreCollisions(newBall);

        return newBall;
    }

    // Removes the specified special ball and shoots a normal ball to replace it.
    public void RemoveBall(Ball ball)
    {
        currentBallCount--;

        Ball chosenBall = (Ball)ballDatabase.GetItem(ball.GUID);
        chosenBall.currMachineBallCount--;

        instantiatedBalls.Remove(ball);
        Destroy(ball.gameObject);
        uiManager.uiBallManager.equippedBallValueText.text = currentBallCount.ToString();
        UIManager.instance.uiBallManager.SetNormalBallCount(normalBallCount);
        ShootNormalBall();
    }

    // Shoots a normal ball.
    public void ShootNormalBall()
    {
        Ball currBall = (Ball)PlayerManager.instance.ballDatabase.GetItem("BA000");
        Ball newBall = Instantiate(currBall, transform.parent);
        IgnoreCollisions(newBall);
        instantiatedNormalBalls.Add(newBall);
        normalBallCount++;
        UIManager.instance.uiBallManager.SetNormalBallCount(normalBallCount);
        shooter.ShootBall(newBall);
    }

    public void EnableUpgradeWindow()
    {

        foreach (ObjectManager manager in objectManagers)
        {
            manager.EnableUpgradeCanvas();
        }
    }

    public void DisableUpgradeWindow()
    {
        foreach (ObjectManager manager in objectManagers)
        {
            manager.DisableUpgradeCanvas();
        }
    }

    public void ExitMachine()
    {
        machineData.isPlaying = false;
        SetAwayTime();
        SaveMachine();
    }

    public void SetAwayTime()
    {
        if (machineData.accumulatedCoins == 0 && machineData.awayCheckPoint != null)
        {
            machineData.awayCheckPoint = DateTime.Now;
        }
    }

    public void RewardAway()
    {
        TimeSpan span = DateTime.Now - machineData.awayCheckPoint;

        if (machineData.accumulatedCoins > 0 && span.TotalMinutes > 3)
        {
            uiManager.uiAwayPopupManager.SetMachine(machineData);
        }
    }

    public void CheckAutoPaddles()
    {
        if (leftPaddle.isAuto && rightPaddle.isAuto)
        {
            paddleMultiplier = 1.5;
        }
        else if (leftPaddle.isAuto || rightPaddle.isAuto)
        {
            paddleMultiplier = 1.25;
        }
        else
        {
            paddleMultiplier = 1;
        }
    }

    public void SaveMachine()
    {
        machineData.SetAwayCheckpoint();
        for (int i = 0; i < objectManagers.Length; i++)
        {
            objectManagers[i].SaveManager(machineSceneName);
        }

        ES3.Save(machineSceneName + "-maxEquippedBalls", maxEquippedBalls);

        foreach (Ball currBall in ballDatabase.database)
        {
            ES3.Save(machineSceneName + currBall.GUID + "equipped-count", currBall.currMachineBallCount);
        }
    }

    public void LoadMachine()
    {
        for (int i = 0; i < objectManagers.Length; i++)
        {
            objectManagers[i].LoadManager(machineSceneName);
        }

        maxEquippedBalls = ES3.Load(machineSceneName + "-maxEquippedBalls", 1);

        foreach(Ball currBall in ballDatabase.database)
        {
            currBall.currMachineBallCount = ES3.Load(machineSceneName + currBall.GUID + "equipped-count", 0);
        }

    }
}
