using System;
using UnityEngine;

[RequireComponent(typeof(PathSeeker))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _targetPosition;
    private PathSeeker _pathSeeker;

    public Vector2 CurrentDirection 
        => (_targetPosition - transform.position).normalized;

    private void OnEnable()
    {
        Validate();
        _pathSeeker.TargetPositionChanged += OnTargetPositionChanged;
    }

    private void OnDisable() 
        => _pathSeeker.TargetPositionChanged -= OnTargetPositionChanged;

    private void Awake() => _pathSeeker = GetComponent<PathSeeker>();

    public void Initialize(Transform target)
    {
        if (target == null)
            throw new InvalidOperationException();

        _pathSeeker.Initialize(target);
    }

    public void Move()
    {
        if (_targetPosition != null)
            transform.Translate(_speed * Time.deltaTime * CurrentDirection);
    }

    private void OnTargetPositionChanged(Vector3 targetPosition)
        => _targetPosition = targetPosition;

    private void Validate()
    {
        if (_speed <= 0)
            throw new InvalidOperationException();
    }
}