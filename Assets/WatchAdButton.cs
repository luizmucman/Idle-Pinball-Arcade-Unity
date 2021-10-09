using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatchAdButton : MonoBehaviour
{
    public Image btnImage;

    public Sprite watchAds;
    public Sprite noAds;

    private void Start()
    {
        btnImage = GetComponent<Image>();
    }

    public void CheckAdFree()
    {
        if (!PlayerManager.instance.isAdFree)
        {
            btnImage.sprite = watchAds;
        }
        else
        {
            btnImage.sprite = noAds;
        }
    }

}
