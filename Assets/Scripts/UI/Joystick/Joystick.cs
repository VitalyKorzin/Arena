using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _handle;
    [SerializeField] private HeroSpawner _heroSpawner;

    private readonly float _maximumMagnitude = 1f;
    private Vector2 _position;
    private Vector2 _radius;

    protected Hero Hero;

    public Vector2 Direction { get; private set; }
    public bool IsUsing => Direction != Vector2.zero;

    private void OnEnable()
    {
        Validate();
        _heroSpawner.Spawned += OnHeroSpawned;
    }

    private void OnDisable() 
        => _heroSpawner.Spawned -= OnHeroSpawned;

    private void Start()
    {
        _position = RectTransformUtility.WorldToScreenPoint(Camera.main, _background.position);
        _radius = _background.sizeDelta / 2f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Direction = (eventData.position - _position) / _radius;
        Direction = Direction.magnitude > _maximumMagnitude ? Direction.normalized : Direction;
        _handle.anchoredPosition = Direction * _radius;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        Direction = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;
    }

    protected virtual void Validate()
    {
        if (_background == null)
            throw new InvalidOperationException();

        if (_handle == null)
            throw new InvalidOperationException();

        if (_heroSpawner == null)
            throw new InvalidOperationException();
    }

    private void OnHeroSpawned(Hero hero)
        => Hero = hero != null ? hero : throw new InvalidOperationException();
}