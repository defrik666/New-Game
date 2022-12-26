using UnityEngine;

public class AttachmentMove : MonoBehaviour
{
    public GameObject targetPos;
    public float speed = 5f;
    public bool pickedUp = false;
    public Rigidbody gameObjectRigbody;
    public Collider boxCollider;
    public GameObject attachTriggerObj;
    public bool attached = false;

    private void Start(){
        gameObjectRigbody = this.gameObject.GetComponent<Rigidbody>();
        boxCollider = this.gameObject.GetComponent<Collider>();
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
        attached = false;

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        gameObjectRigbody.useGravity = false;
        gameObjectRigbody.isKinematic = false;
        boxCollider.isTrigger = false;
    }

    public void OnPlaceDown(){
        pickedUp = false;
        targetPos = null;
        gameObject.layer = LayerMask.NameToLayer("GunParts");
        gameObjectRigbody.useGravity = true;
        boxCollider.isTrigger = false;
    }

    public void OnAttach(GameObject attachObj){
        attached = true;
        pickedUp = false;
        targetPos = null;
        attachTriggerObj = attachObj;

        gameObjectRigbody.useGravity = false;
        gameObjectRigbody.velocity = Vector3.zero;
        gameObjectRigbody.isKinematic = true;

        boxCollider.isTrigger = true;
  
        transform.SetParent(attachTriggerObj.transform);
        Debug.Log(gameObject.name + "is cringe");
        transform.position = attachTriggerObj.transform.position;

        gameObject.layer = LayerMask.NameToLayer("GunParts");
    }
}
