using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    public TutorialManager tutorialManager;
    public Button btn;

    // Start is called before the first frame update
    void Start()
    {
        if(UIManager.instance.tutorialManager != null && !PlayerManager.instance.tutorialFinished)
        {
            tutorialManager = UIManager.instance.tutorialManager;
            btn = GetComponent<Button>();
            btn.onClick.AddListener(() => tutorialManager.IncrementTutorial());
        }
    }
}
