using Godot;
using System;
using System.Runtime.Versioning;

public partial class Teleporter : Node2D, IInteractable
{
    [Export]
    Area2D teleportzone_1;
    [Export]
    Area2D teleportzone_2;

    [Export]
    bool isTwoWay;

    public void Interact()
    {
        throw new NotImplementedException();
    }
}
