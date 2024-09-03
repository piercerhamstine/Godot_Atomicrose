using AtomicRose.Player;
using Godot;
using System;

public partial class PlayerPassive : Node
{
    [Export]
    public float modifierValue;
    [Export]
    public StatType modifierType;
}
