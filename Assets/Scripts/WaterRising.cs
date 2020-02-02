using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRising : MonoBehaviour
{
    public WindowScript script;

    private float waterSpeedModifier = 1.0f;
    private GameObject[] windowArray;
    private int brokenWindowCount;
    private double timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
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

        breakWindows();

        waterSpeedModifier = (float)(brokenWindowCount / 1.6);

        if (transform.position.y < 9)
        {
            if (brokenWindowCount == 0)
            {
                transform.position += Vector3.up * 0.001f;
            }
            else
            {
                transform.position += Vector3.up * 0.003f * waterSpeedModifier;
            }
        }
    }

    void breakWindows()
    {
        if (Time.realtimeSinceStartup - timer > 8 && brokenWindowCount < 4)
        {
            if (Time.realtimeSinceStartup - timer > 8.2)
            {
                timer = Time.realtimeSinceStartup;
                return;
            }

            bool found = false;
            while (!found)
            {
                int randNum = Mathf.RoundToInt(Random.Range(0, 8));
                //Debug.Log(randNum);
                script = windowArray[randNum].GetComponent<WindowScript>();
                if ((gameObject.CompareTag("Water1") && script.roomId == 1)
                 || (gameObject.CompareTag("Water2") && script.roomId == 2))
                {
                    if (script.isFixed)
                    {
                        script.isFixed = false;
                        found = true;
                        timer = Time.realtimeSinceStartup;
                        Debug.Log("Window broken");
                    }
                    else
                    {
                        //Debug.Log("no");
                    }
                }
            }
        }
    }
}
