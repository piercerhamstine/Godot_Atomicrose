using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class GlobalTriggerManager : Node
{
    public static GlobalTriggerManager Instance {get; private set;}
	
    public override void _Ready()
	{
        Instance = this;
	}

    [Signal]
    public delegate void OnPlayerDeathEventHandler();
    [Signal]
    public delegate void OnPlayerDamagedEventHandler();
    [Signal]
    public delegate void OnPlayerHealedEventHandler();
}
