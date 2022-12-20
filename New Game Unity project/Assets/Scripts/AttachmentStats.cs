using UnityEngine;

public class AttachmentStats : MonoBehaviour
{
    public enum AttachmentTypes{
        None,
        GunStock,
        Handle,
        Forearm
    }

    public AttachmentTypes _type = AttachmentTypes.None;

    private void Start() {
        switch(tag){
            case "GunStock":
            _type = AttachmentTypes.GunStock;
            break;
            case "Handle":
            _type = AttachmentTypes.Handle;
            break;
            case "Forearm":
            _type = AttachmentTypes.Forearm;
            break;
        }
        
    }
}
