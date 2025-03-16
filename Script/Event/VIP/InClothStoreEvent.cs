using UnityEngine;
using Event = Parkjung2016.Event;

public class InClothStoreEvent : Event
{
    [SerializeField] private Customer _vipPrefab;

    public override void HappenEvent(StoreZone owner)
    {
        Customer cus = Instantiate(_vipPrefab, SpawnManager.Instance.transform);
        cus.transform.position = SpawnManager.Instance.startTrm.localPosition;
        cus.currentDataSO = SpawnManager.Instance.dataSO; 

        SpawnManager.Instance.dataSO.storeZone.currentCustomer.Enqueue(cus);

        SpawnManager.Instance.SelectSO();
    }
}
