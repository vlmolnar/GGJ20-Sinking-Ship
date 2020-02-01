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
    public WindowScript script;

    //Private variables
    private Rigidbody rb;
    private int plankCount;
    private int ladderCount;
    private const int MAX_LADDER = 5;
    private float wadingModifier;
    private bool canMove;
    private double interactionStart = -10;
    private bool buildingLadder;
    private bool fixingWindow;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canMove = true;
        buildingLadder = false;
        fixingWindow = false;
        SetCountText();
        wadingModifier = 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fixingWindow)
        {
            if (Time.realtimeSinceStartup - interactionStart < 2)
            {
                //Debug.Log(Time.realtimeSinceStartup - interactionStart);
                return;
            }
            else
            {
                script.isFixed = true;
                fixingWindow = false;
                canMove = true;
                Debug.Log("Finished fixing window");
            }

        }
        else if (buildingLadder)
        {
            if (Time.realtimeSinceStartup - interactionStart < 3)
            {
                //Debug.Log(Time.realtimeSinceStartup - interactionStart);
                return;
            }
            else
            {
                plankCount -= 1;
                ladderCount += 1;
                SetCountText();
                buildingLadder = false;
                canMove = true;
                Debug.Log("Finished using plank for ladder");
            }
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
                buildingLadder = true;
                Debug.Log("Started using plank for ladder");
            }
            else if (other.tag == "Window") // and window is broken
            {
                script = other.gameObject.GetComponent<WindowScript>();
                if (!script.isFixed)
                {
                    canMove = false;
                    interactionStart = Time.realtimeSinceStartup;
                    fixingWindow = true;
                    Debug.Log("Started fixing window");
                }
                else
                {
                    Debug.Log("Window is already fixed");
                }
               
                //fix window
                
            }

        }
    }

}
