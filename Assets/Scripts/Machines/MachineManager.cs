using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MachineManager : MonoBehaviour
{
    //UI Manager
    private UIManager uiManager;

    // Ball Info
    [HideInInspector] public List<int> equippedBallCount;
    [HideInInspector] public int normalBallCount;
    [HideInInspector] public int currentBallCount;
     public int maxEquippedBalls;
    [HideInInspector] public List<Ball> instantiatedBalls;
    private List<Ball> instantiatedNormalBalls;

    // Machine Info
    public bool testBalance;
    [HideInInspector] public string machineSceneName;
    [HideInInspector] public float cpsMinuteCounter;
    [HideInInspector] public ulong coinsPerSecondCounter;
    [HideInInspector] public MachineData machineData;

    // Connected Objects

    [HideInInspector] public ObjectManager[] objectManagers;
    [HideInInspector] public List<UpgradeData> upgradeDatas;
    private Canvas[] objectCanvases;

    // Paddles Data
    [HideInInspector] public Double paddleMultiplier;


    [Header("Set In Inspector")]
    public bool isEvent;
    public Shooter shooter;
    public Paddle rightPaddle;
    public Paddle leftPaddle;


    private void Awake()
    {
        objectManagers = GetComponentsInChildren<ObjectManager>();
        objectCanvases = GetComponentsInChildren<Canvas>();
        machineSceneName = SceneManager.GetActiveScene().name;

        for (int i = 0; i < objectManagers.Length; i++)
        {
            objectManagers[i].LoadManager(machineSceneName);
        }

        if (ES3.KeyExists(machineSceneName)) {
            ES3.LoadInto(machineSceneName, gameObject);
        }
    }

    private void Start()
    {
        if(testBalance)
        {
            Time.timeScale = 50;
        }

        PlayerManager.instance.currentMachine = this;
        uiManager = UIManager.instance;
        CheckAutoPaddles();

        DisableUpgradeWindow();

        instantiatedNormalBalls = new List<Ball>();

        // Add a new ball count for each ball in the database
        while (equippedBallCount.Count < PlayerManager.instance.ballDatabase.database.Count)
        {
            equippedBallCount.Add(0);
        }

        currentBallCount = 0;
        normalBallCount = 0;

        uiManager.uiBallManager.NewMachine();

        // Instantiate the normal balls until max balls for machine is reached
        while (normalBallCount + currentBallCount < maxEquippedBalls)
        {
            ShootNormalBall();
        }

        if (isEvent)
        {
            foreach (MachineData data in PlayerManager.instance.eventMachines)
            {
                if (data.machineGUID.Equals(SceneManager.GetActiveScene().name))
                {
                    machineData = data;
                    break;
                }
            }
        }
        else
        {
            foreach (MachineData data in PlayerManager.instance.mainMachines)
            {
                if (data.machineGUID.Equals(SceneManager.GetActiveScene().name))
                {
                    machineData = data;
                    break;
                }
            }
        }

        machineData.isPlaying = true;
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
            SetAwayTime();
        }
    }

    private void OnApplicationQuit()
    {
        SetAwayTime();
        SaveMachine();
    }

    private void SetCPS()
    {
        if(coinsPerSecondCounter > machineData.coinsPerSecond)
        {
            machineData.coinsPerSecond = coinsPerSecondCounter / 60;
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
        equippedBallCount[ball.ballID]++;

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

        IgnoreCollisions(newBall);

        return newBall;
    }

    // Removes the specified special ball and shoots a normal ball to replace it.
    public void RemoveBall(Ball ball)
    {
        currentBallCount--;
        equippedBallCount[ball.ballID]--;
        instantiatedBalls.Remove(ball);
        Destroy(ball.gameObject);
        uiManager.uiBallManager.equippedBallValueText.text = currentBallCount.ToString();
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

    public void SaveMachine()
    {
        for (int i = 0; i < objectManagers.Length; i++)
        {
            objectManagers[i].SaveManager(machineSceneName);
        }
        ES3.Save(machineSceneName, gameObject);
    }

    public void RewardAway()
    {
        TimeSpan span = DateTime.Now - machineData.awayCheckPoint;

        if (span.TotalMinutes > 5)
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
}
