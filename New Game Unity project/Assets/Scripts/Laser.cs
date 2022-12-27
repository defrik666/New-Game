using UnityEngine;
using UnityEngine.InputSystem;

public class Laser : MonoBehaviour{
    private LineRenderer lineRenderer;
    [SerializeField] private Transform laserPos;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start() {
        lineRenderer.enabled = false;
    }

    private void Update() {
        if(lineRenderer.enabled == false){
            return;
        }
        lineRenderer.SetPosition(0, transform.position);
        RaycastHit hit;

        if (Physics.Raycast(laserPos.position, laserPos.forward, out hit))
        {
            Debug.Log(hit.point);

            if(hit.collider){
                lineRenderer.SetPosition(1, hit.point);
            }
            else{
                lineRenderer.SetPosition(1, new Vector3(transform.position.x, transform.position.y, 5000));
            }
        }
    }

    public void TurnOnOff(InputAction.CallbackContext context){
        Debug.Log(context);
        if(lineRenderer.enabled){
            lineRenderer.enabled = false;
            return;
        }
        else{
            lineRenderer.enabled = true;
        }
    }
}
