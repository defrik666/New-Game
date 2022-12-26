using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrash : MonoBehaviour
{
    private CameraInteraction camInt;

    private void Awake() {
        camInt = FindObjectOfType<CameraInteraction>();
    }

    private void OnTriggerEnter(Collider other) {
        switch(other.tag){
            case "Handle":
            StartCoroutine(DestroyAttachment(other.gameObject));
            return;
            case "Forearm":
            StartCoroutine(DestroyAttachment(other.gameObject));
            return;
            case "GunStock":
            StartCoroutine(DestroyAttachment(other.gameObject));
            return;
            case "Magazine":
            StartCoroutine(DestroyAttachment(other.gameObject));
            return;
            case "Handguard":
            StartCoroutine(DestroyAttachment(other.gameObject));
            return;
        }
    }

    private IEnumerator DestroyAttachment(GameObject obj){
        Destroy(obj,0.3f);
        yield return new WaitForSeconds(0.3f);
        camInt.pickedUp = false;
        yield return null;
    }
}
