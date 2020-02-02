using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankController : MonoBehaviour
{
    public GameObject button;
    public GameObject plank;
    public GameObject plane;
    private bool isActivePlank;
    private double lastActivePlank;
    private bool isActiveButton;
    private double lastActiveButton;

    // Start is called before the first frame update
    void Start()
    {
        lastActivePlank = 0;
        lastActiveButton = 10;
        addPlank();
    }

    // Update is called once per frame
    void Update()
    {
        if (! plank.gameObject.activeInHierarchy)
        {
            if (isActivePlank)
            {
                isActivePlank = false;
                lastActivePlank = Time.realtimeSinceStartup;
            }
            else if (Time.realtimeSinceStartup - lastActivePlank > 5)
            {
                addPlank();
            }
        }

        if (!button.gameObject.activeInHierarchy)
        {
            if (isActiveButton)
            {
                isActiveButton = false;
                lastActiveButton = Time.realtimeSinceStartup;
            }
            else if (Time.realtimeSinceStartup - lastActiveButton > 15)
            {
                button.SetActive(true);
                isActiveButton = true;
            }
        }
    }

    void addPlank()
    {
        float n = 9f;
        float randX = Random.Range(plane.transform.position.x - n, plane.transform.position.x + n);
        float randZ = Random.Range(plane.transform.position.z - n, plane.transform.position.z + n);
        plank.transform.position = new Vector3(randX, 25, randZ);
        plank.SetActive(true);
        isActivePlank = true;
        //Instantiate(plank, new Vector3(randX, 11, randZ), new Quaternion());
    }

}
