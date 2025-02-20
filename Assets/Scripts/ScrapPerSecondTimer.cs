using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetDamagePerSecondTimer : MonoBehaviour
{
    public float TimerDuration = 1f;

    public double DamagePerSecond {  get; set; }

    private float counter;

    private void Update()
    {
        counter += Time.deltaTime;

        if (counter >= TimerDuration) {

            ShipManager.instance.FleetDamageIncrease(DamagePerSecond);
            counter = 0;
        }
    }

}
