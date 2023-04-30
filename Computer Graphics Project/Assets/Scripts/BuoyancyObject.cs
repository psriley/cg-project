using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuoyancyObject : MonoBehaviour
{
    public Transform[] floaters;

    public float underwaterDrag = 3f;
    public float underwaterAngularDrag = 1f;

    public float airDrag = 1.2f;
    public float airAngularDrag = 0.05f;

    public float floatingForce = 15f;
    public Vector3 torque;

    OceanManager riverManager;
    Rigidbody rb;
    bool underwater;
    int floatersUnderwater;

    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;

    // Start is called before the first frame update
    void Start()
    {
        riverManager = FindObjectOfType<OceanManager>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        floatersUnderwater = 0;
        for (int i = 0; i < floaters.Length; i++)
        {
            float height = riverManager.WaterHeightAtPosition(floaters[i].position);
            float difference = floaters[i].position.y - height;
            if (difference < 0)
            {
                rb.AddForceAtPosition(Vector3.up * floatingForce * Mathf.Abs(difference), floaters[i].position, ForceMode.Force);
                /*rb.AddForceAtPosition(Vector3.forward * riverManager.waveSpeed, floaters[i].position, ForceMode.Force);*/
                /*rb.AddForceAtPosition(Vector3.right * riverManager.currentSpeed * floatingForce, floaters[i].position, ForceMode.Force);*/
                floatersUnderwater += 1;
                if (!underwater)
                {
                    underwater = true;
                    SwitchState(underwater);
                }
                /*float r = Random.Range(-(transform.localScale.y / 8), (transform.localScale.y / 8));*/
                /*float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
                float r = Mathf.Lerp(-(transform.localScale.y / 8), (transform.localScale.y / 8), interpolationRatio);
                elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);
                floaters[i].localPosition = new Vector3(floaters[i].localPosition.x, r, floaters[i].localPosition.z);
                rb.AddForceAtPosition(Vector3.up * floatingForce * Mathf.Abs(difference), floaters[i].position, ForceMode.Force);*/
            }
            /*else {
                rb.AddForceAtPosition(Vector3.down * floatingForce * Mathf.Abs(difference), floaters[i].position, ForceMode.Force);
            }*/
        }
        
        if (underwater && floatersUnderwater == 0)
        {
            underwater = false;
            SwitchState(underwater);
        }

        rb.AddTorque(torque);
    }

    void SwitchState(bool isUnderwater)
    {
        if (isUnderwater)
        {
            rb.drag = underwaterDrag;
            rb.angularDrag = underwaterAngularDrag;
        }
        else
        {
            rb.drag = airDrag;
            rb.angularDrag = airAngularDrag;
        }
    }
}
