using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collision : MonoBehaviour
{
    public static Collision instance;
    [SerializeField] List<GameObject> legs;
    [SerializeField] List<GameObject> apparance;
    [SerializeField] public GameObject root;
    [SerializeField] Material greenMaterial;
    [SerializeField] Material redMaterial;
    [SerializeField] Material originalMaterial;
    [SerializeField] float minLenght;
    [SerializeField] float minExpand;
    float value;
    bool changeColorToGreen;
    bool changeColorToRed;
    
    // Start is called before the first frame update
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        value = root.gameObject.transform.localPosition.x;
    }
    private void Update()
    {
        root.transform.DOLocalMoveX(value, 0.4f);
        if (root.transform.localPosition.x > minLenght || root.transform.localScale.z < minExpand)
        {
            Destroy(this.gameObject); //destroy if character gets too small
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Scaling player by gate type and feature
        if (other.gameObject.CompareTag("PositiveGate") || other.gameObject.CompareTag("NegativeGate"))
        {
            GateController gateController = other.gameObject.GetComponent<GateController>();

            if (gateController.gateType == GateController.GateType.Expend)
            {
                switch (gateController.gateFeature)
                {
                    case GateController.GateFeature.Normal:
                        foreach (var item in legs)
                        {
                            item.transform.DOScale(new Vector3(1, item.transform.localScale.y + gateController.gateExpandValue, item.transform.localScale.z + gateController.gateExpandValue), 0.4f);

                        }
                        root.transform.DOScale(new Vector3(root.transform.localScale.x + gateController.gateExpandValue, root.transform.localScale.y + gateController.gateExpandValue, root.transform.localScale.z + gateController.gateExpandValue), 0.4f);

                        break;
                    case GateController.GateFeature.Divide:
                        foreach (var item in legs)
                        {
                            item.transform.DOScale(new Vector3(1, item.transform.localScale.y / gateController.divideValue, item.transform.localScale.z / gateController.divideValue), 0.4f);

                        }
                        root.transform.DOScale(new Vector3(root.transform.localScale.x / gateController.divideValue, root.transform.localScale.y / gateController.divideValue, root.transform.localScale.z / gateController.divideValue), 0.4f);
                        break;
                    case GateController.GateFeature.Multiplier:
                        foreach (var item in legs)
                        {
                            item.transform.DOScale(new Vector3(1, item.transform.localScale.y * gateController.multiplierValue /1.5f, item.transform.localScale.z * (gateController.multiplierValue /1.5f)), 0.4f);

                        }
                        root.transform.DOScale(new Vector3(1, root.transform.localScale.y * gateController.multiplierValue, root.transform.localScale.z * (gateController.multiplierValue /1.5f)), 0.4f);
                        break;
                }
            }

            else if (gateController.gateType == GateController.GateType.Lenght)
            {
                switch (gateController.gateFeature)
                {
                    case GateController.GateFeature.Normal:
                        value = root.gameObject.transform.localPosition.x + gateController.gateLenghtValue;
                        break;
                    case GateController.GateFeature.Divide:
                        value = root.gameObject.transform.localPosition.x / gateController.divideValue;
                        break;
                    case GateController.GateFeature.Multiplier:
                        value = root.gameObject.transform.localPosition.x * gateController.multiplierValue;
                        break;
                }


            }

        }

        if(other.gameObject.CompareTag("PositiveGate"))
        {
            changeColorToGreen = true;
            changeColorToRed = false;
        }
        if(other.gameObject.CompareTag("NegativeGate"))
        {
            changeColorToRed = true;
            changeColorToGreen = false;
        }
        StartCoroutine(ChangePlayerColor());
    }



    IEnumerator ChangePlayerColor()
    {
        if (changeColorToGreen == true)
        {
            foreach (var item in apparance)
            {
                item.gameObject.GetComponent<SkinnedMeshRenderer>().material = greenMaterial;
                
            }
            yield return new WaitForSeconds(0.7f);
            foreach (var item in apparance)
            {
               
                item.gameObject.GetComponent<SkinnedMeshRenderer>().material = originalMaterial;
            }
        }
        else if (changeColorToRed == true)
        {
            foreach (var item in apparance)
            {
                item.gameObject.GetComponent<SkinnedMeshRenderer>().material = redMaterial;

            }
            yield return new WaitForSeconds(0.7f);
            foreach (var item in apparance)
            {

                item.gameObject.GetComponent<SkinnedMeshRenderer>().material = originalMaterial;
            }
        }
    }
}