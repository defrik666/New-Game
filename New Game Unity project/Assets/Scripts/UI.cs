using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI : MonoBehaviour
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

    public void ToRange(){
        
        shootingRange.SetActive(true);

        GameObject gun = FindObjectOfType<GunStats>().gameObject;
        StartCoroutine(GunMove(gun));


        // anim.Play("To Range");
    }

    private IEnumerator GunMove(GameObject gun){
        while(Vector3.Distance(gun.transform.position, WeaponPos.transform.position) > 0.2f){
            var step = 10f * Time.deltaTime;
            gun.transform.position = Vector3.Slerp(gun.transform.position, WeaponPos.transform.position, step);
        }
        workBench.SetActive(false);
        yield return null;
    }
}
