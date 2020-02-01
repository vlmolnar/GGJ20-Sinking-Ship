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
    public Text ladderText;
    public GameObject water;

    //Private variables
    private Rigidbody rb;
    private int plankCount;
    private int ladderCount;
    private const int MAX_LADDER = 5;
    private float wadingModifier;
    private bool canMove;
    private double interactionStart = -10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canMove = true;
        SetCountText();
        wadingModifier = 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!canMove)
        {
            if (Time.realtimeSinceStartup - interactionStart < 3) return;
            else canMove = true;
        }

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

    void SetCountText()
    {
        countText.text = "Plank x " + plankCount.ToString();
        ladderText.text = "Ladder x " + ladderCount.ToString() + "/" + MAX_LADDER;
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Plank: " + plankCount + ", ladder: " + ladderCount);

        if (!canMove) return;

        if ((playerId == 1 && Input.GetKeyDown("space")) || (playerId == 2 && Input.GetKeyDown(KeyCode.RightShift)))
        {
            if (other.tag == "LadderTrigger" && plankCount > 0)
            {
                canMove = false;
                interactionStart = Time.realtimeSinceStartup;
                plankCount -= 1;
                ladderCount += 1;
                SetCountText();
                Debug.Log("Interacting with ladder");
            }
            else if (other.tag == "Window")
            {
                //TODO
                /*if (canMove)
                {
                    interactionStart = Time.realtimeSinceStartup;
                    canMove = false;
                }
                if (!canMove)
                {
                    Time.realtimeSinceStartup - lastActive > 2;
                }
                

                
                if (isActive)
                {
                    isActive = false;
                    lastActive = Time.realtimeSinceStartup;
                }
                else if ()
                {
                    //fix window
                    canMove = true;
                }
                Debug.Log("Interacting with window");*/
            }

        }
    }

}
