using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{

    public int[,] shopItems = new int[4, 4];
    // call ScoreAndMoneyManager.instance.money
    public TMP_Text MoneyTXT;

    void Start()
    {

        MoneyTXT.text = "Money: " + ScoreAndMoneyManager.instance.money.ToString();

        // id's
        shopItems[1,1] = 1;
        shopItems[1,2] = 2;
        shopItems[1,3] = 3;
        shopItems[1,4] = 4;

        // item prices
        shopItems[2,1] = 10;
        shopItems[2,2] = 20;
        shopItems[2,3] = 30;
        shopItems[2,4] = 40;

        // quantity
        shopItems[3,1] = 0;
        shopItems[3,2] = 0;
        shopItems[3,3] = 0;
        shopItems[3,4] = 0;

    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        
        if (ScoreAndMoneyManager.instance.money >= shopItems[2,ButtonRef.GetComponent<MenuButtonBehavior>().ItemID]){
            ScoreAndMoneyManager.instance.money -= shopItems[2,ButtonRef.GetComponent<MenuButtonBehavior>().ItemID]; // spend money
            shopItems[3,ButtonRef.GetComponent<MenuButtonBehavior>().ItemID]++; // increase quantity of item
            MoneyTXT.text = "Money: " + ScoreAndMoneyManager.instance.money.ToString();
            ButtonRef.GetComponent<MenuButtonBehavior>().QuantityTxt.text = shopItems[3,ButtonRef.GetComponent<MenuButtonBehavior>().ItemID].ToString(); //update text afer purchase
        }
    }
}
