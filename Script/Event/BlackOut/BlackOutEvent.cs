using UnityEngine;
using UnityEngine.Rendering.Universal;
using Event = Parkjung2016.Event;
using DG.Tweening;
using Unity.Collections;

public class BlackOutEvent : Event
{
    Transform _lightTrm;
    Transform _directionalLight;
    [SerializeField] private Vector4 startColor;
    [SerializeField] private Vector4 endColor;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private BlackOut _blackOutPrefab;
    [SerializeField] private LookAtTarget _lookAtTarget;
    private LiftGammaGain liftGammaGain;

    public override void HappenEvent(StoreZone owner)
    {
        BlackOut blackOut = Instantiate(_blackOutPrefab, _spawnPoint);
        blackOut.Init(owner, blackOut.transform, _lookAtTarget);

        _lightTrm = GameObject.FindWithTag("Light").transform;

        Transform[] lights = _lightTrm.GetComponentsInChildren<Transform>();

        foreach(var light in lights){
            if(light.name == "Directional Light"){
                liftGammaGain = VolumeManager.Instance.GetVolumeType
                        <LiftGammaGain>(VolumeType.LiftGammaGain);
                _directionalLight = light;
                light.gameObject.SetActive(false);

                liftGammaGain.active = true;
                Sequence seq = DOTween.Sequence();
                VolumeManager.Instance.BlinkEffect(new Vector4(0, 0, 0, -0.7f), seq, 11, liftGammaGain, 
                                                liftGammaGain.gamma.value, new Vector4(0, 0, 0, -0.7f), 
                                                null, 0.1f, 0.2f, true);

                SoundManager.Instance.VolumeSetMusic(0);
            }
        }
    }
}
