using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityOSC;

public class InteractableCube : MonoBehaviour, IInteractable
{
    int channelNumber = 1;
    bool channel3Playing = false;

    public void Interact()
    {
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/playchangeradio", 1);
        StartCoroutine(ChangeRadio());
    }

    IEnumerator ChangeRadio()
    {
        yield return new WaitForSeconds(1);
        switch (channelNumber)
        {
            case 1:
                OSCHandler.Instance.SendMessageToClient("pd", "/unity/playchannel1", 1);
                if (channel3Playing)
                {
                    OSCHandler.Instance.SendMessageToClient("pd", "/unity/playchannel3", 0);
                    channel3Playing = false;
                }
                channelNumber = 2;
                break;
            case 2:
                OSCHandler.Instance.SendMessageToClient("pd", "/unity/playchannel2", 1);
                OSCHandler.Instance.SendMessageToClient("pd", "/unity/playchannel1", 0);
                channelNumber = 3;
                break;
            case 3:
                OSCHandler.Instance.SendMessageToClient("pd", "/unity/playchannel3", 1);
                OSCHandler.Instance.SendMessageToClient("pd", "/unity/playchannel2", 0);
                channelNumber = 1;
                channel3Playing = true;
                break;

        }
    }
}
