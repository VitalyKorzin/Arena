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

    private void OnEnable() => Validate();

    private void OnDisable()
    {
        if (_attractionJob != null)
            StopCoroutine(_attractionJob);

        IsActive = false;
        _collider.radius = _defaultRadius;
    }


    private void Awake()
    {
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
        IsActive = true;
        _collider.radius = _attractionRadius;
        yield return new WaitForSeconds(Duration);
        transform.parent = _defaultParent;
        Deactivate();
    }

    private void Validate()
    {
        if (_attractionForce <= 0)
            throw new InvalidOperationException();

        if (_attractionRadius <= 0)
            throw new InvalidOperationException();
    }
}