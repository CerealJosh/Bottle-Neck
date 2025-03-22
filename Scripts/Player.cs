using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    public float JumpVelocity;
    private bool playing = true;

    private AnimatedSprite2D fish;

    public override void _Ready()
    {
        fish = GetNode<AnimatedSprite2D>("FishSprite");
    }

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
                if (fish.Animation != "Rise")
                    fish.Play("Rise");
                velocity.Y = JumpVelocity;
            }
            else if (velocity.Y > 0)
            {
                if (fish.Animation != "Fall")
                    fish.Play("Fall");
            }
            else if (IsOnFloor() || IsOnCeiling())
            {
                if (fish.Animation != "Idle")
                    fish.Play("Idle");
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
