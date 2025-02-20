using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FleetDisplay : MonoBehaviour
{
    public void UpdateGameText(double scrapCount, TextMeshProUGUI textToChange, string optionalEndText = null) {

        string[] suffixes = { "", "k", "M", "B", "T", "Q" };

        int index = 0;

        while (scrapCount >= 1000 && index < suffixes.Length -1)
        {
            scrapCount /= 1000;
            index++;

            if (index >= suffixes.Length - 1 && scrapCount >= 1000 ) {

                break;
            }

        }

        string formattedText;

        if (index == 0)
        {
            formattedText = scrapCount.ToString();
        }

        else {
            formattedText = scrapCount.ToString("F1") + suffixes[index];
        
        }

        textToChange.text = formattedText + optionalEndText;

    } 
}
