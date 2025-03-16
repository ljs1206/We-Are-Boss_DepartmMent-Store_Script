using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LJS
{
    public class SetPos : MonoBehaviour
    {
        private Camera _camera;

        [SerializeField]
        private LayerMask _layerMask;
        [SerializeField]
        private LayerMask _osLayerMask;

        private NavMeshAgent _navAgent;

        private void Awake() {
            _navAgent = GetComponent<NavMeshAgent>();
        }

        private void Start() {
            _camera = Camera.main;
        }

        private void Update() {
            if(Input.GetMouseButtonDown(0)){
                ClickToPos();
            }
        }

        private void ClickToPos(){
            Ray mouseRay = _camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(mouseRay, out RaycastHit hitInfo, 9999, _layerMask);
            
            if(!Physics.Raycast(mouseRay, 9999, _osLayerMask))
            { 
                _navAgent.SetDestination(hitInfo.point);
            }
        }
    }
}
