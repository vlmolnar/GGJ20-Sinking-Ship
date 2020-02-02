using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWindowScript : MonoBehaviour
{
    public WindowScript script;

    private GameObject[] windowArray;
    private int brokenWindowCount;

    public void breakWindows(int roomId)
    {

        brokenWindowCount = 0;

        windowArray = GameObject.FindGameObjectsWithTag("Window");

        for (int i = 0; i < windowArray.Length; i++)
        {
            script = windowArray[i].GetComponent<WindowScript>();
            if ((roomId == 1 && script.roomId == 2)
            || (roomId == 2 && script.roomId == 1)) // swapped for destroying windows in the other room
            {
                if (!script.isFixed)
                { brokenWindowCount++; }
            }
        }

        if (brokenWindowCount < 4)
        {
            /*if (Time.realtimeSinceStartup - timer > 5.2)
            {
                timer = Time.realtimeSinceStartup;
                return 0;
            }*/

            bool found = false;
            while (!found)
            {
                int randNum = Mathf.RoundToInt(Random.Range(0, 8));
                //Debug.Log(randNum);
                script = windowArray[randNum].GetComponent<WindowScript>();
                if ((roomId == 1 && script.roomId == 2)
                 || (roomId == 2 && script.roomId == 1))
                {
                    if (script.isFixed)
                    {
                        script.isFixed = false;
                        found = true;
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
