using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class DoodleAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [Header("����� �������� ����� ������ � ����.")]
    [SerializeField] private int _timeDelayRotation;

    public async UniTaskVoid PlayRotation()
    {
        await UniTask.Delay(_timeDelayRotation);
        _animator.Play("Rotation", 0, 0);
    }
}
