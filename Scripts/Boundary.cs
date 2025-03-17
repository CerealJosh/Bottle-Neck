using Godot;
using System;

public partial class Boundary : StaticBody2D
{
    private Sprite2D _sprite1;
    private Sprite2D _sprite2;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _sprite1 = GetNode<Sprite2D>("First");
        _sprite2 = GetNode<Sprite2D>("Second");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void InterchangePosition(bool sprite)
    {
        if (sprite == true)
        {
            _sprite1.Position = new Vector2(_sprite2.Position.X + 1361, _sprite1.Position.Y);
        }
        else if (sprite == false)
        {
            _sprite2.Position = new Vector2(_sprite1.Position.X + 1361, _sprite2.Position.Y);
        }
    }
}
