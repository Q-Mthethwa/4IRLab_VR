using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkPlayer : NetworkBehaviour
{
    public Transform root;
    public Transform head;
    //public Transform LeftHand;
    //public Transform RightHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsOwner)
        {

        root.position = VRRigReferences.Singleton.root.position;
        root.rotation = VRRigReferences.Singleton.root.rotation;

        head.position = VRRigReferences.Singleton.head.position;
        head.rotation = VRRigReferences.Singleton.head.rotation;
/*
        LeftHand.position = VRRigReferences.Singleton.LeftHand.position;
        LeftHand.rotation = VRRigReferences.Singleton.LeftHand.rotation;

        RightHand.position = VRRigReferences.Singleton.RightHand.position;
        RightHand.rotation = VRRigReferences.Singleton.RightHand.rotation;
        */
        }
    }
}
