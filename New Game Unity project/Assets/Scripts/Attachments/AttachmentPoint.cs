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

        if(other.gameObject.CompareTag(gameObject.tag) && cooldown != true){
            AttachmentMove attachment = other.gameObject.GetComponent<AttachmentMove>();

            gunStats.AddAttachment(attachment.GetComponent<AttachmentStats>());
            attachment.OnAttach(gameObject);
            camInt.pickedUp = false;

            StartCoroutine(Enter());
        }
    }

    private void OnTriggerExit(Collider other) {
        GameObject attachment = other.gameObject;

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
