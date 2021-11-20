using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapjoyUnity;
using UnityEngine.UI;

public class UITapjoyScript : MonoBehaviour
{
    public GameObject offerwallSaleTextObject;

    private TJPlacement tapjoyOfferwall;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TapjoyBalanceCheckFunction());

        tapjoyOfferwall = TJPlacement.CreatePlacement("Offerwall");

        if (Tapjoy.IsConnected)
        {
            tapjoyOfferwall.RequestContent();
        }
        else
        {
            Debug.LogWarning("Tapjoy SDK must be connected before you can request content.");
        }
    }

    // on enable, add delegates
    void OnEnable()
    {
        
        Tapjoy.OnGetCurrencyBalanceResponse += HandleGetCurrencyBalanceResponse;
        //Tapjoy.OnSpendCurrencyResponse += HandleSpendCurrencyResponse;
        //TJPlacement.OnRewardRequest += HandleOnRewardRequest;
    }

    // on disable, remove delegates
    void OnDisable()
    {
        Tapjoy.OnGetCurrencyBalanceResponse -= HandleGetCurrencyBalanceResponse;
        //Tapjoy.OnSpendCurrencyResponse -= HandleSpendCurrencyResponse;
    }

    public void HandleGetCurrencyBalanceResponse(string currencyName, int balance)

    {
        int userBalance = balance;
        //Debug.Log("Balance Checked");
        if (balance > 0)
        {
            UIManager.instance.uiShopManager.BuyGems(balance);
            SpendBalance(userBalance);

        }
    }


    public void SpendBalance(int balance)
    {
        Tapjoy.SpendCurrency(balance);
    }

    public IEnumerator TapjoyBalanceCheckFunction()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            if (Tapjoy.IsConnected)
            {
                Tapjoy.GetCurrencyBalance();
            }
            yield return new WaitForSeconds(15);
        }
    }

    void HandleOnRewardRequest()
    {

    }

    public void TapJoyOfferWallOpen()
    {
        StartCoroutine("TapJoyFunctionDelay");
    }

    public System.Collections.IEnumerator TapJoyFunctionDelay()
    {
        if (tapjoyOfferwall.IsContentReady())
        {

        }
        else
        {
            tapjoyOfferwall.RequestContent();
        }
        float timer = 0;
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            timer += .1f;
            if (tapjoyOfferwall.IsContentReady())
            {
                tapjoyOfferwall.ShowContent();
                yield break;
            }
            if (timer > 5)
            {
                yield break;
            }
        }
    }
    //Tapjoy End
}
