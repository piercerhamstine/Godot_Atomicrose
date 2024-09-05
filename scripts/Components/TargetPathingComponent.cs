using Godot;
using System;

public partial class TargetPathingComponent : Node2D
{
    [Export]
    VelocityComponent velocityComponent;

    [Export]
    private Timer targetUpdateDelay;

    private Vector2 currentTargetPosition;
    private Vector2 previousTargetPosition;

    bool reachedTarget;

    // TODO: Fix this
    public void SetTargetPosition(Vector2 targetPosition){
        if(!targetUpdateDelay.IsStopped()){
            return;
        }

        GD.Print("Position Updated");
        previousTargetPosition = currentTargetPosition;
        currentTargetPosition = targetPosition;
        reachedTarget = false;
        targetUpdateDelay.Start();
    }

    // Follows target until reaching the target
    public void FollowTarget(float distToStop = 0.2f){
        if(reachedTarget){
            velocityComponent.Direction = Vector2.Zero;
            return;
        }

        Vector2 direction = (currentTargetPosition - GlobalPosition).Normalized();
        velocityComponent.Direction = direction;

        if(GlobalPosition.DistanceTo(currentTargetPosition) <= distToStop)
            reachedTarget = true;
    }

    // Follows the direction of the target without stopping.
    public void FollowTargetDirection(){

    }
}
