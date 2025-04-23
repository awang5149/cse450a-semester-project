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
    public int[,] shopItems = new int[3, 4];
    // call ScoreAndMoneyManager.instance.money
    public TMP_Text MoneyTXT;
    
    // vars for ammo cap:
    public TMP_Text ammocap_priceTXT;
    public TMP_Text ammocap_capTXT;

    int numammocappurchases = 0;

    // vars for ammo reward:
    public TMP_Text ammoreward_priceTXT;
    public TMP_Text ammorewardTXT;

    int numammorewardpurchases = 0;

    // vars for power up duration:
    public PowerupManager PowerupManager;
    public TMP_Text powerupduration_priceTXT;
    public TMP_Text powerupduration_TXT;

    int numpowerupdurationpurchases = 0;

    void Awake()
    {
        UpdateMoney();
        GameController.instance.UpdateUpgrades();
        updateCapUIAfterPurchase(ammocap_capTXT);
        updateDurationUIAfterPurchase(powerupduration_TXT);
    
        // id's
        shopItems[0,0] = 0; // ammo cap
        shopItems[0,1] = 1; // ammo reward
        shopItems[0,2] = 2; // power up length
        
        for (int i = 0; i < 3; i++)
        {
            shopItems[1, i] = GameController.instance.shopPrices[i];
        }
        updatePriceUIAfterPurchase(ammocap_priceTXT, shopItems[1, 0]);
        updatePriceUIAfterPurchase(ammoreward_priceTXT, shopItems[1, 1]);
        updatePriceUIAfterPurchase(powerupduration_priceTXT, shopItems[1, 2]);
    }

    public void Buy()
    {
        //gameController.UpdateHighScoreAndTotalCurrency();
        GameObject buttonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        
        int itemID = buttonRef.GetComponent<MenuButtonBehavior>().ItemID;
        int oldprice = shopItems[1, itemID]; 
        Debug.Log("Inside Buy() and itemID is " + itemID);
        Debug.Log("Item Price: " + oldprice);
        
        if (GameController.instance.totalCurrency >= oldprice){
            GameController.instance.totalCurrency -= oldprice; // spend money

            UpdateMoney();
            
            // ammo cap upgrade
            if (itemID == 0){
                numammocappurchases ++; 
                int newPrice = increaseUpgradePrice(oldprice, numammocappurchases);
                shopItems[1, itemID] = newPrice;
                hamtoroController.UpdateMaxAmmoCapacity(); // increase cap by 2
                updatePriceUIAfterPurchase(ammocap_priceTXT, newPrice);
                updateCapUIAfterPurchase(ammocap_capTXT);
            }

            // ammo reward upgrade
            if (itemID == 1){
                Debug.Log("purchased ammo reward upgrade");
                numammorewardpurchases ++; 
                int newPrice = increaseMaxAmmoUpgradePrice(oldprice, numammorewardpurchases);
                shopItems[1, itemID] = newPrice;
                hamtoroController.UpdateMaxAmmoReward();
                Debug.Log("AAAAAA: " + hamtoroController.GetMaxAmmoReward());// increase reward by 1
                updatePriceUIAfterPurchase(ammoreward_priceTXT, newPrice);
                Debug.Log("ammoreward: " + newPrice);
                updateRewardUIAfterPurchase(ammorewardTXT);
            }

            // power up duration upgrade
            if (itemID == 2){
                Debug.Log("purchased power up duration upgrade");
                numpowerupdurationpurchases ++; 
                int newPrice = increaseUpgradePrice(oldprice, numpowerupdurationpurchases);
                shopItems[1, itemID] = newPrice;
                PowerupManager.UpdatePowerupDuration(2); // increase power up duration by 1 second
                updatePriceUIAfterPurchase(powerupduration_priceTXT, newPrice);
                updateDurationUIAfterPurchase(powerupduration_TXT);
            }
            GameController.instance.shopPrices[itemID] = shopItems[1, itemID];

            GameController.instance.UpdateDisplay();
        }
        // PLAYER DOES NOT AHVE EHOUGH MONEY TO MAKE THIS PURCHASE! LOG ERROR
        else{
            Debug.Log("not enough money to purchase this upgrade!");
            // add message that is visible on the ui too so that the player knows
        }
    }

    void OnEnable(){ // called everytime the GameObject is activated aka player opens the update menu
        Debug.Log("Calling UpdateMoney() inside onEnable()");
        UpdateMoney(); 
        
        updateCapUIAfterPurchase(ammocap_capTXT);
        updateRewardUIAfterPurchase(ammorewardTXT);
        updateDurationUIAfterPurchase(powerupduration_TXT);

        updatePriceUIAfterPurchase(ammocap_priceTXT, shopItems[1, 0]);
        updatePriceUIAfterPurchase(ammoreward_priceTXT, shopItems[1, 1]);
        updatePriceUIAfterPurchase(powerupduration_priceTXT, shopItems[1, 2]);
    }

    // method for updating the totalcurrency displayed at the top of the upgrade menu 
    public void UpdateMoney()
    {
        Debug.Log("SHopManager totalCurrency: " + GameController.instance.totalCurrency);
        MoneyTXT.text = "Currency: " + GameController.instance.totalCurrency.ToString();
    }

    // update price by exponentially increasing w each purchase
    private int increaseUpgradePrice(double upgradeprice, int numberofpurchases){
        Debug.Log("upgradeprice: " + upgradeprice);
        upgradeprice = upgradeprice * Math.Pow(1.1, numberofpurchases);
        return (int) upgradeprice;
    }

    // max ammo upgrade price increases much faster
    private int increaseMaxAmmoUpgradePrice(double upgradeprice, int numberofpurchases){
        Debug.Log("upgradeprice: " + upgradeprice);
        upgradeprice = upgradeprice * Math.Pow(1.7, numberofpurchases);
        return (int) upgradeprice;
    }

    // update price UI after purchase
    private void updatePriceUIAfterPurchase(TMP_Text priceToUpdate_TXT, int priceToUpdate){
        Debug.Log("updating UI inside updatePriceUIAfterPurchase()");
        Debug.Log("priceToUpdate: " + priceToUpdate);
        priceToUpdate_TXT.text = "$" + priceToUpdate.ToString();
    }

    // update ammo cap UI after purchase
    private void updateCapUIAfterPurchase(TMP_Text newCap_TXT){
        newCap_TXT.text = "cap: " + hamtoroController.GetMaxAmmoCapacity().ToString();  
    }

    // update ammo reward UI after purchase
    private void updateRewardUIAfterPurchase(TMP_Text newReward_TXT){
        newReward_TXT.text = "ammo reward: " + hamtoroController.GetMaxAmmoReward().ToString();
    }

    private void updateDurationUIAfterPurchase(TMP_Text oldDuration_TXT){
        oldDuration_TXT.text = "duration: " + PowerupManager.GetPowerupLength();
    }
}
