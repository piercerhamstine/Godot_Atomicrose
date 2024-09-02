using Godot;
using System;

// This is purely for prototyping at the moment.
// TODO: refactor and implement properly. 
public partial class Gun : Node2D
{
    [Export]
    PackedScene projectileType;
    [Export]
    Node2D vfxNode;
    [Export]
    Node2D projectileSpawnPos;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Vector2 mousePos = this.GetLocalMousePosition() - vfxNode.Position;
        vfxNode.RotationDegrees = Mathf.RadToDeg(mousePos.Normalized().Angle());
        int spriteFlip = Mathf.Abs(vfxNode.RotationDegrees)<90?1:-1;

        vfxNode.Scale = new Vector2(vfxNode.Scale.X, vfxNode.Scale.Y*(spriteFlip*Mathf.Sign(vfxNode.Scale.Y)));
        
        HandleFire();
    }

    private void HandleFire(){
        if(Input.IsActionJustPressed("action_fire")){
            var projectile = NodeSpawner.Instance.SpawnNode(projectileType) as Projectile;
            projectile.GlobalPosition = projectileSpawnPos.GlobalPosition;
            Vector2 mousePos = this.GetLocalMousePosition() - vfxNode.Position;
            projectile.Rotate(mousePos.Normalized().Angle() + Mathf.Pi/2);
            projectile.Direction = mousePos.Normalized();
        }
    }
}
