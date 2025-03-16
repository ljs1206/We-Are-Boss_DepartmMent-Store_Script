using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum VolumeType{
    Bloom = 1,
    ChromaticAberration,
    ChannelMixer,
    ColorAdjustments,
    ColorCurve,
    DepthofField,
    FilmGrain,
    LensDistortion,
    LiftGammaGain,
    MotionBlur,
    PaniniProjection,
    ShadowsMidtonesHighlights,
    SplitToning,
    Tonemapping,
    Vignette,
    WhiteBalance
}

public class VolumeManager : MonoSingleton<VolumeManager>
{
    private List<VolumeComponent> _volumeCompo = new();
    private Dictionary<VolumeType, VolumeComponent> _volumeCompoDic = new();

    private void Awake() {
        Volume volume = FindObjectOfType<Volume>();

        VolumeComponent volumeComponent = new VolumeComponent();
        

        volume.profile.TryGetAllSubclassOf(volumeComponent.GetType(), _volumeCompo);

        foreach(var vc in _volumeCompo){
            foreach(VolumeType enumType in Enum.GetValues(typeof(VolumeType))){
                if(enumType.ToString() == vc.name.Replace("(Clone)", "")){
                    _volumeCompoDic.Add(enumType, vc);
                }
            }
        }
    }

    public void BlinkEffect(Vector4 changeVec4, Sequence seq, int blinkCount,
                                LiftGammaGain _liftGammaGain,
                                Vector4 startVec4, Vector4 endVec4,
                                Action action, float maxTime, float minTime, bool isReverse){
        Vector4 _startVec4 = startVec4;
        Vector4 _endVec4 = endVec4;
        float _tweenTime = 0;

        for(int i = 0; i < blinkCount; ++i){
            _tweenTime = UnityEngine.Random.Range(minTime, maxTime); 

            if(isReverse){
                if(i % 2 != 0) {
                    _startVec4 = _liftGammaGain.lift.value;
                    _endVec4 = Vector4.zero;
                }
                else{
                    _startVec4 = _liftGammaGain.lift.value;
                    _endVec4 = changeVec4;
                }
            }
            else{
                if(i % 2 == 0) {
                    _startVec4 = _liftGammaGain.lift.value;
                    _endVec4 = Vector4.zero;
                }
                else{
                    _startVec4 = _liftGammaGain.lift.value;
                    _endVec4 = changeVec4;
                }
            }


            if(i == blinkCount - 1){
                if(isReverse){
                    _startVec4 = _liftGammaGain.lift.value;
                    _endVec4 = new Vector4(0, 0, 0, -0.7f);
                }
                else{
                    _startVec4 = _liftGammaGain.lift.value;
                    _endVec4 = Vector4.zero;
                }
                _tweenTime = 1f;

                seq.AppendInterval(1f);
                seq.Append(DOTween.To(() => _startVec4, x => _liftGammaGain.lift.value = x, _endVec4, _tweenTime)
                    .OnComplete(() => {
                    action?.Invoke();
                }));;
                    
                continue;
            }
            seq.Append(DOTween.To(() => _startVec4, x => _liftGammaGain.lift.value = x, _endVec4, _tweenTime));
        }
    }

    public T GetVolumeType<T>(VolumeType volumeType) where T : VolumeComponent{
        return _volumeCompoDic[volumeType] as T;
    }

}
