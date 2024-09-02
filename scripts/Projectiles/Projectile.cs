using Godot;
using Microsoft.VisualBasic.FileIO;
using System;

// This is for prototyping
// TODO: refactor, re-implement properly.
public partial class Projectile : Area2D
{
    [Export]
    private float speed;

    private Vector2 direction;
    public Vector2 Direction { get{ return direction;} set{direction=value;} }

    public override void _PhysicsProcess(double delta)
    {
        this.Position += direction * speed;
    }
}
