using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityOSC;

public class SoundTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", "ready");
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/oscplayseq", 1);

        StartCoroutine(Example());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Example()
    {
        yield return new WaitForSeconds(2);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/oscplayseq", 1);
    }
}
