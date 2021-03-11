using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityOSC;

public class PlayerMovement : MonoBehaviour
{
    public float rayRange = 4;

    public CharacterController controller;

    public float speed = 2f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", "ready");
        
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();

        bool shiftKeyDown = Input.GetKey("left shift");
        bool forwardPressed = Input.GetKey("w");

        // Run
        if (shiftKeyDown && forwardPressed)
        {
            speed = 6f;
        }
        else
        {
            speed = 2f;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // Handle Jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);


        if (Input.GetKey("f"))
        {
            
        }
    }
    
    void CastRay()
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, rayRange);
        if (hit)
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            if (Input.GetMouseButtonDown(0))
            {
                hitObject.GetComponent<IInteractable>().Interact();
            }
        }
    }
}
