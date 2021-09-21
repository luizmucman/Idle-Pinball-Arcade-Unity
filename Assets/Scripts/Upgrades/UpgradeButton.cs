using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public ObjectManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponentInParent<ObjectManager>();

    }

    public void UpgradeButtonClicked()
    {
        UIManager.instance.uiUpgradeManager.SetUpgrade(manager);
    }
}
