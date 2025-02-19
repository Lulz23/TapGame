using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fleet Upgrade/Damage Per Click, ", fileName = "Damage Per Click")]
public class FleetUpgradeDamage : FleetUpgrades
{
    public override void ApplyUpgrade()
    {
        ShipManager.instance.fleetDamagePerClickUpgrade += UpgradeAmount;
    }
}
