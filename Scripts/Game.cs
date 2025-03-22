using Godot;
using System.Collections.Generic;

public partial class Game : Node2D
{
    [Export]
    public PackedScene Logs { get; set; }
    [Export]
    public float Speed { get; set; }
    [Export]
    public float LogSpeed { get; set; }

    private List<Logs> obstacles = new();

    private Boundary bottom;
    private Boundary top;

    private float boundaryPosition = 697f;
    private int Score = 0;

    private bool playing = false;
    private bool _swapOne = true;
    private bool started = false;

    private Timer logTimer;


    public override void _Ready()
    {
        top = GetNode<Boundary>("Boundaries/Top/");
        bottom = GetNode<Boundary>("Boundaries/Bottom/");
        obstacles.Clear();
        logTimer = GetNode<Timer>("LogTimer");
        logTimer.Timeout += createLogs;

        ProcessMode = ProcessModeEnum.Always;
        GetTree().Paused = true;
    }

    public override void _Process(double delta)
    {
        if (Input.IsKeyPressed(Key.Escape))
        {
            GetTree().ReloadCurrentScene();
        }
        if (!started && !playing)
        {
            if (Input.IsActionJustPressed("Jump"))
            {
                GetTree().Paused = false;
                playing = true;
                started = true;
                logTimer.Start();
            }
        }
        if (playing)
        {
            boundaryPosition -= (Speed * (float)delta);
            if (boundaryPosition >= 50)
            {
                bottom.Position = new Vector2(bottom.Position.X - Speed * (float)delta, bottom.Position.Y);
                top.Position = new Vector2(top.Position.X - Speed * (float)delta, top.Position.Y);
            }
            else
            {
                top.InterchangePosition(_swapOne);
                bottom.InterchangePosition(_swapOne);
                _swapOne = !_swapOne;
                boundaryPosition = 697f;
            }
            for (int i = 0; i < obstacles.Count; i++)
            {
                obstacles[i].Position = new Vector2(obstacles[i].Position.X - LogSpeed * (float)delta, obstacles[i].Position.Y);
                if (obstacles[i].Position.X < -250)
                {
                    obstacles[i].ObstacleHit -= stopMovement;
                    obstacles[i].QueueFree();
                    obstacles.Remove(obstacles[i]);
                }
            }
        }
    }

    private void createLogs()
    {
        if (obstacles.Count < 5)
        {
            RandomNumberGenerator rng = new();
            Logs newLog = Logs.Instantiate<Logs>();
            newLog.Position = new Vector2(110, rng.RandiRange(-25, 25));
            AddChild(newLog);
            newLog.Owner = this;
            obstacles.Add(newLog);
            newLog.ObstacleHit += stopMovement;
        }
    }
    public void IncreaseScore()
    {
        var label = GetNode<RichTextLabel>("Score");
        Score++;
        label.Text = $"Score: {Score}";
    }

    public void stopMovement()
    {
        playing = false;
    }
}
