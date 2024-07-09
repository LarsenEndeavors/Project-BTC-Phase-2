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
		var input = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		var velocity = new Vector2(Velocity.X, Velocity.Y);

		if (input != 0)
		{
			velocity = new Vector2(input * Speed, velocity.Y);
			velocity = new Vector2((float)Mathf.Clamp(Speed * delta, -Speed, Speed), velocity.Y);
			sprite.FlipH = input < 0;
		}
		else
		{
			Velocity = new Vector2(0, Velocity.Y);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
