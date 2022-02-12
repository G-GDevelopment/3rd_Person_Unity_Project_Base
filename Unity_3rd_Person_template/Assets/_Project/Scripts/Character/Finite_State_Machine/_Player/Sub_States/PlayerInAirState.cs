using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private float _inputX;
    private float _inputZ;

    private bool _jumpInput;
    private bool _jumpInputStop;

    private bool _isGrounded;

    private bool _coyoteTime;
    private bool _isJumping;
    public PlayerInAirState(Player p_player, PlayerStateMachine p_stateMachine, PlayerStats p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void ChecksForSwitchingState()
    {
        base.ChecksForSwitchingState();

        _isGrounded = core.CollisionSystem.IsGrounded;
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        JumpMultiplier();
        core.Movement.SmoothFalling(playerStats.FallMultiplier, playerStats.LowJumpMultiplier, _jumpInput);
    }

    public override void StandardUpdate()
    {
        base.StandardUpdate();

        HandleCoyoteTime();

        Debug.Log(core.Movement.CurrentVelocity.y);
        _jumpInput = player.InputHandler.JumpInput;
        _jumpInputStop = player.InputHandler.JumpInputStop;

        _inputX = player.InputHandler.RawMovementInput.x;
        _inputZ = player.InputHandler.RawMovementInput.y;

        if (_isGrounded && core.Movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }else if(_jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
    }


    #region methods
    private void HandleCoyoteTime()
    {
        if(_coyoteTime && Time.time > startTime + playerStats.CoyoteTime)
        {
            _coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    private void JumpMultiplier()
    {
        if (_isJumping)
        {
            if (_jumpInputStop)
            {
                core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerStats.VariableJumpHeightMultiplier);
                _isJumping = false;
            }else if(core.Movement.CurrentVelocity.y <= 0f)
            {
                _isJumping = false;
            }
        }
    }

    public void StartCoyoteTime() => _coyoteTime = true;

    public void SetIsJumpingToTrue() => _isJumping = true;
    #endregion
}
