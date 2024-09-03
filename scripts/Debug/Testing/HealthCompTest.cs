using AtomicRose.Components;
using Godot;
using System;

public partial class HealthCompTest : Node
{
    [Export]
    HealthComponent myHC;


	public override void _Ready()
	{
        myHC.Death += OnDeath;
	}

    private void OnDeath(){
        GD.Print($"DEBUG: {this.GetParent().Name}'s health component fired death event");
    }
}
