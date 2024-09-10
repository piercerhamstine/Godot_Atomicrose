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
    private Vector2 direction;

    public void SetUpdateDelay(float time){
        targetUpdateDelay.WaitTime = time;
    }

    public void SetTargetPosition(Vector2 targetPosition){
        if(!targetUpdateDelay.IsStopped()){
            return;
        }

        GD.Print("Position Updated");
        targetUpdateDelay.Call("start");
        previousTargetPosition = currentTargetPosition;
        currentTargetPosition = targetPosition;
    }

    // TODO: Fix this, small chance to get stuck moving between positive and neg direction
    // Follows target until reaching the target
    public void FollowTarget(float distToStop = 0.5f){
        if(GlobalPosition.DistanceSquaredTo(currentTargetPosition) <= distToStop){
            velocityComponent.Direction = Vector2.Zero;
            return;
        }

        direction = (currentTargetPosition - GlobalPosition).Normalized();
        velocityComponent.Direction = direction;
    }

    // Follows the direction of the target without stopping.
    public void FollowTargetDirection(){
    }
}
