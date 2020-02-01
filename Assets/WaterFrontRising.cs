using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFrontRising : MonoBehaviour
{

    public GameObject water;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - water.transform.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = water.transform.position + offset;
    }

}
