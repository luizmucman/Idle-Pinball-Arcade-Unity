using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTarget : InteractableObject
{
    public MultiTargetManager manager;
    private MultiTargetLight connectedLight;

    private bool isHit;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponentInParent<MultiTargetManager>();
        connectedLight = GetComponentInChildren<MultiTargetLight>();

    }

    public void Win()
    {
        theAnimator.Play("isWin");
        if(connectedLight != null)
        {
            connectedLight.PlayWinAnim();
        }
        Invoke(nameof(Reset), 1f);
    }

    public override void BallHit(Ball ball, Collision2D collision)
    {
        if (!isHit)
        {
            isHit = true;
            base.BallHit(ball, collision);
            if (connectedLight != null)
            {
                connectedLight.PlayLitAnim();
            }
            manager.AddHit(ball);
        }
    }

    private void Reset()
    {
        isHit = false;

    }
}
