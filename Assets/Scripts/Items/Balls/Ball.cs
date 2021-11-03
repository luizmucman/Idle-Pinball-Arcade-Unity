using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Item
{
    private SoundsManager soundsManager;

    [Header("Ball Stats")]
    public List<BallStats> ballStats;
    public Sprite ballIcon;

    // Do Not Set
    // Ball Stats
    [HideInInspector] public int ballID;
    [HideInInspector] public int ballCD;
    [HideInInspector] public int ballCDCount;
    [HideInInspector] public double accumulatedCoins;
    [HideInInspector] public int currMachineBallCount;

    // Ball Components
    [HideInInspector] public Rigidbody2D theRB;
    [HideInInspector] public Collider2D theCollider;
    [HideInInspector] public MachineManager machine;
    [HideInInspector] public ParticleSystem particle;

    // Ball State
    [HideInInspector] public bool skillActive;

    private void Awake()
    {
        theRB = GetComponentInChildren<Rigidbody2D>();
        theCollider = GetComponentInChildren<Collider2D>();
        machine = GetComponentInParent<MachineManager>();
        particle = GetComponentInChildren<ParticleSystem>();
        ballID =  int.Parse(GUID.Substring(3));
        Debug.Log(GUID + ": " + ballID);
    }

    public virtual void Start()
    {
        soundsManager = PlayerManager.instance.GetComponent<SoundsManager>();
    }

    public override void SetItemData(ItemData item)
    {
        base.SetItemData(item);
        SetBallStats();
    }

    public virtual void SetBallStats()
    {
        ballCD = ballStats[itemData.rank].ballCD - PlayerManager.instance.playerTicketBuffs.ballCD;
        currRankDescription = itemDescription.Replace("{Value}", ballStats[rank].ballCD.ToString());
        nextRankDescription = itemDescription.Replace("{Value}", ballStats[rank + 1].ballCD.ToString());
    }

    public virtual void BallSkill()
    {
        ballCDCount = 0;
    }

    public override string GetCurrentRankDesc()
    {
        return "Hits Required: " + ballStats[rank].ballCD;

    }

    public override string GetNextRankDesc()
    {
        return "Hits Required: " + ballStats[rank + 1].ballCD;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        GameObject hitObject = other.gameObject;

        // Check if collided object is interactable
        if(hitObject.GetComponent<InteractableObject>() != null)
        {
            UIManager.instance.uiChallengeManager.AddChallengeHit(1, ChallengeType.BallHit);
            hitObject.GetComponent<InteractableObject>().BallHit(this, other);
            particle.Play();
            if(!skillActive)
            {
                ballCDCount++;
                if (ballCDCount >= ballCD)
                {
                    BallSkill();
                }
            }
            PlayerManager.instance.AddSeasonPassHit();
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        GameObject hitObject = other.gameObject;

        // Check if collided object is interactable
        if (hitObject.GetComponent<InteractableObject>() != null)
        {
            UIManager.instance.uiChallengeManager.AddChallengeHit(1, ChallengeType.BallHit);
            hitObject.GetComponent<InteractableObject>().BallHit(this, other);
            particle.Play();
        }
        PlayerManager.instance.AddSeasonPassHit();
    }

}
