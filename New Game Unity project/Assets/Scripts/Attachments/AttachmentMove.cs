using UnityEngine;

public class AttachmentMove : MonoBehaviour
{
    public GameObject targetPos;
    public float speed = 5f;
    public bool pickedUp = false;
    public Rigidbody gameObjectRigbody;
    public GameObject attachTrigger;

    private void Start(){
        gameObjectRigbody = this.gameObject.GetComponent<Rigidbody>();
    }

    public void Pick(GameObject targetPos){  
        var step = speed * Time.deltaTime;
        transform.position = Vector3.Slerp(transform.position, targetPos.transform.position, step);
    }

    private void FixedUpdate(){
        if (pickedUp) Pick(targetPos);
    }

    public void OnPickUp(GameObject Pos){
        pickedUp = true;
        targetPos = Pos;

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        gameObjectRigbody.useGravity = false;
        gameObjectRigbody.isKinematic = false;
    }

    public void OnPlaceDown(){
        pickedUp = false;
        targetPos = null;
        gameObject.layer = LayerMask.NameToLayer("GunParts");
        gameObjectRigbody.useGravity = true;
    }

    public void OnAttach(GameObject attachObj){
        pickedUp = false;
        targetPos = null;
        attachTrigger = attachObj;

        gameObjectRigbody.useGravity = false;
        gameObjectRigbody.velocity = Vector3.zero;
        gameObjectRigbody.isKinematic = true;
  
        transform.SetParent(attachTrigger.transform);
        transform.position = attachTrigger.transform.position;

        gameObject.layer = LayerMask.NameToLayer("GunParts");
    }
}
