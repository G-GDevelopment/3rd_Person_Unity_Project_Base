using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Stats/Player Date/Base Data")]
public class PlayerStats : ScriptableObject
{
    [Header("Movement Stats")]
    public float MovementVelocity = 10f;


    [Header("Jump Stats")]
    public float JumpForce = 15f;
    public float FallMultiplier = 2.5f;
    public float LowJumpMultiplier = 2f;

    [Header("In Air Stats")]
    public float InAirControl = 0.5f;
}
