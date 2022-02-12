using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected float inputX;
    protected float inputZ;

    private bool _isGrounded;
    private bool _jumpInput;

    public PlayerGroundedState(Player p_player, PlayerStateMachine p_stateMachine, PlayerStats p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }

    public override void ChecksForSwitchingState()
    {
        base.ChecksForSwitchingState();

        _isGrounded = core.CollisionSystem.IsGrounded;
    }

    public override void EnterState()
    {
        base.EnterState();

        player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        core.CollisionSystem.OnHandlingPhysicsCollision();
    }

    public override void StandardUpdate()
    {
        base.StandardUpdate();

        inputX = player.InputHandler.RawMovementInput.x;
        inputZ = player.InputHandler.RawMovementInput.y;

        _jumpInput = player.InputHandler.JumpInput;

        if(_jumpInput && player.JumpState.CanJump()) //Plus if there is no celling
        {
            stateMachine.ChangeState(player.JumpState);
        }else if (!_isGrounded)
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }
}
