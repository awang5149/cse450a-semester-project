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

    void Update()
    {
        PriceTxt.text = "Price: $" + ShopManager.GetComponent<ShopManagerScript>().shopItems[2, ItemID].ToString();
        QuantityTxt.text = ShopManager.GetComponent<ShopManagerScript>().shopItems[3, ItemID].ToString();
    }
}