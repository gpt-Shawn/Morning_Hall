using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class OVRHandTrackingGrabber : OVRGrabber
{
    private Hand hand;
    public float PinchThreshold = 0.7f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        hand = GetComponent<Hand>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CheckIndexPinch();

    }

    void CheckIndexPinch() 
    {
        float pintchStrength = hand.PinchStrength(OVRPlugin.HandFinger.Index);
        bool isPinching = pintchStrength > PinchThreshold;

        if (!m_grabbedObj && isPinching && m_grabCandidates.Count > 0)
            GrabBegin();
        else if (m_grabbedObj && !isPinching)
            GrabEnd();
    }
    
    protected override void GrabEnd()
    {
        if (m_grabbedObj) 
        {
            Vector3 linearVelocity = (transform.position - m_lastPos) / Time.fixedDeltaTime;
            Vector3 angularVelocity = (transform.eulerAngles - m_lastRot.eulerAngles)/Time.fixedDeltaTime;

            GrabbableRelease(linearVelocity,angularVelocity);
        }
        GrabVolumeEnable(true);
    }
    
}
