using BattleField1942.Scripts.Helper.Constants;
using Battlefields1942.Scripts.Helper.Enums;
using Godot;
using System;

namespace BattleField1942.Scripts.Characters;
public partial class Player : CharacterBody3D
{
	[ExportCategory("Required")]
	[Export]
	private StanceHandler _stanceHandler;

	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;


    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed(InputActionNames.CROUCH_TOGGLE))
		{
			if (_stanceHandler.ActiveStance == StanceType.Crouched)
			{
				_stanceHandler.SwitchStance(StanceType.Standing);
			}
			else
			{
				_stanceHandler.SwitchStance(StanceType.Crouched);
			}
		}

		if (@event.IsActionPressed(InputActionNames.PRONE_TOGGLE))
		{
			if (_stanceHandler.ActiveStance == StanceType.Prone)
			{
				_stanceHandler.SwitchStance(StanceType.Standing);
			}
			else
			{
				_stanceHandler.SwitchStance(StanceType.Prone);
			}
		}

		if (@event.IsActionPressed(InputActionNames.JUMP))
		{
			if (_stanceHandler.ActiveStance != StanceType.Standing)
			{
				_stanceHandler.SwitchStance(StanceType.Standing);
			}
		}
    }


	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (_stanceHandler.ActiveStance == StanceType.Standing)
		{
			if (Input.IsActionJustPressed(InputActionNames.JUMP) && IsOnFloor())
			{
				velocity.Y = JumpVelocity;
			}
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		// Input.ActionPress(action: "")
		
		Vector2 inputDir = Input.GetVector(InputActionNames.MOVE_RIGHT, InputActionNames.MOVE_LEFT, InputActionNames.MOVE_BACKWARD, InputActionNames.MOVE_FORWARD);
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	// Member Methods------------------------------------------------------------------------------
}
