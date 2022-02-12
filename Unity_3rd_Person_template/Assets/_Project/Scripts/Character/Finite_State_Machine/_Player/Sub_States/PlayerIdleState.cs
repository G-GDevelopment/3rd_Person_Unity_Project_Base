using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player p_player, PlayerStateMachine p_stateMachine, PlayerStats p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
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

        core.Movement.SetVelocityZero();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void StandardUpdate()
    {
        base.StandardUpdate();

        if (!isExistingState)
        {
            if(inputX != 0 || inputZ != 0) //Use RawMovement Vector instead
            {
                stateMachine.ChangeState(player.MovementState);
            }
        }
    }
}
