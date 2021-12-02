using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public int currTutIndex;
    public TutorialObject[] tutObjects;


    // Start is called before the first frame update
    void Start()
    {
        currTutIndex = 0;
        if (!PlayerManager.instance.tutorialFinished)
        {
            CheckIfCloudAccountLinked();
            tutObjects[currTutIndex].gameObject.SetActive(true);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CheckIfCloudAccountLinked()
    {
        if(PlayerManager.instance.facebookAccLinked || PlayerManager.instance.googleAccLinked)
        {
            PlayerManager.instance.LoadFromPlayfab();
        }
    }

    public void IncrementTutorial()
    {
        if (currTutIndex < tutObjects.Length)
        {
            tutObjects[currTutIndex].NextTutorial();
        }
    }
}
