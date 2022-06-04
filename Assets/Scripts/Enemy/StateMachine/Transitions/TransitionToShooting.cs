using UnityEngine;

public class TransitionToShooting : TransitionBecauseOfObstacles
{
    protected override bool CanTransit() 
        => DistanceToTarget < TransitionRange && ObstaclesSeeker.ThereAreNoObstacles();
}