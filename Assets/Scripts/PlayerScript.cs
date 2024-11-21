using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Movement Variables
    private float horizontal, vertical;
    public float speed = 5.0f;

    //Camera variables
    public bool cursorFree;
    private Vector3 direction;

    public CharacterController control;
    public GameObject mainCam;

    public Vector3 rotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        direction = new Vector3(horizontal, 0, vertical);
        if(!cursorFree) Cursor.lockState = CursorLockMode.Locked;

        this.rotation = new Vector3(0, horizontal * 180 * Time.deltaTime, 0);

        Vector3 move = new Vector3(0, 0, vertical * Time.deltaTime);
        move = this.transform.TransformDirection(move);
        control.Move(move * speed);
        this.transform.Rotate(this.rotation);
    }
}
