using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_gyro : MonoBehaviour {

    void Start ()
    {
        Input.gyro.enabled = true;
    }

    void Update ()
    {
        this.transform.Rotate (0, -Input.gyro.rotationRateUnbiased.y, -Input.gyro.rotationRateUnbiased.z);
    }

}
