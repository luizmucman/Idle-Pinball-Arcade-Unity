using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleIdleIncomeCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerManager.instance.is2xIdleIncome)
        {
            gameObject.SetActive(false);
        }
    }
}
