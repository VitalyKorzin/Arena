using System;
using UnityEngine;

public abstract class Bullet : PoolObject
{
    [SerializeField] private float _movementSpeed;

    private void OnEnable() => Validate();

    private void Update()
        => transform.Translate(_movementSpeed * Time.deltaTime * Vector2.right);

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PoolObject _) == false)
            Deactivate();
    }

    private void Validate()
    {
        if (_movementSpeed <= 0)
            throw new InvalidOperationException();
    }
}