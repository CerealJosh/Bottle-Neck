using Godot;
using System;

public partial class Logs : Node2D
{
    public override void _Ready()
    {

    }

    public override void _Process(double delta)
    {
    }

    private void _on_boundary_body_entered(Node2D body)
    {
        //GetTree().ReloadCurrentScene();
    }
    private void _on_score_zone_body_entered(Node2D body)
    {

        if(Owner is Game game)
        {
            game.IncreaseScore();
        }
    }
}
