using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObject : MonoBehaviour
{
    public TutorialManager tutorialManager;

    // Start is called before the first frame update
    void Start()
    {
        tutorialManager = GetComponentInParent<TutorialManager>();
    }

    public virtual void NextTutorial()
    {
        gameObject.SetActive(false);
        if (tutorialManager.currTutIndex < tutorialManager.tutObjects.Length - 1)
        {
            tutorialManager.tutObjects[tutorialManager.currTutIndex + 1].StartTutorial();
            tutorialManager.currTutIndex++;
        }
        else
        {
            PlayerManager.instance.tutorialFinished = true;
            ES3.Save("playerTutorialFinished", true);
        }
    }

    public virtual void StartTutorial()
    {
        gameObject.SetActive(true);
    }
}
