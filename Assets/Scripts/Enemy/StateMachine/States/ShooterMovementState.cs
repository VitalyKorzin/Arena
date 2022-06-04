using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class ShooterMovementState : MovementState
{
    private Shooter _shooter;

    protected override void Awake()
    {
        base.Awake();
        _shooter = GetComponent<Shooter>();
    }

    protected override void Update()
    {
        base.Update();
        _shooter.TakeAim(GetAngle(DirectionToTarget));
    }
}