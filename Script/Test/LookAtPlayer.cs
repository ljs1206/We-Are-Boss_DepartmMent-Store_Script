using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private void LateUpdate() {
        Transform mainCamTrm = Camera.main.transform;
        Vector3 camDirection  = (transform.position - mainCamTrm.position).normalized;
        transform.rotation = Quaternion.LookRotation(-camDirection);
    }
}
