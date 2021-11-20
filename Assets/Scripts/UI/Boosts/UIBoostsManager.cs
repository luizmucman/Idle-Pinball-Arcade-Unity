using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBoostsManager : MonoBehaviour
{

    public List<BoostActiveContainer> boostContainers;

    public UIWindow boostWindow;
    public Text mainUIBoostText;

    // Active Boost Instantiate
    public BoostActiveContainer activeBoostContainerPrefab;
    public GameObject activeBoostContentContainer;

    // Owned Boost Instantiate
    public BoostOwnedContainer ownedBoostContainerPrefab;
    public GameObject ownedBoostContentContainer;

    // Total Boost Data
    public float checkBoostsTimer;
    public Text totalBoostAmtText;
    public double totalBoostAmt;

    // Boost Description Window
    public BoostDescriptionWindow descriptionWindow;

    // Start is called before the first frame update
    void Start()
    {
        checkBoostsTimer = 1f;
        totalBoostAmt = 1;
        foreach(BoostData data in PlayerManager.instance.boostDatabase.database)
        {
            BoostActiveContainer activeContainer = Instantiate(activeBoostContainerPrefab, activeBoostContentContainer.transform);
            activeContainer.SetBoostData(data);
            boostContainers.Add(activeContainer);
        }

        foreach (BoostData data in PlayerManager.instance.boostInventory)
        {
            AddNewOwnedBoostContainer(data);
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkBoostsTimer += Time.deltaTime;
        if(checkBoostsTimer > 1f)
        {
            GetBoostAmt();
            checkBoostsTimer = 0f;
        }
    }

    public void AddNewOwnedBoostContainer(BoostData data)
    {
        BoostOwnedContainer ownedContainer = Instantiate(ownedBoostContainerPrefab, ownedBoostContentContainer.transform);
        ownedContainer.SetBoostData(data);
    }

    private void GetBoostAmt()
    {
        double currBoostAmt = 0;
        foreach (BoostActiveContainer container in boostContainers)
        {
            currBoostAmt += container.UpdateBoostStatus();
        }

        if (currBoostAmt > 0)
        {
            totalBoostAmt = currBoostAmt;
        }
        else
        {
            totalBoostAmt = 1;
        }

        totalBoostAmtText.text = DoubleFormatter.Format(totalBoostAmt) + "X";
        mainUIBoostText.text = DoubleFormatter.Format(totalBoostAmt) + "X";
    }

    public void CloseBoostWindow()
    {
        boostWindow.CloseAnim();
    }

    public void OpenBoostWindow()
    {
        ResetOwnedBoosts();
        boostWindow.OpenAnim();
    }

    public void ResetOwnedBoosts()
    {
        BoostOwnedContainer[] boostsOwned = ownedBoostContentContainer.GetComponentsInChildren<BoostOwnedContainer>();
        foreach (BoostOwnedContainer ownedContainer in boostsOwned)
        {
            Destroy(ownedContainer.gameObject);
        }

        foreach (BoostData data in PlayerManager.instance.boostInventory)
        {
            BoostOwnedContainer ownedContainer = Instantiate(ownedBoostContainerPrefab, ownedBoostContentContainer.transform);
            ownedContainer.SetBoostData(data);
        }
    }
}
