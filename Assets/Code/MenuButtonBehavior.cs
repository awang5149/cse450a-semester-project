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
    public GameObject ShopManager;

    private int[,] shopItems;

    void Start()
    {
        shopItems = ShopManager.GetComponent<ShopManagerScript>().shopItems;
        PriceTxt.text = "Price: $" + shopItems[1, ItemID].ToString();
        QuantityTxt.text = shopItems[2, ItemID].ToString();
    }
    void Update()
    {
        
        
    }
}