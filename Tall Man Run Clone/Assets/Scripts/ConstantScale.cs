using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ConstantScale : MonoBehaviour
{

    [SerializeField] float FixedScale = 1;
    [SerializeField] GameObject parent;

    // Update is called once per frame
    void Update()
    {
        // Keeping the scale of some limbs constant
        transform.localScale = new Vector3(FixedScale / parent.transform.localScale.x, FixedScale / parent.transform.localScale.y, FixedScale / parent.transform.localScale.z);

    }
}