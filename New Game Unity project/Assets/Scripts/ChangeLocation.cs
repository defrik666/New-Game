using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLocation : MonoBehaviour{
    [Header("Objects")]
    private GameObject mainCamera;
    private Animator anim;
    private GameObject shootingRange;
    private GameObject WeaponPos;
    private GameObject workBench;
    public GameObject rangePos;
    private Gun gun;
    private ActionController actionController;
    [Header("Speed")]
    public float SecondsBetwenPositions;
    public float DegreesForSecond;

    private void Awake() {
        actionController = GetComponent<ActionController>();
        gun = FindObjectOfType<Gun>();
        mainCamera = Camera.main.gameObject;
        anim = mainCamera.GetComponent<Animator>();
        shootingRange = GameObject.Find("Player/ShootingRange");
        WeaponPos = GameObject.Find("Player/ShootingRange/WeaponPos");
        workBench = GameObject.Find("Player/Workbench");

    }

    private void Start() {
        shootingRange.SetActive(false);
    }

    public void MoveToRange(){
        GameObject gun = FindObjectOfType<GunStats>().gameObject;
        StartCoroutine(GunMove(gun));
    }

    private IEnumerator GunMove(GameObject gunObj){
        workBench.SetActive(false);
        gunObj.transform.SetParent(WeaponPos.transform);
        shootingRange.SetActive(true);

        var startPos = gun.transform.position;
        float moveStep = 0;

        while(Vector3.Distance(gunObj.transform.position, WeaponPos.transform.position) > 0.001f){
            moveStep += Time.deltaTime / SecondsBetwenPositions;
            float rotStep = DegreesForSecond * Time.deltaTime;

            gun.transform.position = Vector3.Slerp(startPos, WeaponPos.transform.position, moveStep);
            gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, WeaponPos.transform.rotation, rotStep);

            yield return new WaitForEndOfFrame();
        }

        gunObj.transform.rotation = WeaponPos.transform.rotation;
        gunObj.transform.position = WeaponPos.transform.position;

        startPos = transform.position;
        moveStep = 0;

        while(Vector3.Distance(transform.position, rangePos.transform.position) > 0.001f){
            moveStep += Time.deltaTime / SecondsBetwenPositions;
            var rotStep = DegreesForSecond * Time.deltaTime;

            transform.position = Vector3.Slerp(startPos, rangePos.transform.position, moveStep);
            transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, rangePos.transform.rotation, rotStep);

            yield return new WaitForEndOfFrame();
        }

        transform.rotation = rangePos.transform.rotation;
        transform.position = rangePos.transform.position;
        transform.SetParent(rangePos.transform);
        gameObject.GetComponent<Recoil>().enabled = true;
        rangePos.GetComponent<MouseLook>().enabled = true;
        
        gun.GetComponent<Animator>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        actionController.ChangeToRange(gun);
        yield return null;
    }
}
