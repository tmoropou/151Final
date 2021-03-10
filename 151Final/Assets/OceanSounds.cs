using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityOSC;

public class OceanSounds : MonoBehaviour
{
    bool startWave = true;
    bool moveWaterUp = false;
    bool moveWaterDown = false;

    float oceanRiseAmount;
    public int interpolationFramesCount = 10;
    float elapsedFrames = 0.0f;

    Vector3 initialOceanPosition = new Vector3(0, 0, 0);
    Vector3 oceanRisePosition;

    float min = 0.0f;
    float max = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", "ready");
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/oscplayocean", 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startWave)
        {
            startWave = false;
            StartCoroutine(Example());
        }

        if(moveWaterUp)
        {
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

            // transform.position = Vector3.Lerp(initialOceanPosition, oceanRisePosition, interpolationRatio);
            transform.position = new Vector3(0.0f, Mathf.SmoothStep(min, oceanRiseAmount, interpolationRatio), 0.0f);

            elapsedFrames = (elapsedFrames + 0.1f); //% (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)

            if (interpolationRatio >= 1.0f)
            {
                moveWaterUp = false;
                elapsedFrames = 0.0f;
                moveWaterDown = true;
            }
        }

        if (moveWaterDown)
        {
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
            // Debug.Log(interpolationRatio);

            //transform.position = Vector3.Lerp(oceanRisePosition, initialOceanPosition, interpolationRatio);
            transform.position = new Vector3(0.0f, Mathf.SmoothStep(oceanRiseAmount, min, interpolationRatio), 0.0f);

            elapsedFrames = (elapsedFrames + 0.1f); //% (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)

            if (interpolationRatio >= 1.0f)
            {
                moveWaterUp = false;
                elapsedFrames = 0.0f;
                moveWaterDown = false;
                startWave = true;
            }
        }
    }

    IEnumerator Example()
    {
        // Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(Random.Range(3, 6));
        oceanRiseAmount = Random.Range(750.0f, 2000.0f);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/oscplaywave", oceanRiseAmount);
        moveWaterUp = true;
        oceanRiseAmount = ((oceanRiseAmount / 200.0f) / 1.3f);
        // Debug.Log("Resetting can wave at timestamp : " + Time.time);

    }
}
