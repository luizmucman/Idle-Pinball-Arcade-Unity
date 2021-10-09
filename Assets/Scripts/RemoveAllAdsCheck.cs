using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAllAdsCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerManager.instance.isAdFree)
        {
            gameObject.SetActive(false);
        }
    }

}
