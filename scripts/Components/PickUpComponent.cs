using Godot;
using System;

public partial class PickUpComponent : Area2D
{
    [Signal]
    public delegate void OnPickUpEventHandler();

    public override void _Ready()
    {
        Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
    }

    private void OnBodyEntered(Node2D other){
        if(other is PlayerController player){
            GD.Print("Player");
            EmitSignal(SignalName.OnPickUp);
        }
    }
}
