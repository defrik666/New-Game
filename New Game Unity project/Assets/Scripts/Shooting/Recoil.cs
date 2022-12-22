using UnityEngine;

public class Recoil : MonoBehaviour{

    //Rotations
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    //Settings
    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;

    private void Update() {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire(float recoil){
        targetRotation += new Vector3(-recoil, Random.Range(-recoil,recoil), Random.Range(-recoil / 10,recoil / 10));
    }
}
