using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLocation : MonoBehaviour
{
    private GameObject mainCamera;
    private Animator anim;
    public GameObject shootingRange;
    public GameObject WeaponPos;
    public GameObject workBench;

    private void Awake() {
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

    private IEnumerator GunMove(GameObject gun){
        workBench.SetActive(false);
        gun.transform.SetParent(WeaponPos.transform);
        shootingRange.SetActive(true);

        while(Vector3.Distance(gun.transform.position, WeaponPos.transform.position) > 0.2f){
            var step = 2f * Time.deltaTime;
            gun.transform.position = Vector3.Slerp(gun.transform.position, WeaponPos.transform.position, step);
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }
}
