using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentSpawn : MonoBehaviour
{
    [SerializeField] public GameObject attachment;
    [SerializeField] public Transform SpawnPos;

    public void SpawnAttachment(){
        Instantiate<GameObject>(attachment,SpawnPos.position,attachment.transform.rotation,SpawnPos.parent);
    }
}
