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
    
    
    public GameObject textPopup;
    [SerializeField] private GameObject backgroundObj;


    [Space]
    [SerializeField] private GameObject upgradeUIToSpawn;
    [SerializeField] private Transform upgradeUIParent;
    public GameObject attackPerSecondObjToSpawn;

    public double currentScrapCount { get; set; }
    public double currentFleetDamagePerSecond { get; set; }

    //Upgrades
    public double fleetDamagePerClickUpgrade {  get; set; }

    //Health
    private double shipHealth = 4.0;
    //Time To Spawn
    private float spawnTime = 2.0f;
    private float timer = 0f;

    private void Awake()
    {
        currentFleetDamagePerSecond = 1;

        if (instance == null) { 
            instance = this;
        }

        UpdateScrapUI();
        UpdateFleetDamagePerSecondUI();

        upgradeCanvas.SetActive(false);
        MainGameCanvas.SetActive(true);
    }

    //Update the scrap counter UI element
    private void UpdateScrapUI() { 
       
        scrapCounter.text = currentScrapCount.ToString();
    }

    //Update the damage per tap/click UI element
    private void UpdateFleetDamagePerSecondUI() { 
    
        fleetDamagePerSecond.text = currentFleetDamagePerSecond.ToString() + "P/S";
    }


    //Reduce the enemy ship health until 0 and remove it, then spawn a new one
    public void OnShipClicked() {

        ReduceHealth();
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
        
        //Increase the amount 
        currentScrapCount += amount;
        UpdateScrapUI();

    }

    //Increase the damage done per tap
    public void FleetDamageIncrease(double amount) {

        //Logic here is needed

        //Goals:

        //Initial auto damage is nonexistent

        //Gradually increase the amount of damage done automatically over time

        currentFleetDamagePerSecond += amount;
        UpdateFleetDamagePerSecondUI();


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
                print("Calling Respawn");
                //Respawn the object
                RespawnShip();
                //Reset the timer
                timer = 0;
            }
        }
        
    }

}
