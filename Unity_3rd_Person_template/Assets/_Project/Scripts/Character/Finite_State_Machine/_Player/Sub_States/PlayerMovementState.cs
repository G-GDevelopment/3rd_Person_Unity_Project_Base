using UnityEngine;

public class PlayerMovementState : PlayerGroundedState
{
    public PlayerMovementState(Player p_player, PlayerStateMachine p_stateMachine, PlayerStats p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
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

        core.Movement.SetVelocity(inputX, inputZ, playerStats.RotationSpeed, playerStats.AllowRotation, playerStats.MovementSpeed);
    }

    public override void StandardUpdate()
    {
        base.StandardUpdate();

        if (!isExistingState)
        {
            if(inputX == 0 && inputZ == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }

        }

    }
}
