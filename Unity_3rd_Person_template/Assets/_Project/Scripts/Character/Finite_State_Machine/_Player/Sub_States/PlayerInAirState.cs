using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public PlayerInAirState(Player p_player, PlayerStateMachine p_stateMachine, PlayerStats p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }
}
