using System;
using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Seeker))]
public class PathSeeker : MonoBehaviour
{
    [SerializeField] private float _secondsBetweenPathUpdate;
    [SerializeField] private float _distanseBetweenWaypoints;

    private Path _path;
    private Seeker _seeker;
    private Transform _target;
    private uint _currentWaypointIndex;
    private Coroutine _updatingPathJob;
    private WaitForSeconds _delayBetweenPathUpdate;

    public event UnityAction<Vector3> TargetPositionChanged;

    private void OnDisable()
    {
        if (_updatingPathJob != null)
            StopCoroutine(_updatingPathJob);
    }

    private void Awake()
    {
        Validate();
        _seeker = GetComponent<Seeker>();
        _delayBetweenPathUpdate = new WaitForSeconds(_secondsBetweenPathUpdate);
    }

    private void Update()
    {
        if (_path == null)
            return;

        UpdateCurrentWaypoint();
    }

    public void Initialize(Transform target)
    {
        _target = target != null ? target : throw new InvalidOperationException();
        _updatingPathJob = StartCoroutine(UpdatePath());
    }

    private IEnumerator UpdatePath()
    {
        while (_seeker.IsDone())
        {
            _seeker.StartPath(transform.position, _target.transform.position, OnPathComplete);
            yield return _delayBetweenPathUpdate;
        }
    }

    private void OnPathComplete(Path path)
    {
        if (path.error == false)
            ChangePath(path);
    }

    private void ChangePath(Path path)
    {
        _path = path;
        _currentWaypointIndex = uint.MinValue;
        NotifyOnTargetPositionChanged();
    }

    private void UpdateCurrentWaypoint()
    {
        if (GetDistanceToCurrentWaypoint() < _distanseBetweenWaypoints)
            TransitToNextWaypoint();
    }

    private void TransitToNextWaypoint()
    {
        _currentWaypointIndex = (uint)Mathf.Clamp(++_currentWaypointIndex,
            uint.MinValue, _path.vectorPath.Count - 1);
        NotifyOnTargetPositionChanged();
    }

    private float GetDistanceToCurrentWaypoint()
        => Vector2.Distance(transform.position, GetCurrentWaypoint());

    private Vector3 GetCurrentWaypoint()
        => _path.vectorPath[(int)_currentWaypointIndex];

    private void NotifyOnTargetPositionChanged() 
        => TargetPositionChanged?.Invoke(GetCurrentWaypoint());

    private void Validate()
    {
        if (_secondsBetweenPathUpdate <= 0)
            throw new InvalidOperationException();

        if (_distanseBetweenWaypoints <= 0)
            throw new InvalidOperationException();
    }
}