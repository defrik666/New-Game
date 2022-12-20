using System.Collections;
using UnityEngine;

public class AttachmentPoint : MonoBehaviour
{
    public Transform workArea;
    private CameraInteraction camInt;
    private bool cooldown = false;

    private void Start() {
        camInt = Camera.main.GetComponent<CameraInteraction>();
        workArea = GameObject.Find("Work Area").transform;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject + " Entered trigger");

        if(other.gameObject.CompareTag(gameObject.tag) && cooldown != true){
            AttachmentMove attachment = other.gameObject.GetComponent<AttachmentMove>();

            attachment.OnAttach(gameObject);
            camInt.pickedUp = false;

            StartCoroutine(Enter());
        }
    }

    private void OnTriggerExit(Collider other) {


        GameObject attachment = other.gameObject;
        Debug.Log(attachment.name + " Exited trigger");

        if(attachment.CompareTag(gameObject.tag) && cooldown != true){
            attachment.transform.SetParent(workArea);
            StartCoroutine(Exit());
        }
    }

    private IEnumerator Enter(){
        cooldown = true;
        yield return new WaitForSeconds(0.1f);
        cooldown = false;
    }

    private IEnumerator Exit(){
        cooldown = true;
        yield return new WaitForSeconds(0.1f);
        cooldown = false;
    }
}
