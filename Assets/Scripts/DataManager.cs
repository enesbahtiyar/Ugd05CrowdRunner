using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManager : SingletonMonoBehaviour<DataManager>
{
    [Header("Coin Texts")]
    [SerializeField] private TMP_Text[] coinsTexts;
    private int coins;

    protected override void Awake()
    {
        base.Awake();

        coins = PlayerPrefs.GetInt("coins", 0);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateCoinsText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
            coins = PlayerPrefs.GetInt("coins", 0);
            UpdateCoinsText();
        }
    }

    void UpdateCoinsText()
    {
        foreach(TMP_Text coinText in coinsTexts)
        {
            coinText.text = coins.ToString();
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinsText();
        PlayerPrefs.SetInt("coins", coins);
    }

    public int GetCoins()
    {
        return coins;
    }

    public void UseCoins(int amount)
    {
        coins -= amount;
        UpdateCoinsText();
        PlayerPrefs.SetInt("coins", coins);
    }
}
