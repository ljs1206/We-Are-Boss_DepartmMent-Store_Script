using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class BlackOut : MonoBehaviour
{
    private Transform _backGround;
    private Tween _ScaleDown;
    private Tween _ScaleUp;
    private bool _isEnter;
    private bool _endStart;
    private Transform _lightTrm;
    private Transform _directionalLight;
    private Transform _blackOutTrm;
    private StoreZone _owner;
    private LiftGammaGain _liftGammaGain;

    private LookAtTarget _lookAtTarget;
    
    #region Tween
    private Tween _gammaChangeTween;
    private Vector4 _startVec4;
    private Vector4 _endVec4;
    private float _tweenTime;
    [SerializeField] private int blinkCount = 12;
    #endregion

    public void Init(StoreZone owner, Transform blackOutTrm, LookAtTarget lookAtTarget) {
        _blackOutTrm = blackOutTrm;
        _owner = owner;
        _lookAtTarget = lookAtTarget;

        _liftGammaGain = VolumeManager.Instance.GetVolumeType
            <LiftGammaGain>(VolumeType.LiftGammaGain);
            
        _startVec4 = _liftGammaGain.gamma.value;
        _endVec4 = Vector4.zero;

        _gammaChangeTween = DOTween.To(() => _startVec4, x => _liftGammaGain.gamma.value = x, _endVec4, _tweenTime);
        _backGround = transform.Find("BlackOutCanvas/Background");

        _lightTrm = GameObject.FindWithTag("Light").transform;

        Transform[] lights = _lightTrm.GetComponentsInChildren<Transform>();
        foreach(var light in lights){
            if(light.name == "Directional Light"){
                _directionalLight = light.transform;
            }
        }

        _ScaleUp = _backGround.DOScale(1, 0.5f);
        _ScaleDown = _backGround.DOScale(0, 0.5f);

        _backGround.transform.localScale = Vector3.zero;

        _isEnter = false;
        _endStart = false;
    }

    private void Update() {
        if(StoreManager.Instance.currentStore != null){
            _lookAtTarget._targetTrm = _owner.exitPlayerPos;
        }
        else{
            _lookAtTarget._targetTrm = _blackOutTrm;
        }

        if(_isEnter && Input.GetKeyDown(KeyCode.E) && !_endStart){
            _endStart = true;

            _liftGammaGain.gamma.value = new Vector4(0, 0, 0, -0.7f);
            
            Sequence seq = DOTween.Sequence();

            VolumeManager.Instance.BlinkEffect(new Vector4(0, 0, 0, -0.7f), seq, blinkCount,
                                                _liftGammaGain, _startVec4, _endVec4, CompleteEvent,
                                                0.1f, 0.2f, false);
        }
    }

    public void CompleteEvent(){
        _directionalLight.gameObject.SetActive(true);
        _liftGammaGain.active = false;
        SoundManager.Instance.VolumeSetMaster(100);
        _lookAtTarget.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Player player))
        {
                _isEnter = true;
                DOTween.Complete(_ScaleUp);
                _backGround.DOScale(1, 0.5f);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent(out Player player))
        {
                _isEnter = false;
                DOTween.Complete(_ScaleDown);
                _backGround.DOScale(0, 0.5f);
        }
    }
}
