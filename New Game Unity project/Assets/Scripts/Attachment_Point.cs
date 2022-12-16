using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment_Point : MonoBehaviour
{
    public GameObject workArea;
    private Camera_Interaction camInt;

    private void Start() {
        camInt = Camera.main.GetComponent<Camera_Interaction>();
    }

    private void OnTriggerEnter(Collider other) {
        GameObject attachment = other.gameObject;

        if(attachment.CompareTag(gameObject.tag)){
            attachment.GetComponent<Attachment_Move>().pickedUp = false;
            camInt.pickedUp = false;

            attachment.GetComponent<Rigidbody>().useGravity = false;
            attachment.GetComponent<Rigidbody>().velocity = Vector3.zero;

            attachment.transform.position = gameObject.transform.position;
            attachment.transform.SetParent(gameObject.transform.parent);

            attachment.layer = LayerMask.NameToLayer("GunParts");
        }
    }

    private void OnTriggerExit(Collider other) {
        other.transform.SetParent(workArea.transform);
    }



}
