using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Button purchaseButton;
    [SerializeField] private SkinButton[] skinButtons;

    [Header("Skins")]
    [SerializeField] private Sprite[] skins;

    [Header("Pricing")]
    [SerializeField] private int skinPrice;
    [SerializeField] private TMP_Text priceText;

    [Header("Events")]
    public static Action<int> onSkinSelected;

    private void Awake()
    {
        priceText.text = skinPrice.ToString();
    }

    void Start()
    {
        ConfigureButtons();

        UpdatePurchaseButton();

        SelectSkin(GetLastSelectedSkin());

        //SelectSkin(0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            UnlockSkin(Random.Range(0, skinButtons.Length));
    }

    private void ConfigureButtons()
    {
        for(int i = 0; i < skinButtons.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("skinButton" + i) == 1;

            skinButtons[i].Configure(skins[i], unlocked);

            int skinIndex = i;

            skinButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
        }
    }

    public void UnlockSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("skinButton" + skinIndex, 1);
        skinButtons[skinIndex].Unlock();    
    }

    private void UnlockSkin(SkinButton skinButton)
    {
        int skinIndex = skinButton.transform.GetSiblingIndex();
        UnlockSkin(skinIndex);
    }

    private void SelectSkin(int skinIndex)
    {
        for(int i = 0; i < skinButtons.Length; i++)
        {
            if(skinIndex == i)
            {
                skinButtons[i].Select();
            }
            else
            {
                skinButtons[i].DeSelect();
            }
        }


        onSkinSelected?.Invoke(skinIndex);
        SaveLastSelectedSkin(skinIndex);
    }

    public void PurchaseSkin()
    {
        List<SkinButton> skinButtonList = new List<SkinButton>();


        for(int i = 0; i < skinButtons.Length; i++)
        {
            if (!skinButtons[i].IsUnlocked())
            {
                skinButtonList.Add(skinButtons[i]);
            }
        }

        if(skinButtonList.Count <= 0)
        {
            return;
        }

        SkinButton randomSkinButton = skinButtonList[Random.Range(0, skinButtonList.Count)];

        UnlockSkin(randomSkinButton);
        SelectSkin(randomSkinButton.transform.GetSiblingIndex());

        DataManager.Instance.UseCoins(skinPrice);

        UpdatePurchaseButton();
    }



    public void UpdatePurchaseButton()
    {
        if(DataManager.Instance.GetCoins() < skinPrice)
        {
            purchaseButton.interactable = false;
        }
        else
        {
            purchaseButton.interactable = true;
        }
    }

    private int GetLastSelectedSkin()
    {
        return PlayerPrefs.GetInt("lastSelectedSkin", 0);
    }

    private void SaveLastSelectedSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("lastSelectedSkin", skinIndex);
    }
}
