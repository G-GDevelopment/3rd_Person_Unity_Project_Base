using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Stats/Player Stats/Base Data")]
public class PlayerStats : ScriptableObject
{
    [Header("Movement Stats")]
    public float MovementSpeed = 10f;
    public float RotationSpeed = 0.3f;
    public float AllowRotation = 0.1f;


    [Header("Jump Stats")]
    public float JumpForce = 15f;
    public float CoyoteTime = 0.2f;
    public int AmountOfJumps = 1;

    [Header("In Air Stats")]
    public float InAirControl = 0.5f;
    public float VariableJumpHeightMultiplier = 0.5f;
    public float FallMultiplier = 2.5f;
    public float LowJumpMultiplier = 2f;
}
