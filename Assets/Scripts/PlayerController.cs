using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    //public Rigidbody theRB;

    private Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        print("Mechanics");
        //theRB = GetComponent<Rigidbody>();
        //moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        //Also Usefull since it acts directly on the transformation of the object independant of the ridgid body /  transform.Translate(moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime,0f, moveSpeed*Input.GetAxis("Vertical")*Time.deltaTime); 
        //theRB.velocity = new Vector3(Input.GetAxis("Horizontal")*moveSpeed, theRB.velocity.y, Input.GetAxis("Vertical")*moveSpeed);

        /*if(Input.GetButtonDown("Jump"))
        {
            theRB.velocity = new Vector3(theRB.velocity.x, jumpForce, theRB.velocity.z);
        }*/
        movementDirection = new Vector3(Input.GetAxis("Horizontal"), movementDirection.y, Input.GetAxis("Vertical"));
        if (controller.isGrounded)
        {
            movementDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                movementDirection.y = jumpForce;
            }
        }
        movementDirection.y = movementDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        Vector3 mymov = transform.TransformDirection(movementDirection);
        controller.Move((mymov * Time.deltaTime) * moveSpeed);
    }
}
