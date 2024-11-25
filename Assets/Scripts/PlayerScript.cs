using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        PlayerMovement();
        Debug.DrawRay(mainCam.transform.position, transform.TransformDirection(Vector3.forward) * 25, Color.blue);
        if (Input.GetButtonDown("Fire1"))
        {
            gunObject.GetComponent<GunScript>().TriggerGun();
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
