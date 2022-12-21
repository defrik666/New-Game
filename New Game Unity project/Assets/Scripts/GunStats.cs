using System.Collections.Generic;
using UnityEngine;

public class GunStats : MonoBehaviour
{
    public Gun gun;
    public List<AttachmentStats> attachmentsList;
    public float recoil;
    public float spread;
    public int ammo;

    private void Awake() {
        gun = FindObjectOfType<Gun>();
    }

    public void AddAttachment(AttachmentStats attachment){
        attachmentsList.Add(attachment);

        if(attachment._type == AttachmentStats.AttachmentTypes.Magazine) ammo = attachment.ammo;
        recoil = attachment.recoil;
        spread = attachment.spread;

        SetInfo();
    }

    public void RemoveAttachment(AttachmentStats attachment){
        attachmentsList.Add(attachment);

        SetInfo();
    }

    private void SetInfo(){
        gun.ammo = ammo;
        gun.recoil = recoil;
        gun.spread = spread;
    }
}
