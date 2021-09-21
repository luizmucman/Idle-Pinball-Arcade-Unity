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
    }

    private void Start()
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
        ballCD = ballStats[itemData.rank].ballCD;
    }

    public virtual void BallSkill()
    {
        ballCDCount = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject hitObject = other.gameObject;

        // Check if collided object is interactable
        if(hitObject.GetComponent<InteractableObject>() != null)
        {
            PlayerManager.instance.AddSeasonPassHit();
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
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject hitObject = other.gameObject;
        PlayerManager.instance.AddSeasonPassHit();

        // Check if collided object is interactable
        if (hitObject.GetComponent<InteractableObject>() != null)
        {
            hitObject.GetComponent<InteractableObject>().BallHit(this, other);
            particle.Play();
        }
    }

}
