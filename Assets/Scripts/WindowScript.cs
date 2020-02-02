using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour
{

    public bool isFixed;
    public int roomId;
    public GameObject waterSplash;

    // Start is called before the first frame update
    void Start()
    {
        isFixed = true;
        waterSplash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFixed) waterSplash.SetActive(false);
        else waterSplash.SetActive(true);
    }

}
