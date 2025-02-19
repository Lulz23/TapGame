using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipAlphaThreshold : MonoBehaviour
{
    private Image shipImage;


    private void Awake()
    {
        shipImage = GetComponent<Image>();
        shipImage.alphaHitTestMinimumThreshold = 0.001f;

    }

}
