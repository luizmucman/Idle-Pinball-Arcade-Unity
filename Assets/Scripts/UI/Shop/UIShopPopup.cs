using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopPopup : MonoBehaviour
{
    // Popup Components
    public Image itemIcon;
    public Text itemTitle;
    public Text itemDesc;
    public GameObject starContainer;
    public List<Image> stars;

    // Star Sprites
    public Sprite unlitStar;
    public Sprite litStar;

    // Gem Sprites
    public Sprite gems300;
    public Sprite gems800;
    public Sprite gems1700;
    public Sprite gems3600;
    public Sprite gems9500;
    public Sprite gems20000;

    // Specials Sprites
    public Sprite adFreeSprite;
    public Sprite incomeBuffSprite;
    public Sprite tripleIncomeBuffSprite;
    public Sprite idleBuffSprite;

    public Animator theAnim;

    private void Start()
    {
        theAnim = GetComponent<Animator>();
    }

    public void SetPopup(Item item)
    {
        itemIcon.sprite = item.itemIcon;
        itemTitle.text = item.itemName;

        ItemData chosenItemData;

        if (item.itemType == ItemType.Ball)
        {
            chosenItemData = PlayerManager.instance.ballDataList.GetItemData(item.GUID);
            
        }
        else if(item.itemType == ItemType.Ticket)
        {
            chosenItemData = PlayerManager.instance.ticketDataList.GetItemData(item.GUID);
        }

        itemDesc.text = item.currRankDescription;
        itemIcon.SetNativeSize();

        for(int i = 0; i < 5; i++)
        {
            stars[i].enabled = true;
            if (i <= item.rank)
            {
                stars[i].sprite = litStar;
            }
            else
            {
                stars[i].sprite = unlitStar;
            }
        }

        gameObject.SetActive(true);

        theAnim.Play("open");
    }

    public void SetPopup(BoostData boost)
    {
        itemIcon.sprite = PlayerManager.instance.boostDatabase.GetBoost(boost.boostID).boostImg;
        itemTitle.text = DoubleFormatter.Format(boost.boostAmt) + "X Boost";
        itemDesc.text = "Gain a " + itemTitle.text + " for " + boost.FormatDuration();
        starContainer.SetActive(false);

        itemIcon.SetNativeSize();

        gameObject.SetActive(true);
        theAnim.Play("open");
    }

    public void SetGemPopup(int amt)
    {
        if (amt <= 300)
        {
            itemIcon.sprite = gems300;
        }
        else if (amt <= 800)
        {
            itemIcon.sprite = gems800;
        }
        else if (amt <= 1700)
        {
            itemIcon.sprite = gems1700;
        }
        else if (amt <= 3600)
        {
            itemIcon.sprite = gems3600;
        }
        else if (amt <= 9500)
        {
            itemIcon.sprite = gems9500;
        }
        else if (amt <= 20000)
        {
            itemIcon.sprite = gems20000;
        }

        itemTitle.text = amt + " Gems";
        itemDesc.text = "You gained " + amt + " gems to use for new balls, machines, boosts, and more!";
        starContainer.SetActive(false);

        itemIcon.SetNativeSize();

        gameObject.SetActive(true);
        theAnim.Play("open");
    }

    public void SetAdFreePopup()
    {
        itemIcon.sprite = adFreeSprite;
        itemTitle.text = "Ad Free Experience";
        itemDesc.text = "All ads have been permanently removed from the game. Enjoy!";
        starContainer.SetActive(false);

        itemIcon.SetNativeSize();

        gameObject.SetActive(true);
        theAnim.Play("open");
    }

    public void SetIncomeBuffPopup()
    {
        itemIcon.sprite = incomeBuffSprite;
        itemTitle.text = "2X All Income";
        itemDesc.text = "You will now permanently gain 2x coins from all sources!";

        starContainer.SetActive(false);

        itemIcon.SetNativeSize();

        gameObject.SetActive(true);
        theAnim.Play("open");
    }

    public void SetTripleIncomeBuffPopup()
    {
        itemIcon.sprite = tripleIncomeBuffSprite;
        itemTitle.text = "3X All Income";
        itemDesc.text = "You will now permanently gain 3x coins from all sources!";

        starContainer.SetActive(false);

        itemIcon.SetNativeSize();

        gameObject.SetActive(true);
        theAnim.Play("open");
    }

    public void SetIdleBuffPopup()
    {
        itemIcon.sprite = idleBuffSprite;
        itemTitle.text = "2X Idle Income";
        itemDesc.text = "You will now permanently gain 2x coins from all idle income";

        starContainer.SetActive(false);

        itemIcon.SetNativeSize();

        gameObject.SetActive(true);
        theAnim.Play("open");
    }

    public void CloseAnimation()
    {
        theAnim.Play("close");
    }

    private void CloseWindow()
    {
        gameObject.SetActive(false);
    }
}
