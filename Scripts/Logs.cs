using Godot;
using System;

public partial class Logs : Node2D
{
    [Signal]
    public delegate void ObstacleHitEventHandler();

    private AudioStreamPlayer2D hitPlayer;

    public override void _Ready()
    {
        hitPlayer = GetNode<AudioStreamPlayer2D>("HitPlayer");
    }

    private void _on_boundary_body_entered(Node2D body)
    {
        if (body is Player player)
        {
            hitPlayer.Play();
            player.stopPlayer();
            EmitSignal(SignalName.ObstacleHit);
        }
    }
    private void _on_score_zone_body_entered(Node2D body)
    {

        if (Owner is Game game)
        {
            game.IncreaseScore();
        }
    }
}
