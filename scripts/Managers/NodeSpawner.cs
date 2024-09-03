using Godot;
using System;

public partial class NodeSpawner : Node
{
    public static NodeSpawner Instance {get; private set;}
	public override void _Ready()
	{
        Instance = this;
	}

    public Node SpawnNode(PackedScene scene, Node parent = null){
        var newEntity = scene.Instantiate();
        if(parent == null)
            GetTree().CurrentScene.AddChild(newEntity);
        else
            parent.AddChild(newEntity);
            
        return newEntity;
    }
}
