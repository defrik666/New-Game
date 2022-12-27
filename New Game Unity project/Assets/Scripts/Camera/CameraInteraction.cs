using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInteraction : MonoBehaviour{
    public LayerMask layerMask;
    public GameObject targetPos;
    public AttachmentMove attachment;

    public bool pickedUp = false;
    
    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(gameObject.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.black);
    }

    public void Interact(InputAction.CallbackContext context){
        if(context.performed){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, 100, layerMask);

            if(pickedUp == true){
                pickedUp = false;
                attachment.OnPlaceDown();
                attachment = null;
            }

            else if(hit.collider != null){
                RaycastCheck(hit);
            }
        }
    }

    private void RaycastCheck(RaycastHit hit) {
        Debug.Log(hit.collider.name);
        if(hit.collider.TryGetComponent<AttachmentMove>(out AttachmentMove comp)){
            
            attachment = comp;
            pickedUp = true;

            attachment.OnPickUp(targetPos);
            attachment.gameObjectRigbody.velocity = Vector3.zero;
        }
    }
}
