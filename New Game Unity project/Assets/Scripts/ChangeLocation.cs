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
    public float movementSpeed;
    public float rotationSpeed;

    private void Awake() {
        actionController = GetComponent<ActionController>();
        gun = FindObjectOfType<Gun>();
        mainCamera = Camera.main.gameObject;
        anim = mainCamera.GetComponent<Animator>();
        shootingRange = GameObject.Find("/Player/ShootingRange");
        WeaponPos = GameObject.Find("/Player/ShootingRange/WeaponPos");
        workBench = GameObject.Find("/Player/Workbench");

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

        while(Vector3.Distance(gunObj.transform.position, WeaponPos.transform.position) > 0.002f){
            var moveStep = movementSpeed * Time.deltaTime;
            var rotStep = rotationSpeed * Time.deltaTime;
            gun.transform.position = Vector3.Slerp(gun.transform.position, WeaponPos.transform.position, moveStep);
            gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, WeaponPos.transform.rotation, rotStep);
            yield return new WaitForSeconds(0.01f);
        }

        gunObj.transform.rotation = WeaponPos.transform.rotation;
        gunObj.transform.position = WeaponPos.transform.position;

        while(Vector3.Distance(gameObject.transform.position, rangePos.transform.position) > 0.08f){
            var moveStep = movementSpeed * Time.deltaTime;
            var rotStep = rotationSpeed * Time.deltaTime;
            gameObject.transform.position = Vector3.Slerp(gameObject.transform.position, rangePos.transform.position, moveStep);
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, rangePos.transform.rotation, rotStep);
            yield return new WaitForSeconds(0.01f);
        }

        gameObject.transform.rotation = rangePos.transform.rotation;
        gameObject.transform.position = rangePos.transform.position;
        gameObject.transform.SetParent(rangePos.transform);
        gameObject.GetComponent<MouseLook>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        actionController.ChangeToRange(gun);
        yield return null;
    }
}
