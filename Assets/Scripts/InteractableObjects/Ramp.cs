using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    private Animator theAnim;

    // Start is called before the first frame update
    void Start()
    {
        theAnim = GetComponent<Animator>();
    }

    public void PlayHit()
    {
        theAnim.Play("isHit");
    }

    public void PlayWin()
    {
        theAnim.Play("isWin");
    }
}
