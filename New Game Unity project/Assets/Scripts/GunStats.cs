using System.Collections.Generic;
using UnityEngine;

public class GunStats : MonoBehaviour
{
    public Gun gun;
    public List<AttachmentStats> attachmentsList;
    [Header("Base stats")]
    [SerializeField] public float baseSpread;
    [SerializeField] public float baseRecoil;
    [Header("Final stats")]
    [SerializeField] public float spread;
    [SerializeField] public float recoil;
    [SerializeField] public int ammo;

    private void Awake() {
        gun = FindObjectOfType<Gun>();
        spread = baseSpread;
        recoil = baseRecoil;
    }

    private void Start() {
        SetInfo();
    }

    public void AddAttachment(AttachmentStats attachment){
        Debug.Log(attachment.name + "Attachment added");
        attachmentsList.Add(attachment);

        if(attachment._type == AttachmentStats.AttachmentTypes.Magazine) ammo = attachment.ammo;
        recoil -= attachment.recoil;
        spread -= attachment.spread;

        SetInfo();
    }

    public void RemoveAttachment(AttachmentStats attachment){
        Debug.Log(attachment.name + "Attachment removed");
        attachmentsList.Add(attachment);

        if(attachment._type == AttachmentStats.AttachmentTypes.Magazine) ammo = attachment.ammo;
        recoil += attachment.recoil;
        spread += attachment.spread;

        SetInfo();
    }

    private void SetInfo(){
        gun.ammo = ammo;
        gun.bullets = gun.ammo;
        gun.recoil = recoil;
        gun.spread = spread;
    }

}
