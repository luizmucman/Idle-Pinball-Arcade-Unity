using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLight : MonoBehaviour
{
    private Animator theAnim;

    // Start is called before the first frame update
    void Start()
    {
        theAnim = GetComponent<Animator>();
    }

    public void PlayLitAnim()
    {
        theAnim.Play("isHit");
    }

    public void PlayWinAnim()
    {
        theAnim.Play("isWin");
    }
}
