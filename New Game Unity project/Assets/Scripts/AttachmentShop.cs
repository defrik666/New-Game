using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttachmentShop : MonoBehaviour{
    [SerializeField] public GameObject shopElementPrefab;
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private Transform contentPos;
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject types;
    [SerializeField] private GameObject curentPage;
    [SerializeField] public GameObject backButton;
    [SerializeField] public GameObject shopButton;

    private void Awake() {
        headerText = transform.Find("Viewport/Header/Text").GetComponent<TextMeshProUGUI>();
        contentPos = transform.Find("Viewport/Content");
        SpawnPos = GameObject.Find("/Work Area/Attachment Spawn Point").transform;
        types = contentPos.Find("Types").gameObject;
        scrollRect = GetComponent<ScrollRect>();

        foreach(GameObject obj in Resources.LoadAll<GameObject>("Prefabs/Attachments")){
            switch(obj.GetComponent<AttachmentStats>()._type){
                case AttachmentStats.AttachmentTypes.GunStock:
                SpawnShopElement(obj,GameObject.Find("Gunstoks").transform,false);
                break;
                case AttachmentStats.AttachmentTypes.ComplexGunStock:
                SpawnShopElement(obj,GameObject.Find("Gunstoks").transform,false);
                break;
                case AttachmentStats.AttachmentTypes.StockExtensionPad:
                SpawnShopElement(obj,GameObject.Find("Gunstoks").transform,false);
                break;
                case AttachmentStats.AttachmentTypes.Magazine:
                SpawnShopElement(obj,GameObject.Find("Magazines").transform,true);
                break;
                case AttachmentStats.AttachmentTypes.Handle:
                SpawnShopElement(obj,GameObject.Find("Handles").transform,false);
                break;
                case AttachmentStats.AttachmentTypes.ForearmHandle:
                SpawnShopElement(obj,GameObject.Find("ForearmHandles").transform,false);
                break;
                case AttachmentStats.AttachmentTypes.Forearm:
                SpawnShopElement(obj,GameObject.Find("Forearms").transform,false);
                break;
                case AttachmentStats.AttachmentTypes.Handguard:
                SpawnShopElement(obj,GameObject.Find("Handguards").transform,false);
                break;
                case AttachmentStats.AttachmentTypes.Scope:
                SpawnShopElement(obj,GameObject.Find("Scopes").transform,false);
                break;
                case AttachmentStats.AttachmentTypes.Barrel:
                SpawnShopElement(obj,GameObject.Find("Barrels").transform,false);
                break;
                case AttachmentStats.AttachmentTypes.LAM:
                SpawnShopElement(obj,GameObject.Find("LAM").transform,false);
                break;
            }
        }

        contentPos.Find("Gunstoks").gameObject.SetActive(false);
        contentPos.Find("Magazines").gameObject.SetActive(false);
        contentPos.Find("Handles").gameObject.SetActive(false);
        contentPos.Find("ForearmHandles").gameObject.SetActive(false);
        contentPos.Find("Forearms").gameObject.SetActive(false);
        contentPos.Find("Handguards").gameObject.SetActive(false);
        contentPos.Find("Scopes").gameObject.SetActive(false);
        contentPos.Find("Barrels").gameObject.SetActive(false);
        contentPos.Find("LAM").gameObject.SetActive(false);
    }

    private void Start() {
        gameObject.SetActive(false);
    }

    private void SpawnShopElement(GameObject attachment,Transform parent, bool isMagazine){
        GameObject shopElement = Instantiate(shopElementPrefab, parent);

        shopElement.GetComponentInChildren<RawImage>().texture = Resources.Load<Texture2D>($"Prefabs/Attachments/{attachment.name}");
        shopElement.GetComponent<AttachmentSpawn>().attachment = attachment;
        shopElement.GetComponent<AttachmentSpawn>().SpawnPos = SpawnPos;

        shopElement.transform.Find("Texts/Name").gameObject.GetComponent<TextMeshProUGUI>().text = attachment.name;
        if(isMagazine){
            shopElement.transform.Find("Texts/Ammo").gameObject.GetComponent<TextMeshProUGUI>().text = $"Количество патрон: {attachment.GetComponent<AttachmentStats>().ammo}";
            shopElement.transform.Find("Texts/Spread").gameObject.GetComponent<TextMeshProUGUI>().text = $"Уменьшение разброса: {attachment.GetComponent<AttachmentStats>().spread}";
        shopElement.transform.Find("Texts/Recoil").gameObject.GetComponent<TextMeshProUGUI>().text = $"Уменьшение отдачи: {attachment.GetComponent<AttachmentStats>().recoil}";
            return;
        }
        shopElement.transform.Find("Texts/Ammo").gameObject.SetActive(false);
        shopElement.transform.Find("Texts/Spread").gameObject.GetComponent<TextMeshProUGUI>().text = $"Уменьшение разброса: {attachment.GetComponent<AttachmentStats>().spread}";
        shopElement.transform.Find("Texts/Recoil").gameObject.GetComponent<TextMeshProUGUI>().text = $"Уменьшение отдачи: {attachment.GetComponent<AttachmentStats>().recoil}";
    }

    public void NextPage(GameObject page){
        types.SetActive(false);
        page.SetActive(true);
        scrollRect.content = page.GetComponent<RectTransform>();
        curentPage = page;
        backButton.SetActive(true);

        switch(page.name){
            case "Gunstoks":
            headerText.text = "Приклады";
            break;
            case "Magazines":
            headerText.text = "Магазины";
            break;
            case "Handles":
            headerText.text = "Рукоятки";
            break;
            case "Forearms":
            headerText.text = "Цевья";
            break;
            case "Handguards":
            headerText.text = "Крышки ствольной коробки";
            break;
            case "Scopes":
            headerText.text = "Прицелы";
            break;
            case "Barrels":
            headerText.text = "ДТК";
            break;
            case "LAM":
            headerText.text = "Лазеры";
            break;
        }

    }

    public void PrevPage(){
        types.SetActive(true);
        scrollRect.content = types.GetComponent<RectTransform>();
        curentPage.SetActive(false);
        curentPage = null;
        headerText.text = "Виды обвесов";
        backButton.SetActive(false);
    }

    public void CloseShop(){
        gameObject.SetActive(false);
        shopButton.SetActive(true);
    }

}
