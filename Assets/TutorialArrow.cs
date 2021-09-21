using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArrow : MonoBehaviour
{
    public ArrowDirection direction;

    public Animator theAnim;

    public enum ArrowDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    // Start is called before the first frame update
    void Start()
    {
        theAnim = GetComponent<Animator>();

        if (direction == ArrowDirection.Down)
        {
            theAnim.Play("down");
        }
        else if (direction == ArrowDirection.Up)
        {
            theAnim.Play("up");
        }
    }

}
