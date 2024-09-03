using Godot;
using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace AtomicRose.Player;

public enum StatType
{
    Health,
    AttackSpeed,
};

public partial class PlayerStats : Node
{
    // TODO: Look into a better way of handling this
    [Signal]
    public delegate void OnStatsChangedEventHandler();

    private float baseHealthMultiplier = 1;
    private float baseMoveSpeedMultiplier = 1;
    private float baseAttackSpeedMultiplier = 1;
    private float baseCritChanceMultiplier = 1;
    private float baseCritDamageMultiplier = 1;

    private float currentHealthMultiplier;
    private float currentMoveSpeedMultiplier;
    private float currentAttackSpeedMultiplier;
    private float currentCritChanceMultiplier;
    private float currentCritDamageMultiplier;

    public float HealthMultiplier{
        get => currentHealthMultiplier;
    }

    public float MoveSpeedMultiplier{
        get => currentMoveSpeedMultiplier;
    }

    public float AttackSpeedMultiplier{
        get => currentAttackSpeedMultiplier;
        set{
            currentAttackSpeedMultiplier += value;
        }
    }

    public float CritChanceMultiplier{
        get => currentCritChanceMultiplier;
    }

    public float CritDamageMultiplier{
        get => currentCritDamageMultiplier;
    }

    private void InitPlayerStats(){
        currentHealthMultiplier = baseHealthMultiplier;
        currentMoveSpeedMultiplier = baseMoveSpeedMultiplier;
        currentAttackSpeedMultiplier = baseAttackSpeedMultiplier;
        currentCritChanceMultiplier = baseCritChanceMultiplier;
        currentCritDamageMultiplier = baseCritDamageMultiplier;
    }

    public void AddModifier(StatType type, float value){
        switch(type){
            case StatType.AttackSpeed:{
                AttackSpeedMultiplier += value;
                break;
            }
        }

        EmitSignal(SignalName.OnStatsChanged);
    }

    public void RemoveModifier(StatType type, float value){
        AddModifier(type, -value);
    }

    public override void _Ready()
    {
        CallDeferred(nameof(InitPlayerStats));
    }
}
