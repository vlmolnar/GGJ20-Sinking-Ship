using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public bool pressed;
    public BreakWindowScript script;
    public int roomId;
    private double lastPressTime;
    private bool isActive;
   // private double lastActive;

    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
        lastPressTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (pressed)
        {
           
            script = gameObject.GetComponent<BreakWindowScript>();
            script.breakWindows(roomId);
            gameObject.SetActive(false);
            pressed = false;
            isActive = false;
            Debug.Log("Button hidden");
   
        } 
    }
}
