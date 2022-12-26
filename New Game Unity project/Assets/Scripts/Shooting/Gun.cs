using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour{
    [Header("General Effects")]
    [SerializeField] GameObject metalHitEffect;
    [SerializeField] GameObject stoneHitEffect;
    [SerializeField] GameObject woodHitEffect;
    
    [Header("Gun Effects")]
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem cartridgeEjection;

    [Header("Objects")]
    public Recoil recoilScript;
    RaycastHit RayHit;
    private Camera playerCam;
    public Animator anim;
    public Transform shootingPoint;
    public LayerMask layerMask;
    public GameObject normalPos;
    public GameObject scopingPos;

    [Header("General Gun Stats")]
    [SerializeField] public int ammo;
    [SerializeField] public int bullets;
    [SerializeField] int range = 100;
    [SerializeField] public float spread;
    [SerializeField] public float recoil;
    [Header("Coroutines")]
    private Coroutine fireCoroutine;
    [SerializeField] private Coroutine ScopingCoroutine;
    [Header("bools")]
    [SerializeField] private bool isScoping = false;


    private void Awake(){
        playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        recoilScript = FindObjectOfType<Recoil>();
        anim = GetComponent<Animator>();
    }

    private void Start() {
        bullets = ammo;
    }

    private void Update() {
        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * 100, Color.black);
    }

    public void  Shoot(InputAction.CallbackContext context){
        Debug.Log(context);
        if(context.performed){
            if (bullets <= 0){
                return;
            }

            fireCoroutine = StartCoroutine(Shooting());
        }
        else if(context.canceled){
            if(fireCoroutine == null){
                return;
            }
            StopCoroutine(fireCoroutine);
        }
    }

    public void Reload(InputAction.CallbackContext context){
        Debug.Log(context);
        if(context.performed){
            bullets = ammo;
        }
    }

    public void Scope(InputAction.CallbackContext context){
        Debug.Log(context);
        if(isScoping){
            if(ScopingCoroutine != null){
                StopCoroutine(ScopingCoroutine);
            }
            
            ScopingCoroutine = StartCoroutine(UnScoping());
            isScoping = false;
        }
        else{
            if(ScopingCoroutine != null){
                StopCoroutine(ScopingCoroutine);
            }
            
            ScopingCoroutine = StartCoroutine(Scoping());
            isScoping = true;
        }

    }
    
    private void HandleHit(RaycastHit hit){
        if(hit.collider.sharedMaterial == null){
            SpawnDecal(hit, stoneHitEffect);
            return;
        }
        
        string materialName = hit.collider.sharedMaterial.name;

        switch(materialName){
            case "Metal":
                SpawnDecal(hit, metalHitEffect);
				break;
			case "Stone":
				SpawnDecal(hit, stoneHitEffect);
                break;
			case "Wood":
				SpawnDecal(hit, woodHitEffect);
                break;
        }
    }

    private void SpawnDecal(RaycastHit hit, GameObject prefab){
		GameObject spawnedDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
		spawnedDecal.transform.SetParent(hit.collider.transform);
	}

    private IEnumerator Shooting(){
        while(bullets > 0){
            //muzzleFlash.Play();
            //cartridgeEjection.Play();

            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")){
                anim.Play("Shoot");
                bullets--;

                Vector3 spreadDir = new Vector3 (Random.Range(-spread,spread),Random.Range(-spread,spread),Random.Range(-spread,spread));

                Vector3 startPoint = playerCam.transform.position;
                Vector3 direction = playerCam.transform.forward + spreadDir;

                if (Physics.Raycast(startPoint, direction.normalized, out RayHit, range, layerMask)){
                    HandleHit(RayHit);
                }

                recoilScript.RecoilFire(recoil);
            }

            //yield return new WaitForSeconds(0.1f);
            yield return new WaitForEndOfFrame();
        }
        yield return null;   
    } 

    private IEnumerator Scoping(){
        Debug.Log("scope");

        float moveStep = 0f;

        while(Vector3.Distance(transform.parent.position, scopingPos.transform.position) > 0.001f){
            moveStep += Time.deltaTime / 0.3f;

            transform.parent.position = Vector3.Slerp(normalPos.transform.position, scopingPos.transform.position, moveStep);
            
            yield return new WaitForEndOfFrame();
        }

        transform.parent.position = scopingPos.transform.position;

        yield return null;
    }

    private IEnumerator UnScoping(){
        Debug.Log("unscope");

        float moveStep = 0f;

        while(Vector3.Distance(transform.parent.position, normalPos.transform.position) > 0.001f){
            moveStep += Time.deltaTime / 0.3f;

            transform.parent.position = Vector3.Slerp(scopingPos.transform.position, normalPos.transform.position, moveStep);
            
            yield return new WaitForEndOfFrame();
        }

        transform.parent.position = normalPos.transform.position;

        yield return null;
    }
}

