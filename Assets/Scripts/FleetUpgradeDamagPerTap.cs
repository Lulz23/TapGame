using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fleet Upgrade/Damage Per Tap ", fileName = "Damage Per Tap")]
public class FleetUpgradeDamage : FleetUpgrades
{
    public override void ApplyUpgrade()
    {
        ShipManager.instance.fleetDamagePerClickUpgrade += UpgradeAmount;
    }
}
