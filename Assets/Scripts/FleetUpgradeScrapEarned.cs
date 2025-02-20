using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fleet Upgrade/Scrap Earned ", fileName = "Scrap Earned")]
public class FleetUpgradeDamage : FleetUpgrades
{
    public override void ApplyUpgrade()
    {

        //Change this for damage increase
        ShipManager.instance.fleetDamagePerClickUpgrade += UpgradeAmount;
    }
}
