using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuoyancyObject : MonoBehaviour
{
    public Transform[] floaters;

    public float underwaterDrag = 3f;
    public float underwaterAngularDrag = 1f;

    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;

    public float floatingForce = 15f;

    OceanManager riverManager;
    Rigidbody rb;
    bool underwater;
    int floatersUnderwater;

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
            float difference = floaters[i].position.y - riverManager.WaterHeightAtPosition(floaters[i].position);
            if (difference < 0)
            {
                rb.AddForceAtPosition(Vector3.up * floatingForce * Mathf.Abs(difference), floaters[i].position, ForceMode.Force);
                /*rb.AddForceAtPosition(Vector3.right * riverManager.waveSpeed * floatingForce, floaters[i].position, ForceMode.Force);*/
                floatersUnderwater += 1;
                if (!underwater)
                {
                    underwater = true;
                    SwitchState(underwater);
                }
            }
        }
        
        if (underwater && floatersUnderwater == 0)
        {
            underwater = false;
            SwitchState(underwater);
        }
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
