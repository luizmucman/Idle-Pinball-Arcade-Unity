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
    [SerializeField] private Text currRankText;
    [SerializeField] private Text nextRankText;
    [SerializeField] private Text expText;
    [SerializeField] private Slider expSlider;
    public Text nextRankBallDesc;

    [Header("Stars")]
    public Sprite litStar;
    public Sprite unlitStar;

    [Header("Unassigned Data")]
    public ItemData selectedBall;

    public void SetPopupData(Ball ball)
    {
        ballIcon.sprite = ball.ballIcon;
        ballTitle.text = ball.itemName;
        ballDesc.text = ball.currRankDescription;
        currRankText.text = ball.itemData.rank.ToString();
        nextRankText.text = (ball.itemData.rank + 1).ToString();
        expText.text = ball.itemData.exp.ToString() + "/" + ball.itemData.expReqs[ball.itemData.rank].ToString();
        expSlider.maxValue = ball.itemData.expReqs[ball.itemData.rank];
        expSlider.value = ball.itemData.exp;
        nextRankBallDesc.text = ball.GetNextRankDesc();
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
