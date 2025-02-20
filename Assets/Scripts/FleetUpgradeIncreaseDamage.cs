using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fleet Upgrade/Fleet Damage Increase", fileName = "Fleet Damage Increase")]
public class FleetUpgradeAutoDamage : FleetUpgrades
{
    public override void ApplyUpgrade()
    {
       GameObject gameObject = Instantiate(ShipManager.instance.attackPerSecondObjToSpawn, Vector3.zero, Quaternion.identity);
       gameObject.GetComponent<FleetDamagePerSecondTimer>().DamagePerSecond = UpgradeAmount;

        ShipManager.instance.FleetDamageIncrease(UpgradeAmount);
    }
}
