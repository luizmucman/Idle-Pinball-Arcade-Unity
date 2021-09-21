using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBallPopup : MonoBehaviour
{
    [Header("UI Elements")]
    public Image ballIcon;
    public Text ballTitle;
    public Text ballDesc;
    public Image[] stars;
    public Text currRankHits;
    public Text nextRankHits;

    [Header("Stars")]
    public Sprite litStar;
    public Sprite unlitStar;

    [Header("Unassigned Data")]
    public ItemData selectedBall;

    public void SetPopupData(Ball ball)
    {
        ballIcon.sprite = ball.ballIcon;
        ballTitle.text = ball.itemName;
        ballDesc.text = ball.itemDescription;
        for (int i = 0; i < 5; i++)
        {
            if (i <= ball.rank)
            {
                stars[i].sprite = litStar;
            }
            else
            {
                stars[i].sprite = unlitStar;
            }
        }
        currRankHits.text = "Hits Required: " + ball.ballStats[ball.rank].ballCD;
        nextRankHits.text = "Hits Required: " + ball.ballStats[ball.rank + 1].ballCD;
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
