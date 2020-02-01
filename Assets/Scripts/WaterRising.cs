using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRising : MonoBehaviour
{
    public WindowScript script;

    private float waterSpeedModifier = 1.0f;
    private GameObject[] windowArray;
    private int brokenWindowCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        brokenWindowCount = 0;

        windowArray = GameObject.FindGameObjectsWithTag("Window");

        for (int i = 0; i < windowArray.Length; i++)
        {
            script = windowArray[i].GetComponent<WindowScript>();
            if ((gameObject.CompareTag("Water1") && script.roomId == 1)
             || (gameObject.CompareTag("Water2") && script.roomId == 2))
            {
                if (!script.isFixed)
                { brokenWindowCount++; }
            }
        }
        //Debug.Log(brokenWindowCount);

        waterSpeedModifier = (float)(brokenWindowCount / 3.0);

        if (brokenWindowCount == 0)
        {
            transform.position += Vector3.down * 0.001f;
        }
        else
        {
            transform.position += Vector3.up * 0.003f * waterSpeedModifier;
        }
    }
}
