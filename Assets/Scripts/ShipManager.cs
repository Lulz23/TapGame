using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using System;

public class ShipManager : MonoBehaviour
{

    public static ShipManager instance;

    public GameObject MainGameCanvas;
    [SerializeField] private GameObject upgradeCanvas;
    [SerializeField] private TextMeshProUGUI scrapCounter;
    [SerializeField] private TextMeshProUGUI fleetDamagePerSecond;

    //This can be copied to have multiple fleet objects
    [SerializeField] private GameObject shipObj;
    
    
    public GameObject damageTextPopup;
    [SerializeField] private GameObject backgroundObj;


    [Space]
    public FleetUpgrades[] fleetUpgrades;
    [SerializeField] private GameObject upgradeUIToSpawn;
    [SerializeField] private Transform upgradeUIParent;
    public GameObject attackPerSecondObjToSpawn;

    public double currentScrapCount { get; set; }
    public double currentFleetDamagePerSecond { get; set; }

    //Upgrades
    public double fleetDamagePerClickUpgrade {  get; set; }

    //Health
    private double shipHealth = 3.0;
    //Time To Spawn
    private float spawnTime = 2.0f;
    private float timer = 1f;

    private InitializeUpgrades initializeUpgrades;
    private FleetDisplay fleetDisplay;

    private void Awake()
    {
        currentFleetDamagePerSecond = 1;

        if (instance == null) { 
            instance = this;
        }

        fleetDisplay = GetComponent<FleetDisplay>();

        UpdateScrapUI();
        UpdateFleetDamagePerSecondUI();

        upgradeCanvas.SetActive(false);
        MainGameCanvas.SetActive(true);

        initializeUpgrades = GetComponent<InitializeUpgrades>();
        initializeUpgrades.Initialize(fleetUpgrades, upgradeUIToSpawn, upgradeUIParent);
    }

    //Update the scrap counter UI element
    private void UpdateScrapUI() {

        fleetDisplay.UpdateGameText(currentScrapCount, scrapCounter);
    }

    //Update the damage per tap/click UI element
    private void UpdateFleetDamagePerSecondUI() { 
    
        fleetDisplay.UpdateGameText(currentFleetDamagePerSecond, fleetDamagePerSecond, "P/S");

    }


    //Reduce the enemy ship health until 0 and remove it, then spawn a new one
    public void OnShipClicked() {

        ReduceHealth();

        PopupText.Create(1 + fleetDamagePerClickUpgrade);
    }


    //Upgrade UI Interaction
    public void OnUpgradeButtonPress() { 
    
        MainGameCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        
    }

    public void OnResumeButtonPress() { 
    
        upgradeCanvas.SetActive(false);
        MainGameCanvas.SetActive(true);
    }


    //Increase the amount of scrap earned from destroying fleets

    public void ScrapIncrease(double amount) {
        
        //Adjust this method so that damage is increased instead

        //Increase the amount 
        currentScrapCount += amount;
        UpdateScrapUI();

    }

    //Increase the damage done per tap
    public void FleetDamageIncrease(double amount) {

       //Current Bug here sets the health value to a constant value
       //Preventing the object from losing health and dying

        currentFleetDamagePerSecond += amount;
        UpdateFleetDamagePerSecondUI();


    }


    public void OnUpgradeButtonClick(FleetUpgrades upgrades, UpgradeButtonReferences buttonRef) { 
    
        if(currentScrapCount >= upgrades.CurrentUpgradeCost)
        {

            upgrades.ApplyUpgrade();

            currentScrapCount -= upgrades.CurrentUpgradeCost;

            upgrades.CurrentUpgradeCost = Mathf.Round((float)(upgrades.CurrentUpgradeCost * (1 + upgrades.CostIncreaseMultiplierPerUpgrade)));

            buttonRef.upgradeCostText.text = "Cost: " + upgrades.CurrentUpgradeCost;

        }

    }

 

    public void ReduceHealth() {

        if(shipHealth == 0) {

            shipObj.SetActive(false);
            currentScrapCount += 1 + fleetDamagePerClickUpgrade;
            UpdateScrapUI();
        }
        shipHealth = Math.Abs(shipHealth - currentFleetDamagePerSecond);
    }

    private void RespawnShip()
    {
       

        //Can also have logic here to randomize which fleet is active
        shipObj.SetActive(true);
        shipHealth = 5;
        //If the current damage done by tapping is equal to the current ship health
        //Double the current health.
        if(shipHealth == currentFleetDamagePerSecond)
        {
            shipHealth *= 2;
        }

    }

    //To respawn the enemy fleet with the timer
    private void Update()
    {
        timer += Time.deltaTime;

        if (shipObj.activeSelf)
        {
        }

        else {
            if (timer >= spawnTime)
            {
                //Respawn the object
                RespawnShip();
                //Reset the timer
                timer = 0;
            }
        }
        
    }

}
