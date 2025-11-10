using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Button dealBtn;
    public Button hitBtn;

    public Button stayBtn;

    public Button betBtn;

    public PlayerScript playerScript;
    public PlayerScript dealerScript;

    void Start()
    {
        dealBtn.onClick.AddListener(() => DealClicked());
        hitBtn.onClick.AddListener(() => HitClicked());
        stayBtn.onClick.AddListener(() => StayClicked());
    }

    // Update is called once per frame
    private void DealClicked()
    {
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();
    }
    private void HitClicked()
    {
        throw new NotImplementedException();
    }
    private void StayClicked()
    {
        throw new NotImplementedException();
    }
}
