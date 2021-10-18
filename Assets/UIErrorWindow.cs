using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIErrorWindow : MonoBehaviour
{
    public Text errorDesc;

    public void SetErrorDesc(string text)
    {
        errorDesc.text = text;
        gameObject.SetActive(true);
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }
}
