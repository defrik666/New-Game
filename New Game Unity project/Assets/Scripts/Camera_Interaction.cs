using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera_Interaction : MonoBehaviour
{
    //Vector3 screnCenter = new Vector3(Screen.width / 2, Screen.height / 2);

    public LayerMask layerMask;
    public GameObject targetPos;
    public Attachment_Move attachment;

    public bool pickedUp = false;
    
    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(gameObject.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.black);
    }

    public void Interact(InputAction.CallbackContext context){ 
        if(context.performed){
            Debug.Log("Click");

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out hit, 100, layerMask);

            if(pickedUp == true){
                pickedUp = false;

                attachment.pickedUp = false;
                attachment.target_pos = null;
                attachment.gameObject.layer = LayerMask.NameToLayer("GunParts");
                attachment.gameObjectRigbody.useGravity = true;
            }

            else if(hit.collider != null){
                RaycastCheck(hit);
            }
        }
    }

    private void RaycastCheck(RaycastHit hit) {
        if(hit.collider.TryGetComponent<Attachment_Move>(out Attachment_Move comp)){
            attachment = comp;

            pickedUp = true;
            attachment.pickedUp = true;
            attachment.target_pos = targetPos;
            attachment.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            attachment.gameObjectRigbody.useGravity = false;
        }

    }



}
