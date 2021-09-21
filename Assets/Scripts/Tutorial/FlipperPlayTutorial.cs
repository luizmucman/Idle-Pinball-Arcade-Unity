using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperPlayTutorial : TutorialObject
{
    public override void StartTutorial()
    {
        base.StartTutorial();
        StartCoroutine(EndFlipperTutorial());
    }

    private IEnumerator EndFlipperTutorial()
    {
        yield return new WaitForSeconds(3f);
        NextTutorial();
    }
}
