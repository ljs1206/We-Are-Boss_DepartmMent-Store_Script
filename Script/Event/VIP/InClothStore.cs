using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class VIP : Customer
{
    public float _waitTime;
    [HideInInspector] public float _oldTime;

    public override void Awake()
    {
        base.Awake();

        _oldTime = Time.time;

        itemData =
            SpawnManager.Instance.dataSO.storeZone.buyAbleItemData[
                Random.Range(0, SpawnManager.Instance.dataSO.storeZone.buyAbleItemData.Length)];
        InitCompo();

        StateMachine = new CustomerStateMachine();

        foreach (CustomerEnumState stateEnum in Enum.GetValues(typeof(CustomerEnumState)))
        {
            string typeName = stateEnum.ToString();

            try
            {
                Type t = Type.GetType($"Customer{typeName}State");
                CustomerState state = Activator.CreateInstance(t, this, StateMachine, typeName) as CustomerState;

                StateMachine.AddState(stateEnum, state);
            }
            catch (Exception ex)
            {
                Debug.LogError($"{typeName} is loading error check Message");
                Debug.LogError(ex.Message);
            }
        }
    }

    private void Start()
    {
        StateMachine.Initialize(CustomerEnumState.EnterStoreZone, this);
    }

    private void OnEnable()
    {
        customerUI.RandomObj();
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
        if(Time.time - _oldTime >= _waitTime || Keyboard.current.nKey.wasPressedThisFrame){
            GetOut();
            Debug.Log("시간 초과");
        }
    }
}
