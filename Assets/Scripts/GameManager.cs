using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button dealBtn;
    public Button hitBtn;
    public Button stayBtn;
    public Button betBtn;

    [Header("Players")]
    public PlayerScript playerScript;
    public PlayerScript dealerScript;

    [Header("UI Texts")]
    public TMP_Text scoreText;        
    public TMP_Text dealerScoreText;  
    public TMP_Text betsText;         
    public TMP_Text cashText;        
    public TMP_Text mainText;         
    public TMP_Text stayBtnText;      

    [Header("Dealer Hole Card")]
    public GameObject hideCard;

    int pot = 0;
    bool roundOver = false;
    int stayClicks = 0;

    void Start()
    {
        dealBtn.onClick.AddListener(DealClicked);
        hitBtn.onClick.AddListener(HitClicked);
        stayBtn.onClick.AddListener(StayClicked);
        betBtn.onClick.AddListener(BetClicked);

        hitBtn.interactable = false;
        stayBtn.interactable = false;

        if (betsText != null) betsText.text = "Bets: $0";
        if (cashText != null) cashText.text = "$" + playerScript.GetMoney();
        if (mainText != null) mainText.text = "NEW";
        if (stayBtnText != null) stayBtnText.text = "Stay";
    }

    // ========================
    // Betting
    // ========================

    public void BetClicked()
    {
        if (betBtn == null)
        {
            Debug.LogError("betBtn is not assigned on GameManager.");
            return;
        }

        TMP_Text newBet = betBtn.GetComponentInChildren<TMP_Text>();
        if (newBet == null)
        {
            Debug.LogError("Bet button has no TMP_Text child.");
            return;
        }

        string betString = newBet.text.Trim();   
        if (betString.StartsWith("$"))
            betString = betString.Substring(1);

        if (!int.TryParse(betString, out int intBet))
        {
            Debug.LogError("Invalid bet text on chip: " + newBet.text);
            return;
        }

        if (playerScript.GetMoney() < intBet)
        {
            mainText.text = "Not enough money!";
            return;
        }

        
        playerScript.AdjustMoney(-intBet);
        pot += intBet;

        if (cashText != null)
            cashText.text = "$" + playerScript.GetMoney();
        if (betsText != null)
            betsText.text = "Bets: $" + pot;
    }

    // Start round

    private void DealClicked()
    {
        if (pot <= 0)
        {
            mainText.text = "Place a bet first!";
            return;
        }

        roundOver = false;
        stayClicks = 0;
        if (stayBtnText != null) stayBtnText.text = "Stay";

        //Reset both dealer and player hand
        playerScript.ResetHand();
        dealerScript.ResetHand();

        // UI reset
        if (mainText != null) mainText.gameObject.SetActive(false);
        if (dealerScoreText != null) dealerScoreText.gameObject.SetActive(false);

        // open the hide card
        if (hideCard != null)
        {
            var r = hideCard.GetComponent<Renderer>();
            if (r != null) r.enabled = true;
        }

        // shuffle and deal the card 
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();

        if (scoreText != null)
            scoreText.text = "Hand: " + playerScript.handValue;

        // shows the button status base on the situation
        dealBtn.interactable = false;
        dealBtn.gameObject.SetActive(false);
        betBtn.interactable = false;
        hitBtn.interactable = true;
        hitBtn.gameObject.SetActive(true);
        stayBtn.interactable = true;
        stayBtn.gameObject.SetActive(true);

        // Checking black jack once hit the deal
        if (playerScript.handValue == 21 || dealerScript.handValue == 21)
        {
            HitDealer();   
            RoundOver();
        }
    }

    // Player actions
  
    private void HitClicked()
    {
        if (roundOver) return;

        if (playerScript.cardIndex < playerScript.hand.Length)
        {
            playerScript.GetCard();
            if (scoreText != null)
                scoreText.text = "Hand: " + playerScript.handValue;

            if (playerScript.handValue > 21)
            {
                RoundOver();
            }
        }
    }

    private void StayClicked()
    {
        if (roundOver) return;

        stayClicks++;

        
        if (stayClicks == 1)
        {
            HitDealer();
            RoundOver();
            if (stayBtnText != null) stayBtnText.text = "Call";
        }
    }

    // Dealer logic

    private void HitDealer()
    {
        // open hide card
        if (hideCard != null)
        {
            var r = hideCard.GetComponent<Renderer>();
            if (r != null) r.enabled = false;
        }

        // hit card til 16
        while (dealerScript.handValue < 16 &&
               dealerScript.cardIndex < dealerScript.hand.Length)
        {
            dealerScript.GetCard();
        }

        if (dealerScoreText != null)
        {
            dealerScoreText.gameObject.SetActive(true);
            dealerScoreText.text = "Hand: " + dealerScript.handValue;
        }
    }

    // Round over & payout

    public void RoundOver()
    {
        if (roundOver) return;

        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;

        // If no one bust nor hit 21 and if the player didn't press stay it will not called
        if (!playerBust && !dealerBust && !player21 && !dealer21 && stayClicks == 0)
            return;

        roundOver = true;

        int payout = 0;

        if (playerBust && dealerBust)
        {
            mainText.text = "All bust - bets returned";
            payout = pot;                 // return the bet if both bust
        }
        else if (playerBust || (!dealerBust && dealerScript.handValue > playerScript.handValue))
        {
            mainText.text = "Dealer wins";
            payout = 0;                   // player lose all the bet
        }
        else if (dealerBust || playerScript.handValue > dealerScript.handValue)
        {
            mainText.text = "Player wins!";
            payout = pot * 2;             // win 2x of the bet
        }
        else
        {
            mainText.text = "Push - bets returned";
            payout = pot;                 // push return all the bet in this round
        }

        if (payout > 0)
            playerScript.AdjustMoney(payout);

        if (cashText != null)
            cashText.text = "$" + playerScript.GetMoney();

        pot = 0;
        if (betsText != null)
            betsText.text = "Bets: $0";

        // reset UI
        hitBtn.interactable = false;
        hitBtn.gameObject.SetActive(false);

        stayBtn.interactable = false;
        stayBtn.gameObject.SetActive(false);

        dealBtn.gameObject.SetActive(true);
        dealBtn.interactable = true;

        betBtn.interactable = true;

        if (mainText != null)
            mainText.gameObject.SetActive(true);

        stayClicks = 0;

        if(player21 && !dealer21 && playerScript.cardIndex == 2) {
            mainText.text = "BlackJack!";
            payout = Mathf.RoundToInt(pot * 2.5f);
        }
    }

    
}
