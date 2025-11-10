using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Button dealBtn;
    public Button hitBtn;
    public Button stayBtn;
    public Button betBtn;

    private int stayClicks = 0;

    public PlayerScript playerScript;
    public PlayerScript dealerScript;


    public TMP_Text scoreText;
    public TMP_Text dealerScoreText;
    public TMP_Text betsText;
    public TMP_Text cashText;
    // public Text mainText;
    public TMP_Text stayBtnText;

    public GameObject hideCard;
    int pot = 0;
    void Start()
    {
        dealBtn.onClick.AddListener(() => DealClicked());
        hitBtn.onClick.AddListener(() => HitClicked());
        stayBtn.onClick.AddListener(() => StayClicked());
    }

    // Update is called once per frame
    private void DealClicked()
    {
        //
        dealerScoreText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();
        //update the score displayed
        scoreText.text = "Hand: " + playerScript.handValue.ToString();
        dealerScoreText.text = "Hand: " + dealerScript.handValue.ToString();
        // adjuest buttons visibility
        dealBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(true);
        stayBtn.gameObject.SetActive(true);
        stayBtnText.text = "Stay";
        // set standard pot size
        pot = 40;
        betsText.text = pot.ToString();
        //playerScript.AdjustMoney(-20);
        //cashText.text = playerScript.GetMoney().ToString();

    }
    private void HitClicked()
    {
        if(playerScript.GetCard() <= 10)
        {
            playerScript.GetCard();
        }
    }
    private void StayClicked()
    {
        stayClicks++;
        if (stayClicks > 1) Debug.Log("end function");
        HitDealer();
        stayBtnText.text = "Call";

    }

    private void HitDealer()
    {
        while(dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCard();
        }
    }
}
