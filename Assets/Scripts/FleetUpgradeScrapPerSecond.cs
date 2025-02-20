using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fleet Upgrade/Fleet Scrap Per Second", fileName = "Fleet Scrap Per Second")]
public class FleetUpgradeAutoDamage : FleetUpgrades
{
    public override void ApplyUpgrade()
    {
       GameObject gameObject = Instantiate(ShipManager.instance.attackPerSecondObjToSpawn, Vector3.zero, Quaternion.identity);
        gameObject.GetComponent<FleetDamagePerSecondTimer>().DamagePerSecond = UpgradeAmount;

        ShipManager.instance.FleetDamageIncrease(UpgradeAmount);
    }
}
