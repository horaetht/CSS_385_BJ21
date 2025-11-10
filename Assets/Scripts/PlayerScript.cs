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
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        handValue += cardValue;

        if (cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());
        }
        cardIndex++;
        return handValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
