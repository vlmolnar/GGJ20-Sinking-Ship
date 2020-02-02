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
    public Text gameOverText;
    public Text gameWinText;
    public GameObject water;
    public GameObject character;
    public WindowScript script;
    public ButtonScript bScript;
    Animator anim;

    //Private variables
    private Rigidbody rb;
    private int plankCount;
    private int ladderCount;
    private const int MAX_LADDER = 6;
    private float wadingModifier;
    private bool canMove;
    private double interactionStart = -10;
    private bool buildingLadder;
    private bool fixingWindow;
    private bool drowned;
    private bool win;
    private bool pressingButton;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = character.GetComponent<Animator>();
        anim.speed = 1.5f;
        canMove = true;
        buildingLadder = false;
        fixingWindow = false;
        pressingButton = false;
        SetCountText();
        wadingModifier = 1.0f;
        drowned = false;
        win = false;
        gameOverText.text = "";
        gameWinText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     /*   if (drowned || win)
        {
            return;
        }*/

        if (!win && water.transform.position.y > 7)
        {
            canMove = false;
            drowned = true;
            gameOverText.text = "You Died";
            return;
        }

        if (!drowned && ladderCount >= MAX_LADDER)
        {
            win = true;
            gameWinText.text = "You Win!";
            return;
        }

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
        else if (pressingButton && Time.realtimeSinceStartup - interactionStart > 3)
        {
            pressingButton = false;
            //waterRising = GameObject.FindGameObjectsWithTag("WaterRising")[0]; //needs a tag
            Debug.Log("Finished pushing button");
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
            anim.Play("Run");
        }
        else
        {
            anim.Play("Idle");
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

    void OnTriggerEnter(Collider other)
    {
        if (!canMove) return;

        if (other.tag == "Button")
        {
            interactionStart = Time.realtimeSinceStartup;
            pressingButton = true;
            Debug.Log("Started pressing button");
        }
   }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Plank: " + plankCount + ", ladder: " + ladderCount);

        if (!canMove)
        {
            anim.Play("Repair");
            return;
        }

        if (other.tag == "Button" && !pressingButton)
        {
            bScript = other.gameObject.GetComponent<ButtonScript>();
            bScript.pressed = true;
        }

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
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Button")
        {
            pressingButton = false;
        }
    }

}
