using Godot;
using System;

public partial class NodeSpawner : Node
{
    public static NodeSpawner Instance {get; private set;}
	public override void _Ready()
	{
        Instance = this;
	}

    public Node SpawnNode(PackedScene scene){
        var newEntity = scene.Instantiate();
        GetTree().CurrentScene.AddChild(newEntity);
        return newEntity;
    }
}
