using Godot;
using System;
using AtomicRose.Managers;

namespace AtomicRose;
public partial class Fragment : Node
{
    [Export]
    Node passives;

    public void OnAddToPlayer(){
        GD.Print("Added to player");
        
        foreach(var passive in passives.GetChildren()){
            if(passive is PlayerPassive plyrPassive)
                PlayerManager.Instance.playerStats.AddModifier(plyrPassive.modifierType, plyrPassive.modifierValue);
        }
    }
    
    private void OnRemoveFromPlayer(){
        foreach(var passive in passives.GetChildren()){
            if(passive is PlayerPassive plyrPassive)
                PlayerManager.Instance.playerStats.RemoveModifier(plyrPassive.modifierType, plyrPassive.modifierValue);
        }
    }
}
