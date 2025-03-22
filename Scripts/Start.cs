using Godot;
using System;

public partial class Start : Control
{
    public void _on_start_pressed()
    {
        GetTree().ChangeSceneToFile("../Scenes/Game.tscn");
    }

    public void _on_exit_pressed()
    {
        GetTree().Quit();
    }
}
