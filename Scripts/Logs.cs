using Godot;
using System;

public partial class Logs : Node2D
{
    [Signal]
    public delegate void ObstacleHitEventHandler();

    private void _on_boundary_body_entered(Node2D body)
    {
        if (body is Player player)
        {
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
