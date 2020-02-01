using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankController : MonoBehaviour
{
    public GameObject plank;
    public GameObject plane;
    private bool activePlank;

    // Start is called before the first frame update
    void Start()
    {
        addPlank();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addPlank()
    {
        //    Vector3 min = plane.GetComponent<MeshFilter>().mesh.bounds.min;
        //    Vector3 max = plane.GetComponent<MeshFilter>().mesh.bounds.max;

        //    float randX = Random.Range(min.x + 3, max.x - 3);//plane.transform.position.x - plane.transform.localScale.x / 2, plane.transform.position.x + plane.transform.localScale.x / 2);
        //    float randZ = Random.Range(plane.transform.position.z - plane.transform.localScale.z / 2, plane.transform.position.z + plane.transform.localScale.z / 2);

        float n = 6.5f;
        float randX = Random.Range(plane.transform.position.x - n, plane.transform.position.x + n);
        float randZ = Random.Range(plane.transform.position.z - n, plane.transform.position.z + n);
        Instantiate(plank, new Vector3(randX, 11, randZ), new Quaternion());
        activePlank = true;
    }
}
