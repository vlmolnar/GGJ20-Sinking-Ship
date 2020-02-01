using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFrontRising : MonoBehaviour
{

    public GameObject water;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = water.transform.position;
    }
}
