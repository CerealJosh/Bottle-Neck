using Godot;
using System.Collections.Generic;

public partial class Game : Node2D
{
    [Export]
    public PackedScene Logs { get; set; }
    [Export]
    public float Speed { get; set; }

    private List<Node2D> obstacles = new();

    private int Score = 0;

    public override void _Ready()
    {
        obstacles.Clear();
        var logTimer = GetNode<Timer>("LogTimer");
        logTimer.Timeout += createLogs;
        logTimer.Start();
    }

    public override void _Process(double delta)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].Position = new Vector2(obstacles[i].Position.X - Speed * (float)delta, obstacles[i].Position.Y);
            if (obstacles[i].Position.X < -250)
            {
                obstacles[i].QueueFree();
                obstacles.Remove(obstacles[i]);
            }
        }
    }

    private void createLogs()
    {
        if (obstacles.Count < 5)
        {
            RandomNumberGenerator rng = new();
            Logs newLog = Logs.Instantiate<Logs>();
            newLog.Position = new Vector2(110, rng.RandiRange(-35, 35));
            AddChild(newLog);
            newLog.Owner = this;
            obstacles.Add(newLog);
        }
    }
    public void IncreaseScore()
    {
        var label = GetNode<RichTextLabel>("Score");
        Score++;
        label.Text = $"Score: {Score}";
    }
}
