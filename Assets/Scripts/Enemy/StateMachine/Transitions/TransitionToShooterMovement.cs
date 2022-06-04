using UnityEngine;

public class TransitionToShooterMovement : TransitionBecauseOfObstacles
{
    protected override bool CanTransit()
        => DistanceToTarget > TransitionRange && ObstaclesSeeker.ThereAreNoObstacles();
}