using UnityEngine;
using Event = Parkjung2016.Event;

public class AnotherBossEvent : Event
{
    [SerializeField] private AnotherBoss _anotherBossPrefab;
    [SerializeField] private Transform spawnPoint;

    public override void HappenEvent(StoreZone owner)
    {
        AnotherBoss anotherBoss = Instantiate(_anotherBossPrefab, spawnPoint);
        anotherBoss.Init();
    }
}
