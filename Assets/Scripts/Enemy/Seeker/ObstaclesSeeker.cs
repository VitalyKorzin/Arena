using System;
using UnityEngine;

public class ObstaclesSeeker : MonoBehaviour
{
    [SerializeField] private ContactFilter2D _obstaclesFilter;

    private readonly RaycastHit2D[] _obstacles = new RaycastHit2D[1];
    private uint _obstaclesCount;
    private Transform _target;

    private void OnEnable() => Validate();

    public void Initialize(Transform target)
        => _target = target != null ? target : throw new InvalidOperationException();

    public bool ThereAreNoObstacles()
    {
        _obstaclesCount = (uint)Physics2D.Raycast(transform.position, GetDirectionToTarget(), 
            _obstaclesFilter, _obstacles, GetDistanceToTarget());
        return _obstaclesCount == uint.MinValue;
    }

    private void Validate()
    {
        if (_obstaclesFilter.useLayerMask == false)
            throw new InvalidOperationException();
    }

    private Vector2 GetDirectionToTarget() 
        => (_target.transform.position - transform.position).normalized;

    private float GetDistanceToTarget()
        => Vector2.Distance(_target.position, transform.position);
}