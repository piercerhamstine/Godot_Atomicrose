using AtomicRose.Components;
using Godot;
using System;

namespace AtomicRose.Components;
public partial class HitflashComponent : Node2D
{
    [Export]
    AnimationPlayer animationPlayer;
    [Export]HealthComponent myHealthComponent;

    private void HitTest(){
        if(Input.IsActionJustPressed("action_dash")){
            GD.Print("Got Input");
            myHealthComponent.Damage(1);
        }
    }

    public override void _Process(double delta)
    {
        HitTest();
    }

    private void HandleHitFlash(HealthChangeResult hpcr){
        GD.Print("Health Changed");

        if(!hpcr.WasHealed){
            animationPlayer.Play("hit_flash");
        }
    }

    public override void _Ready()
    {
        myHealthComponent.OnHealthChanged += HandleHitFlash;
    }
}
