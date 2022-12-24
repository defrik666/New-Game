using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI : MonoBehaviour
{
    [SerializeField] private ChangeLocation changeLocation;
    [SerializeField] private AttachmentShop attachmentShop;

    private void Awake() {
        changeLocation = FindObjectOfType<ChangeLocation>();
        attachmentShop = FindObjectOfType<AttachmentShop>();
    }

    public void ToRange(){
        changeLocation.MoveToRange();
    }

    public void ToWorkbeanch(){
        changeLocation.MoveToWorkbench();
    }

    public void OpenShop(){
        var shopButton = transform.Find("Shop Button").gameObject;
        attachmentShop.gameObject.SetActive(true);
        attachmentShop.shopButton = shopButton;
        shopButton.SetActive(false);
        
    }

}
