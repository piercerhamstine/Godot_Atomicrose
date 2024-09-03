using Godot;
using System;

namespace AtomicRose.Player;
public partial class PlayerFragments : Node
{
    public void AddFragment(Fragment frag){
        frag.OnAddToPlayer();
    }

    public void RemoveFragment(){

    }
}
