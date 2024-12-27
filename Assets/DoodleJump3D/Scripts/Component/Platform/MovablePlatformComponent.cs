using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MovablePlatformComponent : BaseComponent
{
    private int _route;
    private float _timer;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _delayChangeRoute;

    public override void Start()
        => base.Start();

    public void ChangeRoute()
    {
        _route *= -1;
        _timer = 0;
    }

    private void OnEnable()
    {
        _timer = 0;

        SetStartMoveRoute();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _delayChangeRoute)
            ChangeRoute();
        Move();
    }

    private void SetStartMoveRoute()
    {
        var startRoute = UnityEngine.Random.Range(-1, 1);
        _route = startRoute == -1 || startRoute == 0 ? -1 : 1;
    }

    private void Move()
    {
        _rigidbody.velocity = Vector3.left * _speed * _route;
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }
}
