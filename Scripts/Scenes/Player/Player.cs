using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 100.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	[Export]
	private static float Gravity { get; set; } = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private AnimatedSprite2D sprite { get; set; }
    private float velocityX { get; set; }
	private float velocityY { get; set; }

    public override void _Process(double delta)
	{
		Movement(delta);
		
	}

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{

	}

	public void Movement(double delta)
	{
		var input = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		velocityX = Velocity.X;
		velocityY = Velocity.Y;

		GD.Print(input);
		if (input != 0)
		{
			if (input > 0)
			{
				velocityX = Mathf.Lerp(velocityX, Speed, 0.1f); // Increase the interpolation factor for faster acceleration
				velocityX = Mathf.Clamp(velocityX, -Speed, Speed);
				sprite.FlipH = false;
				sprite.Animation = "run";
			}
			else if (input < 0)
			{
				velocityX = Mathf.Lerp(velocityX, -Speed, 0.1f); // Increase the interpolation factor for faster acceleration
				velocityX = Mathf.Clamp(velocityX, -Speed, Speed);
				sprite.FlipH = true;
				sprite.Animation = "run";
			}
		}
		else
		{
			velocityX = 0;
			sprite.Animation = "idle";
		}
		GravityForce(delta);

		Velocity = new Vector2(velocityX, velocityY);
		MoveAndSlide();
	}

	public void GravityForce(double delta) 
	{
		velocityY += Gravity * (float)delta;
	}
}
