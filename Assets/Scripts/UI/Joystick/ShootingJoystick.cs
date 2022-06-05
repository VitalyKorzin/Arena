using UnityEngine;
using UnityEngine.EventSystems;

public class ShootingJoystick : Joystick, IBeginDragHandler
{
    private Coroutine _shootingJob;

    private void Update()
    {
        if (IsUsing)
            Hero.TakeAim(Direction);
    }

    public void OnBeginDrag(PointerEventData eventData)
        => _shootingJob = StartCoroutine(Hero.Shoot());

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (_shootingJob != null)
            StopCoroutine(_shootingJob);

        base.OnEndDrag(eventData);
    }
}