using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRising : MonoBehaviour
{

    private float waterSpeedModifier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.CompareTag("Water1"))
        {
            waterSpeedModifier = 2.2f;
        }

        transform.position += Vector3.up * 0.005f * waterSpeedModifier;
    }
}
