using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopManagerScript : MonoBehaviour
{
    public HamtoroController hamtoroController;
    public GameController gameController;
    public int[,] shopItems = new int[3, 4];
    // call ScoreAndMoneyManager.instance.money
    public TMP_Text MoneyTXT;
    
    // vars for ammo cap:
    public TMP_Text ammocap_priceTXT;
    public int ammocap;
    public TMP_Text ammocap_capTXT;
    public int numammocappurchases = 0;

    void Awake()
    {
        UpdateMoney();
        GameController.instance.UpdateHighScoreAndTotalCurrency(); 
        Debug.Log("total currency: " + GameController.instance.totalCurrency);
        // id's
        shopItems[0,0] = 0; // ammo cap
        shopItems[0,1] = 1; // ammo reward
        shopItems[0,2] = 2; // vacuum length
        shopItems[0,3] = 3; // invisiblity cloak (rn it says boost)

        // item prices
        shopItems[1,0] = 25; // ammo cap price
        shopItems[1,1] = 30; // ammo reward price
        shopItems[1,2] = 35; // vacuum length price
        shopItems[1,3] = 30; // invisibility cloak price

        // quantity
        shopItems[2,0] = 0;
        shopItems[2,1] = 0;
        shopItems[2,2] = 0;
        shopItems[2,3] = 0;
    }

    public void Buy()
    {
        GameObject buttonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        
        int itemID = buttonRef.GetComponent<MenuButtonBehavior>().ItemID;
        int price = shopItems[1, itemID]; 
        
        if (ScoreAndMoneyManager.instance.money >= price){
            ScoreAndMoneyManager.instance.money -= price; // spend money
            // shopItems[2, itemID]++; // increase quantity of item
            UpdateMoney();
            if (itemID == 0){ // if player puchased ammo cap upgrade
                hamtoroController.IncreaseAmmoCapacity(2); // each time player purchases ammo cap, it ups by 2. 
                increaseUpgradePrice(price, numammocappurchases);
                numammocappurchases += 1; 
                upgradePriceUIAfterPurchase(ammocap_priceTXT, price);
            }
            // buttonRef.GetComponent<MenuButtonBehavior>().QuantityTxt.text = shopItems[2, itemID].ToString(); //update text afer purchase
            GameController.instance.UpdateDisplay();
        }
    }

    void OnEnable(){ // called everytime the GameObject is activated aka player opens the update menu
        Debug.Log("Calling UpdateMoney() inside onEnable()");
        UpdateMoney(); 
    }

    public void UpdateMoney()
    {
        Debug.Log("inside UpdateMoney()");
        Debug.Log("Total Currency: " + gameController.totalCurrency);
        MoneyTXT.text = "Currency: " + gameController.totalCurrency.ToString();
    }

    // upgrade price by exponentially increasing w each purchase
    private void increaseUpgradePrice(double upgradeprice, int numberofpurchases){
        upgradeprice = upgradeprice * Math.Pow(1.1, numberofpurchases);
    }
    // upgrade price text
    private void upgradePriceUIAfterPurchase(TMP_Text priceToUpdate_TXT, int priceToUpdate){
        priceToUpdate_TXT.text = "Price: " + priceToUpdate.ToString();
    }
}
