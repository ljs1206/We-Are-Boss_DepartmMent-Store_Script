using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private float _radius;
    public Transform _targetTrm;

    private Transform _visual;

    private void Awake() {
        _visual = transform.Find("Visual");
    }

    private void Update() {
        Vector3 dir = (_targetTrm.position - transform.position).normalized;
        dir.y = 0;
        float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;

        _visual.localPosition = (new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, 
                                    Mathf.Sin(angle * Mathf.Deg2Rad)) * _radius);

        _visual.rotation = Quaternion.LookRotation(new Vector3(_targetTrm.position.x, 0, _targetTrm.position.z))
                                * Quaternion.Euler(90f, 0, 0);
    }
}
