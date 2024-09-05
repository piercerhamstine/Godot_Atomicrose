using Godot;
using System;

using AtomicRose.Player;

namespace AtomicRose.Managers;
public partial class PlayerManager : Node
{
    public static PlayerManager Instance;

    [Export]
    public PlayerController playerController;
    [Export]
    public PlayerStats playerStats;
    [Export]
    public PlayerFragments playerFragments;
    
	public override void _Ready()
	{
        Instance = this;
	}
}
