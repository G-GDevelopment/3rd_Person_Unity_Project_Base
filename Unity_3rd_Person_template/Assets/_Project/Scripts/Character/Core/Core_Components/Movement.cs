using UnityEngine;

public class Movement : CoreComponents
{
    [Header("Movement Components")]
    public Rigidbody Rigidbody;
    public Camera Camera;
    public Vector3 CurrentVelocity { get; private set; }

    private Vector3 _playerVelocity;
    private Vector3 _desiredFacingDirection;
    private float _speed; //This is not speed for player velocity

    protected override void Awake()
    {
        base.Awake();

        Rigidbody = GetComponentInParent<Rigidbody>();
        Camera = Camera.main;
    }

    public void LogicUpdate() //FixedUpdate instead
    {
        CurrentVelocity = Rigidbody.velocity;
    }

    #region Set Public Methods
    public void SetVelocityZero()
    {
        Rigidbody.velocity = Vector3.zero;
        CurrentVelocity = Vector3.zero;
    }

    public void SetVelocity(float p_inputX, float p_inputZ, float p_rotateSpeed, float p_allowRotation, float p_speed)
    {

        HandleRotationControl(p_inputX, p_inputZ, p_rotateSpeed, p_allowRotation);

        var forward = p_inputX * Rigidbody.transform.forward;
        var sideway = p_inputZ * Rigidbody.transform.right;

        Vector3 movementDirection = new Vector3(p_inputX, 0, p_inputZ);

        float inputStrength = Mathf.Clamp01(Mathf.Abs(p_inputX) + Mathf.Abs(p_inputZ));

        _playerVelocity = (_desiredFacingDirection * p_speed * inputStrength) + core.CollisionSystem.Gravity;
        Rigidbody.velocity = _playerVelocity;
        CurrentVelocity = _playerVelocity;
    }

    public void SetVelocityY(float p_jumpSpeed)
    {
        _playerVelocity.Set(CurrentVelocity.x, p_jumpSpeed, CurrentVelocity.z);
        Rigidbody.velocity = _playerVelocity;
        CurrentVelocity = _playerVelocity;
    }

    #endregion

    #region Set Private Methods
    private void HandleRotation(float p_inputX, float p_inputZ, float p_rotateSpeed)
    {
        var forward = GetCameraForward(Camera);
        var sideway = GetCameraSideway(Camera);

        forward.y = 0;
        sideway.y = 0;

        _desiredFacingDirection = forward * p_inputZ + sideway * p_inputX;

        Rigidbody.transform.rotation = Quaternion.Slerp(Rigidbody.transform.rotation, Quaternion.LookRotation(_desiredFacingDirection), p_rotateSpeed);
    }

    private Vector3 GetCameraForward(Camera p_camera)
    {
        Vector3 returnValue = p_camera.transform.forward;
        returnValue.y = 0;

        return returnValue.normalized;
    }

    private Vector3 GetCameraSideway(Camera p_camera)
    {
        Vector3 returnValue = p_camera.transform.right;
        returnValue.y = 0;

        return returnValue.normalized;
    }
    private void HandleRotationControl(float p_inputX, float p_inputZ, float p_rotateSpeed, float p_allowRotation)
    {
        _speed = new Vector2(p_inputX, p_inputZ).sqrMagnitude;

        if(_speed > p_allowRotation)
        {
            HandleRotation(p_inputX, p_inputZ, p_rotateSpeed);
        }
        else
        {
            SetVelocityZero();
        }
    }

    #endregion

    #region Other Methods
    public void SmoothFalling(float p_fallMultiplier, float p_lowFallMultiplier, bool p_jumpInput)
    {
        if(Rigidbody.velocity.y <= 0)
        {
            _playerVelocity += Vector3.up * Physics.gravity.y * (p_fallMultiplier - 1) * Time.deltaTime;
            Rigidbody.velocity = _playerVelocity;
            CurrentVelocity = _playerVelocity;
        }else if(Rigidbody.velocity.y > 0 && !p_jumpInput)
        {
            _playerVelocity += Vector3.up * Physics.gravity.y * (p_lowFallMultiplier - 1) * Time.deltaTime;
            Rigidbody.velocity = _playerVelocity;
            CurrentVelocity = _playerVelocity;

            Debug.Log(core.CollisionSystem.Gravity);
        }
    }
    #endregion
}
