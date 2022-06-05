using System;
using UnityEngine;

public class MovementJoystick : Joystick
{
    [SerializeField] private ShootingJoystick _shootingJoystick;

    private void Update()
    {
        if (_shootingJoystick.IsUsing == false)
            Hero.TakeAim(Direction);
    }

    private void FixedUpdate() => Hero.Move(Direction);

    protected override void Validate()
    {
        base.Validate();

        if (_shootingJoystick == null)
            throw new InvalidOperationException();
    }
}