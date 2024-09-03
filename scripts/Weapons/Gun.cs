using AtomicRose.Managers;
using Godot;
using System;
using System.ComponentModel.DataAnnotations;

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

    // Fire Speed
    private float baseAttackSpeed = 1;
    // Bullet Damage
    private float baseBulletDamage;
    // Bullet Count
    private float baseBulletCount;
    // Bullet Speed
    private float baseBulletSpeed;
    // Crit Chance
    private float baseCritChance;
    // Reload speed
    private float baseReloadSpeed;
    // Accuracy

    private float currentAttackSpeed;

    public float AttackSpeed{
        get => currentAttackSpeed;
        private set {
            currentAttackSpeed = baseAttackSpeed * PlayerManager.Instance.playerStats.AttackSpeedMultiplier;
        }
    }

    public override void _Ready()
    {
        PlayerManager.Instance.playerStats.OnStatsChanged += UpdateGunStats;
        CallDeferred(nameof(InitGun));
    }

    private void InitGun(){
        UpdateGunStats();

        attackTimer.WaitTime = AttackSpeed;
    }

    private void UpdateGunStats(){
        AttackSpeed = 0;
        attackTimer.WaitTime = 1/AttackSpeed;
    }

	public override void _Process(double delta)
	{
        Vector2 mousePos = this.GetLocalMousePosition() - vfxNode.Position;
        vfxNode.RotationDegrees = Mathf.RadToDeg(mousePos.Normalized().Angle());
        int spriteFlip = Mathf.Abs(vfxNode.RotationDegrees)<90?1:-1;

        vfxNode.Scale = new Vector2(vfxNode.Scale.X, vfxNode.Scale.Y*(spriteFlip*Mathf.Sign(vfxNode.Scale.Y)));
        
        HandleFire();
    }


    [Export]
    Timer attackTimer;
    private void HandleFire(){
        if(Input.IsActionPressed("action_fire") && attackTimer.TimeLeft <= 0){
            var projectile = NodeSpawner.Instance.SpawnNode(projectileType) as Projectile;
            projectile.GlobalPosition = projectileSpawnPos.GlobalPosition;
            Vector2 mousePos = this.GetLocalMousePosition() - vfxNode.Position;
            projectile.Rotate(mousePos.Normalized().Angle() + Mathf.Pi/2);
            projectile.Direction = mousePos.Normalized();

            attackTimer.Start();
        }
    }
}
