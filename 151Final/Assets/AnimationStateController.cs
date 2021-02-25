using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        bool forwardPressed = Input.GetKey("w");
        bool shiftKeyDown = Input.GetKey("left shift");
        bool rightPressed = Input.GetKey("d");
        bool leftPressed = Input.GetKey("a");
        bool backPressed = Input.GetKey("s");
        bool jumpPressed = Input.GetButtonDown("Jump");

        // Walk
        if (forwardPressed)
        {
            animator.SetBool("isWalking?", true);
        } else
        {
            animator.SetBool("isWalking?", false);
        }

        // Right Strafe
        if (rightPressed)
        {
            animator.SetBool("isStrafingRight?", true);
        }
        else
        {
            animator.SetBool("isStrafingRight?", false);
        }

        // Left Strafe
        if (leftPressed)
        {
            animator.SetBool("isStrafingLeft?", true);
        }
        else
        {
            animator.SetBool("isStrafingLeft?", false);
        }

        // Walking Backwards
        if (backPressed)
        {
            animator.SetBool("isWalkingBackward?", true);
        }
        else
        {
            animator.SetBool("isWalkingBackward?", false);
        }

        // Run
        if (shiftKeyDown && forwardPressed)
        {
            animator.SetBool("isRunning?", true);
        }
        else
        {
            animator.SetBool("isRunning?", false);
        }

        // Jump
        if (jumpPressed)
        {
            animator.SetBool("isJumping?", true);
        }
        else
        {
            animator.SetBool("isJumping?", false);
        }

    }
}
