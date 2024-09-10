using Godot;
using System;

namespace AtomicRose.Components;
public partial class HurtboxComponent : Area2D
{
    [Export]
    HealthComponent myHealthComponent;

    [Signal]
    public delegate void HitByProjectileEventHandler();

    [Signal]
    public delegate void HitByHitboxEventHandler();

    private void OnAreaEntered(Area2D otherArea){
        if(otherArea is Projectile projectile){
            OnHitByProjectile(projectile);
        }
        else if(otherArea is HitboxComponent hitbox){
            OnHitByHitbox(hitbox);
        }
    }

    private void OnHitByProjectile(Projectile projectile){
        if(myHealthComponent == null){
            GD.PrintErr($"{this.GetParent().Name}'s child hurtbox component missing reference to HealthComponent");
            return;
        }
        
        myHealthComponent.Damage(projectile.Damage);
        EmitSignal(SignalName.HitByProjectile);
    }

    private void OnHitByHitbox(HitboxComponent hitbox){
        if(myHealthComponent == null){
            GD.PrintErr($"{this.GetParent().Name}'s child hurtbox component missing reference to HealthComponent");
            return;
        }

        myHealthComponent.Damage(hitbox.Damage);
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Connect("area_entered", new Callable(this, nameof(OnAreaEntered)));
	}
}
