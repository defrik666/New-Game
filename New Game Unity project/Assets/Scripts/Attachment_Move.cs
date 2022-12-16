using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment_Move : MonoBehaviour
{
    public GameObject target_pos;
    public float speed = 5f;
    public bool pickedUp = false;
    public Rigidbody gameObjectRigbody;

    private void Start(){
        gameObjectRigbody = this.gameObject.GetComponent<Rigidbody>();
    }

    public void Pick(GameObject targetPos){  
        var step = speed * Time.deltaTime;
        transform.position = Vector3.Slerp(transform.position, targetPos.transform.position, step);
    }

    private void FixedUpdate(){
        if (pickedUp) Pick(target_pos);
    }
}
