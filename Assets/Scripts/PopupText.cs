using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{

    [SerializeField] private float startingVelocity  = 750f;
    [SerializeField] private float velocityDecayRate = 1500f;
    [SerializeField] private float timeBeforeFadeStarts = 0.6f;
    [SerializeField] private float fadeSpeed = 3f;

    private TextMeshProUGUI clickAmountText;

    private Vector2 currentVelocity;

    private Color startColor;
    private float timer;
    private float textAlpha;

    private void OnEnable()
    {
        clickAmountText = GetComponent<TextMeshProUGUI>();

        Color newColor = clickAmountText.color;
        newColor.a = 1f;
        clickAmountText.color = newColor;

        startColor = newColor;
        timer = 0f;
        textAlpha = 1f;
    }

    public static PopupText Create(double amount) { 
    
        GameObject popupObject = ObjectPool.SpawnObject(ShipManager.instance.damageTextPopup, ShipManager.instance.MainGameCanvas.transform);
        popupObject.transform.position = ShipManager.instance.MainGameCanvas.transform.position;
        
        PopupText damagePopUp = popupObject.GetComponent<PopupText>();
        damagePopUp.Init(amount);

        return damagePopUp;
    }

    public void Init(double amount)
    {
        clickAmountText.text = "-" + amount.ToString("0");

        float randomX = Random.Range(-300f, 300f);
        currentVelocity = new Vector2(randomX, startingVelocity);

    }

    private void Update()
    {

        currentVelocity.y -= Time.deltaTime * velocityDecayRate;
        transform.Translate(currentVelocity * Time.deltaTime);

        timer += Time.deltaTime;
        if(timer > timeBeforeFadeStarts)
        {

            textAlpha -= Time.deltaTime * fadeSpeed;
            startColor.a = textAlpha;
            clickAmountText.color = startColor;

            if (textAlpha <= 0f) { 
            
                ObjectPool.ReturnObjectToPool(gameObject);
            }
        }

    }

}
