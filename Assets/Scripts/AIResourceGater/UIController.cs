using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI woodText;
    private void Awake()
    {
        GameResource.OnResourceAmountChanged += delegate (object sender, EventArgs e)
        {
            UpdateText();
        };
        UpdateText();
    }
    private void UpdateText()
    {
        goldText.text = "Gold: " + GameResource.GetResourceAmount(GameResource.ResourceType.Gold);
        woodText.text = "Wood: " + GameResource.GetResourceAmount(GameResource.ResourceType.Wood);
    }
}
