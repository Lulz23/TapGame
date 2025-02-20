using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeUpgrades : MonoBehaviour
{

    public void Initialize(FleetUpgrades[] upgrades, GameObject UIToSpawn, Transform spawnParent) {

        for (int i = 0; i < upgrades.Length; i++)
        {

            int currentIndex = i;

            GameObject gameObject = Instantiate(UIToSpawn, spawnParent);

            upgrades[currentIndex].CurrentUpgradeCost = upgrades[currentIndex].OriginalUpgradeCost;

            UpgradeButtonReferences buttonRef = gameObject.GetComponent<UpgradeButtonReferences>();
            buttonRef.upgradeButtonText.text = upgrades[currentIndex].UpgradeButtonText;
            buttonRef.upgradeDescriptionText.SetText(upgrades[currentIndex].UpgradeButtonDescription, upgrades[currentIndex].UpgradeAmount);
            buttonRef.upgradeCostText.text = "Cost: " + upgrades[currentIndex].CurrentUpgradeCost;

            buttonRef.upgradeButton.onClick.AddListener(delegate { ShipManager.instance.OnUpgradeButtonClick(upgrades[currentIndex], buttonRef); });
        }
    
    }

}
