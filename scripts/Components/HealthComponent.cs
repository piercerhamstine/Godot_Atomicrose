using Godot;
using System;

namespace AtomicRose.Components;

public partial class HealthChangeResult:Resource{
    public float PreviousHealth;
    public float CurrentHealth;
    public bool WasHealed;
}

public partial class HealthComponent : Node
{
    [Signal]
    public delegate void DeathEventHandler();
	
    [Signal]
    public delegate void OnHealthChangedEventHandler(HealthChangeResult hpChangeResult);

    private float previousHealth;
    private float currentHealth;
    private float maxHealth;

    public float CurrentHealth{
        get => currentHealth;
        private set{
            previousHealth = currentHealth;
            currentHealth = Mathf.Clamp(value, 0, maxHealth);

            HealthChangeResult hpcr = new HealthChangeResult{
                PreviousHealth = previousHealth,
                CurrentHealth = currentHealth,
                WasHealed = currentHealth > previousHealth
            };
            
            EmitSignal(SignalName.OnHealthChanged, hpcr);
            if(!IsLiving){
                EmitSignal(SignalName.Death);
            }
        }
    }

    [Export]
    public float MaxHealth{
        get => maxHealth;
        private set{
            maxHealth = value;
            if(CurrentHealth > maxHealth){
                CurrentHealth = maxHealth;
            }
        }
    }

    private bool IsLiving{
        get{
            return !Mathf.IsEqualApprox(CurrentHealth, 0f);
        }
    }

    public void Damage(float value){
        CurrentHealth -= value;
    }
    
    public void Heal(float value){
        Damage(-value);
    }

    private void InitHealthComponent(){
        CurrentHealth = maxHealth;
    }

	public override void _Ready()
	{
        CallDeferred(nameof(InitHealthComponent));
	}
}
