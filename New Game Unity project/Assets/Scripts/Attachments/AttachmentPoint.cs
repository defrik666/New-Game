using System.Collections;
using UnityEngine;

public class AttachmentPoint : MonoBehaviour
{
    public Transform workArea;
    private CameraInteraction camInt;
    private GunStats gunStats;
    private bool cooldown = false;

    private void Start() {
        camInt = FindObjectOfType<CameraInteraction>();
        gunStats = FindObjectOfType<GunStats>();
        workArea = GameObject.Find("Work Area").transform;
    }

    private void OnTriggerEnter(Collider other) {

        if(other.gameObject.CompareTag(gameObject.tag)){
            AttachmentMove attachment = other.gameObject.GetComponent<AttachmentMove>();
            if(cooldown == true || attachment.attached == true){
                return;
            }

            gunStats.AddAttachment(attachment.GetComponent<AttachmentStats>());
            attachment.OnAttach(gameObject);
            camInt.pickedUp = false;

            StartCoroutine(Enter());
        }
    }

    private void OnTriggerExit(Collider other) {

        if(other.CompareTag(gameObject.tag)){
            AttachmentMove attachment = other.gameObject.GetComponent<AttachmentMove>();
            if(cooldown == true || attachment.attached == true){
                return;
            }

            gunStats.RemoveAttachment(attachment.GetComponent<AttachmentStats>());
            attachment.transform.SetParent(workArea);
            attachment.attached = false;
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
