using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffMeshEscalator : MonoBehaviour
{
    [SerializeField] private int _offMeshArea = 2;
    [SerializeField] private float _moveSpeed = 2f;

    private NavMeshAgent _navAgent;

    private void Awake() {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
        StartCoroutine(StartEscalator());
    }

    private IEnumerator StartEscalator()
    {
        yield return new WaitUntil(() => IsOnEscalator());

        StartCoroutine(ToEscalator());
    }

    private bool IsOnEscalator()
    {
        if (_navAgent.isOnOffMeshLink)
        {
            OffMeshLinkData linkData = _navAgent.currentOffMeshLinkData;

            if(linkData.offMeshLink != null && linkData.offMeshLink.area == _offMeshArea)
                return true;
        }
        return false;
    }

    private IEnumerator ToEscalator()
    {
        _navAgent.isStopped = true;
        OffMeshLinkData linkData = _navAgent.currentOffMeshLinkData;
        Vector3 start = transform.position;
        Vector3 end = linkData.endPos;

        float jumpTime = Mathf.Max(0.3f, Vector3.Distance(start, end)) / _moveSpeed;
        float currnetTime = 0;
        float percent = 0;

            while(percent < 1)
            {
                currnetTime += Time.deltaTime;
                percent = currnetTime / jumpTime;

                Vector3 pos = Vector3.Lerp(start, end, percent);

                transform.position = pos;
                yield return null;
            }
        _navAgent.CompleteOffMeshLink();
        _navAgent.isStopped = false;
    }
}
