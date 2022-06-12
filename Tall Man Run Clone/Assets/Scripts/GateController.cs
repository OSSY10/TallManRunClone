using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateController : MonoBehaviour
{
    public static GateController instance;
    [SerializeField] TMP_Text gateNumberText = null;
    [SerializeField] int gateNumber;
    [SerializeField] GameObject lenghtArrow;
    [SerializeField] GameObject expendArrow;
    public float gateExpandValue;
    public float gateLenghtValue;
    public float divideValue = 1;
    public float multiplierValue;
    public enum GateType
    {
        Expend,
        Lenght

    }

    public enum GateFeature
    {
        Multiplier,
        Divide,
        Normal
    }

    [SerializeField] public GateFeature gateFeature;
    [SerializeField] public GateType gateType;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        GateNumberText();
        GateImageByType();
    }
    private void Update()
    {
        
    }
    
    void GateNumberText()                      //Changing the sign of the gate number according to the gate feature
    {
        if(gateNumber > 0)
        {
            gateNumberText.text = "+" + gateNumber.ToString();
        }
        else
        {
            gateNumberText.text = gateNumber.ToString();
        }
        if (gateFeature == GateFeature.Multiplier)
        {
            gateNumberText.text = "x" + gateNumber.ToString();
        }
        else if (gateFeature == GateFeature.Divide)
        {
            gateNumberText.text = "/" + gateNumber.ToString();
        }

    }
    void GateImageByType()                       // change gate image by type
    {
        switch(gateType)
        {
            case GateType.Expend:
                lenghtArrow.gameObject.SetActive(false);
                expendArrow.gameObject.SetActive(true);
                break;
            case GateType.Lenght:
                expendArrow.gameObject.SetActive(false);
                lenghtArrow.gameObject.SetActive(true);
                break;
        }
            
    }

}