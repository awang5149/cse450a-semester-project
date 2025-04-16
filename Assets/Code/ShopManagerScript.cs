using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[3, 4];
    // call ScoreAndMoneyManager.instance.money
    public TMP_Text MoneyTXT;
    

    void Awake()
    {
        UpdateMoney();
        // id's
        shopItems[0,0] = 1;
        shopItems[0,1] = 2;
        shopItems[0,2] = 3;
        shopItems[0,3] = 4;

        // item prices
        shopItems[1,0] = 10;
        shopItems[1,1] = 10;
        shopItems[1,2] = 30;
        shopItems[1,3] = 40;

        // quantity
        shopItems[2,0] = 0;
        shopItems[2,1] = 0;
        shopItems[2,2] = 0;
        shopItems[2,3] = 0;
    }

    public void Buy()
    {
        GameObject buttonRef = GameObject.FindGameObjectWithTag("Event")
            .GetComponent<EventSystem>().currentSelectedGameObject;
        
        int itemID = buttonRef.GetComponent<MenuButtonBehavior>().ItemID;
        int price = shopItems[1, itemID];
        
        if (ScoreAndMoneyManager.instance.money >= price){
            ScoreAndMoneyManager.instance.money -= price; // spend money
            shopItems[2, itemID]++; // increase quantity of item
            UpdateMoney();
            buttonRef.GetComponent<MenuButtonBehavior>().QuantityTxt.text = shopItems[2, itemID].ToString(); //update text afer purchase
            GameController.instance.UpdateDisplay();
        }
    }

    private void UpdateMoney()
    {
        MoneyTXT.text = "Money: " + ScoreAndMoneyManager.instance.money.ToString();
    }
}
