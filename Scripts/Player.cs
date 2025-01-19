using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public const float Speed = 300.0f;
    public const float JumpVelocity = -400.0f;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity += GetGravity() * (float)delta;
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("Jump")) //&& IsOnFloor())
        {
            velocity.Y = JumpVelocity;
        }

        velocity.X = Speed;

        Velocity = velocity;
        MoveAndSlide();
    }
}
