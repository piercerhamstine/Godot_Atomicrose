using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
	public const float Speed = 100.0f;
    private float currentSpeed;
	
    Vector2 direction;

    StateMachine stateMachine = new();

    [Export]
    private Timer dashActiveTimer;
    [Export]
    private Timer dashCooldownTimer;
    [Export]
    public float DashSpeed = 600.0f;
    [Export]
    GpuParticles2D dashParticles;
    private bool isDashing;
    private void DashState(){
        // If the player is dashing and the dash active is over
        if(isDashing && dashActiveTimer.IsStopped()){
            isDashing = false;
            dashCooldownTimer.Start();
            currentSpeed = Speed;
            stateMachine.ChangeState(BaseState);
        }
        
        // If the user isn't dashing and their dash cooldown is finished.
        if(dashActiveTimer.IsStopped() && dashCooldownTimer.IsStopped()){
            dashActiveTimer.Start();
            isDashing = true;
            currentSpeed = DashSpeed;
            dashParticles.Emitting = true;
        }
    }

    private void BaseState(){
    }

    public override void _Ready()
    {
        currentSpeed = Speed;
        stateMachine.AddState(DashState);
        stateMachine.AddState(BaseState);
        stateMachine.SetInitialState(BaseState);
    }

    public override void _Process(double delta)
    {
        direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        stateMachine.Update();
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

        if(Input.IsActionJustPressed("action_dash") && dashCooldownTimer.IsStopped()){
            stateMachine.ChangeState(DashState);
        }

		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * currentSpeed;
            velocity.Y = direction.Y * currentSpeed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
        }



		Velocity = velocity;
		MoveAndSlide();
	}
}
