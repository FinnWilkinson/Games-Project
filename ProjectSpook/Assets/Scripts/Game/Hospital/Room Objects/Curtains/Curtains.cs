using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains : TriggerEvent
{
    /* 
     * Basically we need to enable a capsule collider and add this to the
     * list of colliders on the cloth. This will allow the character to 'interact'
     * with the curtain
    */

    public SphereCollider leftHandCollider;
    public SphereCollider rightHandCollider;
    public SphereCollider cameraCollider;
    public Cloth curtain1;
    public Cloth curtain2;

    public override void Awake()
    {
        var SphereColliders = new ClothSphereColliderPair[3];
        SphereColliders[0] = new ClothSphereColliderPair(leftHandCollider);
        SphereColliders[1] = new ClothSphereColliderPair(rightHandCollider);
        SphereColliders[2] = new ClothSphereColliderPair(cameraCollider);
        curtain1.sphereColliders = SphereColliders;
        curtain2.sphereColliders = SphereColliders;

        curtain1.enabled = false;
        curtain2.enabled = false;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Tray")
        {
            var CapsuleColliders = new CapsuleCollider[1];
            CapsuleColliders[0] = other.gameObject.GetComponent<CapsuleCollider>();
            curtain1.capsuleColliders = CapsuleColliders;
            curtain2.capsuleColliders = CapsuleColliders;
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Tray")
        {
            curtain1.capsuleColliders = null;
            curtain2.capsuleColliders = null;
        }
    }
}