using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityOSC;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;

    bool playingWalkSound = false;
    bool playingSprintSound = false;
    bool isWalking = false;
    bool isSprinting = false;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();

        OSCHandler.Instance.Init();
        //OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", "ready");
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/oscplayocean", 1);
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

        if (shiftKeyDown && (forwardPressed || rightPressed ||leftPressed || backPressed))
        {
            isWalking = false;
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        if((forwardPressed || rightPressed || leftPressed || backPressed) && !shiftKeyDown)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        if (isWalking && !playingWalkSound)
        {
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/playfootstep", 1);
            playingWalkSound = true;
            playingSprintSound = false;

        } else if (isSprinting && !playingSprintSound)
        {
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/playrunstep", 400);
            playingSprintSound = true;
            playingWalkSound = false;

        } else if (!backPressed && !forwardPressed && !leftPressed && !rightPressed && playingWalkSound)
        {
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/playfootstep", 0);
            playingWalkSound = false;
            playingSprintSound = false;
        } else if (!backPressed && !forwardPressed && !leftPressed && !rightPressed && playingSprintSound)
        {
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/playrunstep", 0);
            playingSprintSound = false;
            playingWalkSound = false;
        }

    }
    void OnApplicationQuit()
    {
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/playfootstep", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/oscplayocean", 0);
    }
}
