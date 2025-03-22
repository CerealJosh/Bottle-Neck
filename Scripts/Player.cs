using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    public float JumpVelocity;
    private bool playing = true;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity += GetGravity() / 2.5f * (float)delta;
        }

        if (playing)
        {
            // Handle Jump.
            if (Input.IsActionJustPressed("Jump")) //&& IsOnFloor())
            {
                velocity.Y = JumpVelocity;
            }

        }
        Velocity = velocity;
        MoveAndSlide();
    }

    public void stopPlayer()
    {
        playing = false;
    }
}
