using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment_Point : MonoBehaviour
{
    public Transform workArea;
    private Camera_Interaction camInt;

    private void Start() {
        camInt = Camera.main.GetComponent<Camera_Interaction>();
        //workArea = gameObject.transform.parent.parent;
    }

    private void OnTriggerEnter(Collider other) {
        GameObject attachment = other.gameObject;

        if(attachment.CompareTag(gameObject.tag)){
            attachment.GetComponent<Attachment_Move>().pickedUp = false;
            attachment.GetComponent<Attachment_Move>().target_pos = null;
            camInt.pickedUp = false;

            attachment.GetComponent<Rigidbody>().useGravity = false;
            attachment.GetComponent<Rigidbody>().velocity = Vector3.zero;
            attachment.GetComponent<Rigidbody>().isKinematic = true;
            

            attachment.transform.SetParent(gameObject.transform);
            attachment.transform.position = gameObject.transform.position;


            attachment.layer = LayerMask.NameToLayer("GunParts");
        }
    }

    private void OnTriggerExit(Collider other) {
        GameObject attachment = other.gameObject;
        if(attachment.CompareTag(gameObject.tag)){
            other.transform.SetParent(workArea);
            attachment.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
