using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

    public double totalScrapeEarned;

    public bool[] upgradesUnlocked;

    public GameData() { 
        totalScrapeEarned = 0;

        //Current upgrades available in array is 2
        upgradesUnlocked = new bool[1];
    }
}
