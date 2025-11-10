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

    void Start()
    {
        dealBtn.onClick.AddListener(() => DealClicked());
        hitBtn.onClick.AddListener(() => HitClicked());
        stayBtn.onClick.AddListener(() => StayClicked());
    }

    // Update is called once per frame
    private void DealClicked()
    {
        throw new NotImplementedException();

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
