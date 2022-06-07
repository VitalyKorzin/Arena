using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Magnet : Booster
{
    [SerializeField] private float _attractionForce;
    [SerializeField] private float _attractionRadius;

    private CircleCollider2D _collider;
    private Coroutine _attractionJob;
    private Transform _defaultParent;
    private float _defaultRadius;

    public float AttractionForce => _attractionForce;
    public bool IsActive { get; private set; }

    private void OnDisable()
    {
        if (_attractionJob != null)
            StopCoroutine(_attractionJob);
    }


    private void Awake()
    {
        Validate();
        _collider = GetComponent<CircleCollider2D>();
        _defaultRadius = _collider.radius;
        _defaultParent = transform.parent;
    }

    public void PickUp(Transform newParent)
    {
        transform.parent = newParent;
        transform.localPosition = Vector2.zero;
        _attractionJob = StartCoroutine(AttractCoins());
    }

    private IEnumerator AttractCoins()
    {
        Activate();
        yield return new WaitForSeconds(Duration);
        Deactivate();
    }

    private new void Activate()
    {
        IsActive = true;
        _collider.radius = _attractionRadius;
    }

    private new void Deactivate()
    {
        transform.parent = _defaultParent;
        _collider.radius = _defaultRadius;
        base.Deactivate();
    }

    private void Validate()
    {
        if (_attractionForce <= 0)
            throw new InvalidOperationException();

        if (_attractionRadius <= 0)
            throw new InvalidOperationException();
    }
}