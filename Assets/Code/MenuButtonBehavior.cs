using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuButtonBehavior : MonoBehaviour
{
    // used this tut: https://www.youtube.com/watch?v=Oie-G5xuQNA 
    public int ItemID;
    public TMP_Text PriceTxt;
    public TMP_Text QuantityTxt;
    public ShopManagerScript shopManager;
    public int price;

    private int[,] shopItems;

    void Start()
    {
        price = shopManager.shopItems[1, ItemID];
        PriceTxt.text = "$" + price;
        QuantityTxt.text = shopManager.shopItems[2, ItemID].ToString();
    }

    void Update()
    {
        
        
    }
}