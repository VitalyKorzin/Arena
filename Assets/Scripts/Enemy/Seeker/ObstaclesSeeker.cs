using System;
using UnityEngine;

public class ObstaclesSeeker : MonoBehaviour
{
    [SerializeField] private ContactFilter2D _obstaclesFilter;

    private readonly RaycastHit2D[] _obstacles = new RaycastHit2D[1];
    private uint _obstaclesCount;

    private void Awake() => Validate();

    public bool ThereAreNoObstacles(Vector2 direction, float distance)
    {
        _obstaclesCount = (uint)Physics2D.Raycast(transform.position, direction, 
            _obstaclesFilter, _obstacles, distance);
        return _obstaclesCount == uint.MinValue;
    }

    private void Validate()
    {
        if (_obstaclesFilter.useLayerMask == false)
            throw new InvalidOperationException();
    }
}