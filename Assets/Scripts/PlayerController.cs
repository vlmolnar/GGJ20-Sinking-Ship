using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Public variables
    public float speed;
    public int playerId;
    public Text countText;
    public GameObject water;

    //Private variables
    private Rigidbody rb;
    private int plankCount;
    private float wadingModifier;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetCountText();
        wadingModifier = 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal, moveVertical;
        if (playerId == 1)
        {
            moveHorizontal = Input.GetAxis("Horizontal1");
            moveVertical = Input.GetAxis("Vertical1");
        } else
        {
            moveHorizontal = Input.GetAxis("Horizontal2");
            moveVertical = Input.GetAxis("Vertical2");
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (water.transform.position.y > 0)
        {
            wadingModifier = 1 - (water.transform.position.y / 10);
        }

        if (movement != Vector3.zero)
        {
            // Source: https://answers.unity.com/questions/803365/make-the-player-face-his-movement-direction.html
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            transform.Translate(movement * speed * wadingModifier * Time.deltaTime, Space.World);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Plank")
        {
            collision.gameObject.SetActive(false);
            plankCount += 1;
            SetCountText();

        }

    }

    void OnTriggerEnter(Collider trigger)
    {
        //action = Input.GetAxis("Horizontal1");
        if (trigger.gameObject.CompareTag("Window"))
        {
            trigger.gameObject.SetActive(false);
            //count += 1;
            //SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Plank x " + plankCount.ToString();
    }

}
