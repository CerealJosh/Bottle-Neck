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
    private Control endScreen;

    private Boundary bottom;
    private Boundary top;

    private float boundaryPosition = 697f;
    private int Score = 0;

    private bool playing = false;
    private bool _swapOne = true;
    private bool started = false;

    private Player player;
    private Vector2 _startPosition;

    private Timer logTimer;
    private Timer endTimer;

    public override void _Ready()
    {
        player = GetNode<Player>("Player");
        _startPosition = player.Position;
        endScreen = GetNode<Control>("OverScreen");
        top = GetNode<Boundary>("Boundaries/Top/");
        bottom = GetNode<Boundary>("Boundaries/Bottom/");
        obstacles.Clear();
        logTimer = GetNode<Timer>("LogTimer");
        endTimer = GetNode<Timer>("EndTimer");

        logTimer.Timeout += createLogs;
        endTimer.Timeout += displayEndScreen;

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
        endTimer.Start();
    }

    public void displayEndScreen()
    {
        if (!endScreen.Visible)
        {
            endScreen.GetNode<RichTextLabel>("Container/Score").Text += Score.ToString();
            endScreen.Visible = true;
        }
    }

    public void _on_retry_pressed()
    {
        ResetGame();
    }
    public void _on_menu_pressed()
    {
        GetTree().ChangeSceneToFile("../Scenes/Start.tscn");
    }

    public void ResetGame()
    {
        //Reset Score
        Score = 0;
        GetNode<RichTextLabel>("Score").Text = "Score: 0";
        endScreen.GetNode<RichTextLabel>("Container/Score").Text = "Score: ";

        //Hide EndScreen
        endScreen.Visible = false;

        //Remove Logs
        obstacles.ForEach(obstacle => obstacle.QueueFree());
        obstacles.Clear();

        //Reset bool checks
        playing = false;
        started = false;

        //Reset Player
        player.startPlayer();
        player.Position = _startPosition;
        GetTree().Paused = true;
    }
}
