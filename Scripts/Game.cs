using Godot;
using Godot.Collections;
using System;

public partial class Game : Node2D
{
    [Export]
    public PackedScene Logs { get; set; }
    [Export]
    public float Speed { get; set; }

    private Array<Node2D> obstacles = new();

    public override void _Ready()
    {
    }

    public override void _Process(double delta)
    {
        if (obstacles.Count < 1)
        {
            RandomNumberGenerator rng = new();
            Logs newLog = Logs.Instantiate<Logs>();
            newLog.Position = new Vector2(120, rng.RandiRange(-35, 35));
            AddChild(newLog);
            obstacles.Add(newLog);
        }
        foreach (Node2D node in obstacles)
        {
            node.Position = new Vector2(node.Position.X - Speed, node.Position.Y);
            if (node.Position.X < -250)
            {
                obstacles.Remove(node);
                node.QueueFree();
            }
        }
    }
}
