using UnityEngine;

public class CollisionSystem : CoreComponents
{
    [Header("Components & variables")]
    [SerializeField] private float _offsetGroundY = 0.4f; //Distance between capsule collider and ground
    [SerializeField] private float _raycastWidth = 0.25f;
    [SerializeField] private int _groundAverageValue;
    [SerializeField] private CapsuleCollider _collider;

    private Vector3 _raycastGroundPos;
    private Vector3 _combindedRaycast;
    private Vector3 _gravity;
    private Vector3 _groundMovement;

    private float _groundRayLength;

    public Vector3 Gravity { get => _gravity; set => _gravity = value; }

    #region Built-in Methods
    protected override void Awake()
    {
        base.Awake();

        _collider = GetComponentInParent<CapsuleCollider>();
    }
    #endregion


    #region Checkers
    public bool IsGrounded
    {
        get => GroundRaycasts(0, 0, _groundRayLength).transform != null;
    }

    #endregion

    #region Public Methods
    public void OnHandlingPhysicsCollision()
    {
        _groundRayLength = (_collider.height * 0.5f) + _offsetGroundY;

        if (!IsGrounded)
        {
            //Activate Gravity
            _gravity += (Vector3.up * Physics.gravity.y * Time.fixedDeltaTime);
        }

        _groundMovement = new Vector3(core.Movement.Rigidbody.position.x, FindGround().y, core.Movement.Rigidbody.position.z);

        if (IsGrounded && _groundMovement != core.Movement.Rigidbody.position)
        {
            core.Movement.Rigidbody.MovePosition(_groundMovement);
            _gravity.y = 0;
        }
    }

    #endregion
    #region Private Methods
    private Vector3 FindGround()
    {
        _combindedRaycast = GroundRaycasts(0, 0, _groundRayLength).point;
        _groundAverageValue += (GetGroundAverage(_raycastWidth, 0) + GetGroundAverage(-_raycastWidth, 0) + GetGroundAverage(0, _raycastWidth) + GetGroundAverage(0, _raycastWidth));

        return _combindedRaycast / _groundAverageValue;
    }

    private int GetGroundAverage(float p_offsetX, float p_offsetZ)
    {
        if(GroundRaycasts(p_offsetX, p_offsetZ, _groundRayLength).transform != null)
        {
            _combindedRaycast += GroundRaycasts(p_offsetX, p_offsetZ, _groundRayLength).point;
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private RaycastHit GroundRaycasts(float p_offsetX, float p_offsetZ, float p_raycastLength)
    {
        RaycastHit hit;
        _raycastGroundPos = transform.TransformPoint(0 + p_offsetX, _collider.center.y, 0 + p_offsetZ);

        Debug.DrawRay(_raycastGroundPos, Vector3.down * _groundRayLength, Color.magenta);

        Physics.Raycast(_raycastGroundPos, -Vector3.up, out hit, p_raycastLength);
        return hit;
    }
    #endregion
}
