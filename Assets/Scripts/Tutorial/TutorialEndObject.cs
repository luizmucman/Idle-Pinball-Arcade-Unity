using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEndObject : TutorialObject
{
    public override void NextTutorial()
    {
        PlayerManager.instance.tutorialFinished = true;
        tutorialManager.gameObject.SetActive(false);
    }
}
