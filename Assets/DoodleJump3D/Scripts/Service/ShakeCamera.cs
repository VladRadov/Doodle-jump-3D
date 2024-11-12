using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;

public class ShakeCamera : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin _cameraNoise;

    [SerializeField] private float _amplitudeGain;
    [SerializeField] private float _frequencyGain;
    [SerializeField] private int _durationShake;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    public async UniTaskVoid Shake()
    {
        _cameraNoise.m_AmplitudeGain = _amplitudeGain;
        _cameraNoise.m_FrequencyGain = _frequencyGain;
        await UniTask.Delay(_durationShake);
        _cameraNoise.m_AmplitudeGain = 0;
        _cameraNoise.m_FrequencyGain = 0;
    }

    private void Start()
    {
        if (_virtualCamera != null)
            _cameraNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnValidate()
    {
        if (_virtualCamera == null)
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
}
