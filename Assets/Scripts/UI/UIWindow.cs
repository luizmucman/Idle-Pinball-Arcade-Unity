using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWindow : MonoBehaviour
{
    [SerializeField]  private Animator theAnim;

    public virtual void OpenAnim()
    {
        gameObject.SetActive(true);
        theAnim.Play("open");

    }

    public void CloseAnim()
    {
        theAnim.Play("close");
        
    }

    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
