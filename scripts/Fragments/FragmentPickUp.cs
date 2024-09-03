using AtomicRose.Managers;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtomicRose;
public partial class FragmentPickUp : Node
{
    [Export]
    PackedScene fragmentScene;

    [Export]
    PickUpComponent myPickUpComponent;

    public override void _Ready()
    {
        myPickUpComponent.OnPickUp += OnPickedUp;
    }

    private void OnPickedUp(){
        AddFrag();
        // CallDeferred(nameof(AddFrag));
    }

    private void AddFrag(){
        var frag = NodeSpawner.Instance.SpawnNode(fragmentScene, PlayerManager.Instance.playerFragments) as Fragment;
        PlayerManager.Instance.playerFragments.AddFragment(frag);
        QueueFree();
    }
}
