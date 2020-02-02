using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public bool pressed;
    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pressed)
        {
            gameObject.SetActive(false);
            Debug.Log("Button hidden");
        }
    }
}
