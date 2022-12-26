using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeLocation : MonoBehaviour{
    [Header("Objects")]
    private GameObject mainCamera;
    private Animator anim;
    private GameObject shootingRange;
    private GameObject WeaponPosRange;
    private GameObject WeaponPosWorkbench;
    private GameObject workBench;
    public GameObject rangePos;
    public GameObject playerPos;
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
        WeaponPosRange = GameObject.Find("Player/ShootingRange/WeaponPos");
        WeaponPosWorkbench = GameObject.Find("Work Area/GunPos");
        playerPos = GameObject.Find("Work Area/PlayerPos");
        workBench = GameObject.Find("Player/Workbench");

    }

    private void Start() {
        shootingRange.SetActive(false);
    }

    public void MoveToRange(){
        StartCoroutine(GunMoveToRange(gun.gameObject));
    }

    public void MoveToWorkbench(){
        StartCoroutine(GunMoveToWorkbench(gun.gameObject));
    }

    public void LockCursor(InputAction.CallbackContext context){
        Debug.Log(context);
        Cursor.lockState = CursorLockMode.Locked;
        rangePos.GetComponent<MouseLook>().enabled = true;
        actionController.ChangeMouseLock(this);
    }

    public void UnLockCursor(InputAction.CallbackContext context){
        Debug.Log(context);
        Cursor.lockState = CursorLockMode.None;
        rangePos.GetComponent<MouseLook>().enabled = false;
        actionController.ChangeMouseLock(this);
    }

    private IEnumerator GunMoveToRange(GameObject gunObj){
        actionController.ChangeToRangeStart();
        Cursor.lockState = CursorLockMode.Locked;

        workBench.SetActive(false);
        gunObj.transform.SetParent(transform);

        var startPos = gun.transform.position;
        float moveStep = 0;

        while(Vector3.Distance(gunObj.transform.position, WeaponPosRange.transform.position) > 0.001f){
            moveStep += Time.deltaTime / SecondsBetwenPositions;
            float rotStep = DegreesForSecond * Time.deltaTime;

            gun.transform.position = Vector3.Slerp(startPos, WeaponPosRange.transform.position, moveStep);
            gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, WeaponPosRange.transform.rotation, rotStep);

            yield return new WaitForEndOfFrame();
        }

        gunObj.transform.rotation = WeaponPosRange.transform.rotation;
        gunObj.transform.position = WeaponPosRange.transform.position;

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

        shootingRange.SetActive(true);
        gunObj.transform.SetParent(WeaponPosRange.transform);
        transform.SetParent(rangePos.transform);

        gameObject.GetComponent<Recoil>().enabled = true;
        rangePos.GetComponent<MouseLook>().enabled = true;
        
        gun.GetComponent<Animator>().enabled = true;
        actionController.ChangeToRangeEnd(gun,this);
        yield return null;
    }

    private IEnumerator GunMoveToWorkbench(GameObject gunObj){
        actionController.ChangeToWorkbenchStart();

        gameObject.GetComponent<Recoil>().enabled = false;
        rangePos.GetComponent<MouseLook>().enabled = false;
        gun.GetComponent<Animator>().enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        gunObj.transform.SetParent(transform);
        shootingRange.SetActive(false);

        var startPos = transform.position;
        float moveStep = 0;

        while(Vector3.Distance(transform.position, playerPos.transform.position) > 0.001f){
            Debug.Log("moving player");
            moveStep += Time.deltaTime / SecondsBetwenPositions;
            var rotStep = DegreesForSecond * Time.deltaTime;

            transform.position = Vector3.Slerp(startPos, playerPos.transform.position, moveStep);
            transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, playerPos.transform.rotation, rotStep);

            yield return new WaitForEndOfFrame();
        }

        transform.rotation = playerPos.transform.rotation;
        transform.position = playerPos.transform.position;

        startPos = gun.transform.position;
        moveStep = 0;
        while(Vector3.Distance(gunObj.transform.position, WeaponPosWorkbench.transform.position) > 0.001f){
            Debug.Log("moving gun");
            moveStep += Time.deltaTime / SecondsBetwenPositions;
            float rotStep = DegreesForSecond * Time.deltaTime;

            gun.transform.position = Vector3.Slerp(startPos, WeaponPosWorkbench.transform.position, moveStep);
            gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, WeaponPosWorkbench.transform.rotation, rotStep);

            yield return new WaitForEndOfFrame();
        }

        gunObj.transform.rotation = WeaponPosWorkbench.transform.rotation;
        gunObj.transform.position = WeaponPosWorkbench.transform.position;

        workBench.SetActive(true);
        gunObj.transform.SetParent(WeaponPosWorkbench.transform);
        transform.SetParent(playerPos.transform);
        
        Cursor.lockState = CursorLockMode.None;
        actionController.ChangeToWorkbenchEnd();
        yield return null;
    }

}
