using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public SoundsManager soundsManager;

    // Object Components
    [HideInInspector] public Rigidbody2D theRB;
    [HideInInspector] public Collider2D theCollider;
    [HideInInspector] public Animator theAnimator;

    public virtual void Awake()
    {
        theRB = GetComponent<Rigidbody2D>();
        theCollider = GetComponent<Collider2D>();
        theAnimator = GetComponent<Animator>();
    }

    public virtual void Start()
    {
        soundsManager = PlayerManager.instance.GetComponent<SoundsManager>();
    }

    public virtual void ResetTarget()
    {
        theAnimator.SetBool("isHit", false);
    }

    public virtual void BallHit(Ball ball, Collision2D collision)
    {
        theAnimator.Play("isHit");
    }

    public virtual void BallHit(Ball ball, Collider2D collision)
    {
        theAnimator.Play("isHit");
    }
}
