using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int _amountOfJumpsLeft;
    public PlayerJumpState(Player p_player, PlayerStateMachine p_stateMachine, PlayerStats p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
        _amountOfJumpsLeft = playerStats.AmountOfJumps;
    }

    public override void EnterState()
    {
        base.EnterState();

        player.InputHandler.SetJumpInputToFalse();
        core.Movement.SetVelocityY(playerStats.JumpForce);

        _amountOfJumpsLeft--;
        player.InAirState.SetIsJumpingToTrue();

        isAbilityDone = true;
    }

    public bool CanJump()
    {
        if (_amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = playerStats.AmountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;
}
