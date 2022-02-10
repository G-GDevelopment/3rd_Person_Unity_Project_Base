using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables Concerning States
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMovementState MovementState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }

    [SerializeField]
    private PlayerStats _playerStats;
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public CapsuleCollider2D CapsuleCollider { get; private set; }

    #endregion

    #region Variables
    private Vector2 _playerVelocity;

    [SerializeField] private bool _debugGizmos = false;
    #endregion

    #region Built In Method
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, _playerStats, "Idle");
        MovementState = new PlayerMovementState(this, StateMachine, _playerStats, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, _playerStats, "Jump");
        InAirState = new PlayerInAirState(this, StateMachine, _playerStats, "InAir");
        LandState = new PlayerLandState(this, StateMachine, _playerStats, "Land");

    }

    private void Start()
    {
        InputHandler = GetComponent<PlayerInputHandler>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        CapsuleCollider = GetComponent<CapsuleCollider2D>();
        StateMachine.Initialize(IdleState);

        Core.StartMethod();
    }

    private void Update()
    {
        Core.LogicUpdate();

        StateMachine.CurrentState.StandardUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
    }

    #endregion

    #region Player Methods
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishedTrigger() => StateMachine.CurrentState.AnimationFinishedTrigger();

    #endregion

    #region DrawGizmos
    private void OnDrawGizmos()
    {
        if (_debugGizmos)
        {

        }
    }

    #endregion
}
