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
    [SerializeField] private TextMeshProUGUI screenTimer;
    [SerializeField] private TextMeshProUGUI enemyFleetHealth;

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
    private double maxShipHealth = 3.0;
    //private double damage = 1;

    //Time To Spawn
    private float spawnTime = 3f;
    private float timer = 1f;

    //Timer for progression
    private float killTimer = 5;

    //Timer for ad display
    private float adTimer = 1f;

    private InitializeUpgrades initializeUpgrades;
    private FleetDisplay fleetDisplay;

    //For saving and loading game data
    public GameData gameData;

    private void Awake()
    {
        gameData = SaveSystem.Load();
        if (instance == null) { 
            instance = this;
        }

        fleetDisplay = GetComponent<FleetDisplay>();

        UpdateScrapUI();
        UpdateFleetDamagePerSecondUI();
        UpdateOnScreenHealth();

        upgradeCanvas.SetActive(false);
        MainGameCanvas.SetActive(true);

        initializeUpgrades = GetComponent<InitializeUpgrades>();
        initializeUpgrades.Initialize(fleetUpgrades, upgradeUIToSpawn, upgradeUIParent);
    }

    //Update the scrap counter UI element
    private void UpdateScrapUI() {

        fleetDisplay.UpdateGameText(currentScrapCount, scrapCounter, " Total Scrap Earned");
    }

    //Update the damage per tap/click UI element
    private void UpdateFleetDamagePerSecondUI() { 
    
        fleetDisplay.UpdateGameText(currentFleetDamagePerSecond, fleetDamagePerSecond, " Coming Soon!!");

    }

    //Update on screen timer
    private void UpdateScreenTimer() {
        screenTimer.text = "Destroy the fleet before time runs out! " + killTimer;
    }

    //Update on screen health
    private void UpdateOnScreenHealth() {
        enemyFleetHealth.text = "Health Remaining: " + shipHealth;
    }

    //Reduce the enemy ship health until 0 and remove it, then spawn a new one
    public void OnShipClicked() {

        ReduceHealth();
        PopupText.Create(fleetDamagePerClickUpgrade + 1);
        UpdateOnScreenHealth();
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

        //Current bug, not currently applying upgrade

        //Increase the amount of scrap earned from
        //currentScrapCount += amount;
        fleetDamagePerClickUpgrade += amount;
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
            UpdateScrapUI();   

            upgrades.CurrentUpgradeCost = Mathf.Round((float)(upgrades.CurrentUpgradeCost * (1 + upgrades.CostIncreaseMultiplierPerUpgrade)));

            buttonRef.upgradeCostText.text = "Cost: " + upgrades.CurrentUpgradeCost;
        }

    }

 

    public void ReduceHealth() {

        if(shipHealth <= 0) {
            shipObj.SetActive(false);
            currentScrapCount += 1;
            UpdateScrapUI();

            //Save the current scrap earned
            gameData.totalScrapeEarned += currentScrapCount;
            SaveSystem.Save(gameData);
        }
        shipHealth = (shipHealth - (fleetDamagePerClickUpgrade + 1));
    }

    private void RespawnShip()
    {
       

        //Can also have logic here to randomize which fleet is active
        shipObj.SetActive(true);
        //If the current damage done by tapping is equal to the current ship health
        //Double the current health.
        if(maxShipHealth <= fleetDamagePerClickUpgrade)
        {
            maxShipHealth *= 2;
        }

        shipHealth = maxShipHealth;
        UpdateOnScreenHealth();
    }

    //To respawn the enemy fleet with the timer
    private void Update()
    {
        timer += Time.deltaTime;

        killTimer -= Time.deltaTime;

        //Reduce a timer to incentivise player to act
        if(killTimer >=0) {
            UpdateScreenTimer();
            //If timer reaches below 1, reset timer
            if(killTimer <= 1) {
                killTimer = 5;
                UpdateScreenTimer();
                //Reduce the maximum health, down until minimum of 3
                if(maxShipHealth > 3) {
                    maxShipHealth = (float)(maxShipHealth / 2);
                }
                //Respawn the ship
                RespawnShip();
            }
        }

        if (timer >= spawnTime && !(shipObj.activeSelf))
        {
            //Respawn the object
            RespawnShip();
            //Reset the timer
            timer = 1;
        }


    }

}
