using AtomicRose.Components;
using AtomicRose.Managers;
using Godot;
using System;

public partial class Spawn : CharacterBody2D
{
    [Export]
    HealthComponent myHealthComponent;
    [Export]
    VelocityComponent myVelocityComponent;
    [Export]
    TargetPathingComponent myTargetPathingComponent;

    [Export]
    Timer moveCoolDown;

    private StateMachine stateMachine = new();

    public override void _Ready(){
        myHealthComponent.Death += OnDeath;

        stateMachine.AddState(PathToPlayerState);
        stateMachine.SetInitialState(PathToPlayerState);
    }

    public override void _Process(double delta)
    {
        stateMachine.Update();
    }

    private void PathToPlayerState(){
        myTargetPathingComponent.SetTargetPosition(PlayerManager.Instance.playerController.GlobalPosition);
        myTargetPathingComponent.FollowTarget();
        myVelocityComponent.Move(this);
    }

    private void OnDeath(){
        QueueFree();
    }
}
