using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplingHook : MonoBehaviour
{
    [SerializeField] private Transform Hedefpos;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private DistanceJoint2D dj;
    [SerializeField] private float activationDistance = 5f; // Set your desired activation distance here

    // Start is called before the first frame update
    void Start()
    {
        dj.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (IsWithinActivationDistance())
            {
                HookOn();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            HookOff();
        }
    }

    private bool IsWithinActivationDistance()
    {
        return Vector2.Distance(transform.position, Hedefpos.position) <= activationDistance;
    }

    public void HookOn()
    {
        dj.enabled = true;
        dj.connectedAnchor = Hedefpos.position;
        lr.positionCount = 2;
        lr.SetPosition(0, Hedefpos.position);
        lr.SetPosition(1, transform.position);
    }

    public void HookOff()
    {
        dj.enabled = false;
        lr.positionCount = 0;
    }
}
