using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerScript : MonoBehaviour
{
    public CardScript cardScript;
    public DeckScript deckScript;

    public int handValue = 0;

    // chips
    private int money = 1000;

    public GameObject[] hand;

    public int cardIndex = 0;

    //Tracking aces for 1 to 11 coversions
    //List<CardScript> aceList = new List<CardScript>();
    public List<CardScript> aceList;


    void Start()
    {
        aceList = new List<CardScript>();
    }
    public void StartHand()
    {
        GetCard();
        GetCard();

    }

    public int GetCard()
    {
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        // Show card on game screen
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        // Add card value to running total of the hand
        handValue += cardValue;
        // If value is 1, it is an ace
        if (cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());
        }
        //check if we should use an 11 instead of a 1
        //AceCheck());
        cardIndex++;
        return handValue;
    }

    // Search for any ace conversion 
    public void AceCheck()
    {   
        // for each ace in the list check
        foreach(CardScript ace in aceList)
        {
            if(handValue + 10 < 22 && ace.GetValueOfcard() == 11)
            {
                // if converting, adjust card object value and hand
                ace.SetValue(11);
                handValue += 10;
            }else if(handValue > 21 && ace.GetValueOfcard() == 11)
            {
                // if converting, adjust gameobj value and hand value
                ace.SetValue(1);
                handValue -= 10;
            }
        }
    }

    // Add or subtract from money, for bets
    public void AdjustMoney(int amount)
    {
        money += amount;
    }

    //Output  players current money amount
    public int GetMoney()
    {
        return money;
    }
}
