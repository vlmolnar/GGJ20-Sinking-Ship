using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankController : MonoBehaviour
{
    public GameObject plank;
    public GameObject plane;
    private bool isActive;
    private double lastActive;

    // Start is called before the first frame update
    void Start()
    {
        lastActive = 0;
        addPlank();
    }

    // Update is called once per frame
    void Update()
    {
        if (! plank.gameObject.activeInHierarchy)
        {
            if (isActive)
            {
                isActive = false;
                lastActive = Time.realtimeSinceStartup;
            }
            else if (Time.realtimeSinceStartup - lastActive > 5)
            {
                addPlank();
            }
        }
    }

    void addPlank()
    {
        float n = 6.5f;
        float randX = Random.Range(plane.transform.position.x - n, plane.transform.position.x + n);
        float randZ = Random.Range(plane.transform.position.z - n, plane.transform.position.z + n);
        plank.transform.position = new Vector3(randX, 25, randZ);
        plank.SetActive(true);
        isActive = true;
        //Instantiate(plank, new Vector3(randX, 11, randZ), new Quaternion());
    }
}
