using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Movement Variables
    private float horizontal, vertical;
    public float speed = 5.0f;

    //Camera variables
    private float rotX,rotY,xVelocity,yVelocity;
    public float mouseSensitivity, snappiness, upDownRange;
    public bool cursorFree,retroMovement;
    private Vector3 direction;

    public CharacterController control;
    public GameObject mainCam;

    public Vector3 rotation,move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //consider adding toggle for boomer vs modern camera settings
        //thats if i can get modern to work
        rotX = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotY -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        xVelocity = Mathf.Lerp(xVelocity, rotX, snappiness * Time.deltaTime);
        yVelocity = Mathf.Lerp(yVelocity, rotY, snappiness * Time.deltaTime);

        if (!retroMovement)
        {
            transform.Rotate(0, xVelocity, 0);
            move = new Vector3(horizontal*Time.deltaTime, 0, vertical * Time.deltaTime);
        }
        if (retroMovement)
        {
            this.rotation = new Vector3(0, horizontal * 180 * Time.deltaTime, 0);
            move = new Vector3(0, 0, vertical * Time.deltaTime);
        }

        direction = new Vector3(horizontal, 0, vertical);
        if(!cursorFree) Cursor.lockState = CursorLockMode.Locked;

        
        move = this.transform.TransformDirection(move);
        control.Move(move * speed);
        this.transform.Rotate(this.rotation);
    }
}
