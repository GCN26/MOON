using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5.0f;
    public float jumpSpeed = 1f;

    public float gravity = .98f;

    public Vector3 inputs = Vector3.zero;
    public Vector3 rotation, move;

    [Header("Camera")]
    private float rotX,rotY,xVelocity,yVelocity;
    public float mouseSensitivity, joyCamSensitivity, snappiness, upDownRange;
    public bool cursorFree,retroMovement;

    [Header("GameObjects")]
    public CharacterController control;
    public GameObject mainCam,gunObject;

    [Header("Items")]
    public bool redKey;
    public bool blueKey;
    public bool yellowKey;

    [Header("UI")]
    public TextMeshProUGUI HPText;
    public Image redKeyUI,blueKeyUI,yellowKeyUI;
    public GameObject FadeToBlack;

    [Header("HP")]
    public float HP;
    private float maxHP = 100;
    public float invulnTimer = 2;
    public float invulnTimerMax = 2;
    public bool dead;
    public float deadTimer;
    public Vector3 deadPos;

    void Start()
    {
        redKeyUI.enabled = false;
        blueKeyUI.enabled = false;
        yellowKeyUI.enabled = false;
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            if (invulnTimer > 0)
            {
                invulnTimer -= Time.deltaTime;
            }
            else if (invulnTimer < 0)
            {
                invulnTimer = 0;
            }
            //Stuff that needs to be done but unsorted
            HPText.text = string.Format("HP: {0}", HP);
            redKeyUI.enabled = redKey;
            blueKeyUI.enabled = blueKey;
            yellowKeyUI.enabled = yellowKey;

            if (HP > maxHP) HP = maxHP;
            if (HP > 0)
            {
                PlayerMovement();
                Debug.DrawRay(mainCam.transform.position, transform.TransformDirection(Vector3.forward) * 25, Color.blue);
                if (Input.GetButtonDown("Fire1"))
                {
                    gunObject.GetComponent<GunScript>().TriggerGun();
                }
            }
            else
            {
                if(deadTimer == 0)
                {
                    deadPos = this.transform.position;
                }
                
                if(deadTimer > 1.334)
                {
                    dead = true;
                }
                else deadTimer += Time.deltaTime;
                this.tag = "Dead";
                HP = 0;
                FadeToBlack.GetComponent<Image>().color = new Color(0, 0, 0, deadTimer);
                this.transform.position = Vector3.Lerp(deadPos, new Vector3(deadPos.x,deadPos.y-.8f,deadPos.z), deadTimer * .75f);
                if (dead)
                {
                 
                }
            }
        }
    }

    public void PlayerMovement()
    {
        //Get movement
        inputs.x = Input.GetAxis("Horizontal") + Input.GetAxis("HorizontalD") * .75f;
        inputs.z = Input.GetAxis("Vertical") + Input.GetAxis("VerticalD") * .75f;
        inputs = Vector3.ClampMagnitude(inputs, 1f);

        //Camera controls
        rotX = Input.GetAxis("Mouse X") * mouseSensitivity + Input.GetAxis("ControllerRightStickX") * joyCamSensitivity;
        rotY -= Input.GetAxis("Mouse Y") * mouseSensitivity + Input.GetAxis("ControllerRightStickY") * joyCamSensitivity;

        xVelocity = Mathf.Lerp(xVelocity, rotX, snappiness * Time.deltaTime);
        yVelocity = Mathf.Lerp(yVelocity, rotY, snappiness * Time.deltaTime);

        //Retro Movement bool
        if (!retroMovement)
        {
            transform.Rotate(0, xVelocity, 0);
            move = new Vector3(inputs.x * speed, move.y, inputs.z * speed);
        }
        if (retroMovement)
        {
            this.rotation = new Vector3(0, inputs.x * 180 * Time.deltaTime, 0);
            move = new Vector3(0, move.y, inputs.z * speed);
        }
        if (!cursorFree) Cursor.lockState = CursorLockMode.Locked;

        //Move
        if (control.isGrounded && Input.GetButton("Jump"))
        {
            move.y = jumpSpeed;
        }
        if (!control.isGrounded)
        {
            move.y -= gravity * Time.deltaTime;
        }

        move = this.transform.TransformDirection(move);
        control.Move(move * Time.deltaTime);
        this.transform.Rotate(this.rotation);
    }

    public void keyCollect(int keyColor)
    {
        if (keyColor == 0) redKey = true;
        if (keyColor == 1) blueKey = true;
        if (keyColor == 2) yellowKey = true;
    }
}
