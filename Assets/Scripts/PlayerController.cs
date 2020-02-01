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

    //Private variables
    private Rigidbody rb;
    private int plankCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetCountText();
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

        if (movement != Vector3.zero)
        {
            // Source: https://answers.unity.com/questions/803365/make-the-player-face-his-movement-direction.html
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
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

    void SetCountText()
    {
        countText.text = "Plank x " + plankCount.ToString();
    }

}
