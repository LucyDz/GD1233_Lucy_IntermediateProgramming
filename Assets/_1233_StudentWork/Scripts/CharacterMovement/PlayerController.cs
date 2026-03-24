using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent (typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;
    public Vector3 screenPos;
    private Vector3 mousePos;

    [SerializeField] private GameObject _playerModel;

    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    [SerializeField] private float speed;

    [SerializeField] private Movement movement;
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private ProjectileWeapon _weapon;

    
    #region Camera


    public void CamSwap()
    {
        CameraManager.Instance.SwitchCamera();
    }
    #endregion
    public void Shoot(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        _animator?.SetTrigger("Shoot");
    }
    //public void ShootHold(InputAction.CallbackContext context)
    //{
    //   if (!context.) return;

    //    _animator?.SetTrigger("Shoot");
    //}
   
    public void Bomb(InputAction.CallbackContext context)
    {
        Debug.Log("Grenade");
        if (!context.started) return;
        if (!_characterController.isGrounded) return;
        //if(_isAttacking) return;

        _animator?.SetTrigger("Throw");
    }
    

    #region Animation
    [SerializeField] private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Grndd = Animator.StringToHash("IsGrounded");
    private static readonly int JumpReq = Animator.StringToHash("JumpReq 0");
    private bool _isJumping;
    #endregion
    #region Gravity
    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;
    #endregion
    #region Jumps
    [SerializeField] private float jumpPower;
    private int _numberOfJumps;
    [SerializeField] private int maxNumberOfJumps = 2;
    #endregion
    private void Awake()
    {
           _characterController = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        if (_health != null)
        {
            _health.OnDamaged += HandleDamaged;
            _health.OnDied += HandleDied;
        }
    }
    private void OnDisable()
    {
        if (_health != null)
        {
            _health.OnDamaged -= HandleDamaged;
            _health.OnDied -= HandleDied;
        }
    }

    private void Update()
    {
        ApplyGravity();
        ApplyRotation();
        ApplyMovement();
        AnimationParameters();
    }
    private void ApplyGravity()
    {
        if (IsGrounded() && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }
        _direction.y = _velocity;
    }
    private void ApplyRotation()
    {
        //if (_input.sqrMagnitude == 0) return;

        /*
        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        */
        screenPos = Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(
                new Vector3(screenPos.x, screenPos.y, 36.2f));
        mousePos = new Vector3(mousePos.x, _weapon.Muzzle.position.y, mousePos.z);

        _playerModel.transform.LookAt(mousePos);
    }
    private void ApplyMovement()
    {
        var targetSpeed = movement.isSprinting ? movement.speed * movement.multiplier : movement.speed;
        movement.currentSpeed = Mathf.MoveTowards(movement.currentSpeed, targetSpeed, movement.acceleration * Time.deltaTime);

        _characterController.Move(_direction * movement.currentSpeed * Time.deltaTime);
    }
    #region InputLogic
    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y); 
    }
    public void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jumped");
        if (!context.started) return;
        if (!IsGrounded() && _numberOfJumps >= maxNumberOfJumps) return;
        if (_numberOfJumps == 0 ) StartCoroutine(WaitForLanding());
        _isJumping = true;
        _numberOfJumps++;
        _velocity = jumpPower;

    }
    public void Sprint(InputAction.CallbackContext context)
    {
        movement.isSprinting = context.started || context.performed;
    }
    private void AnimationParameters()
    {
        Vector3 horizontalMvmnt = new Vector3(_characterController.velocity.x, 0, _characterController.velocity.z);
        _animator?.SetFloat(Speed, horizontalMvmnt.sqrMagnitude);

        _animator?.SetBool(Grndd, IsGrounded());
        if (_isJumping) _animator?.SetTrigger(JumpReq);
        _isJumping = false;
    }
    #endregion
    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(IsGrounded);
        _animator?.SetBool(Grndd, true);
        _numberOfJumps = 0;
    }

    private bool IsGrounded() => _characterController.isGrounded;

    private void HandleDied()
    {
        Debug.Log("[Player] Died! Game Over...");
        _animator?.SetTrigger("Die");
        _characterController = null;

        StartCoroutine(GameOver());
        
    }
    private void HandleDamaged(DamageInfo info)
    {
        Debug.Log(
            $"[Player] Hit by " +
            $"{info.Source?.name ?? "Unknown"} " +
            $"for {info.Amount} damage. " +
            $"HP: {_health.CurrentHealth}/{_health.MaxHealth}");
        if (_health.CurrentHealth > 0)
            _animator?.SetTrigger("Hit");
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        GameMgr.Instance.GameOver();
    }
}

[Serializable]
public struct Movement
{
    public float speed;
    public float multiplier;
    public float acceleration;
    [HideInInspector]public bool isSprinting;
    [HideInInspector] public float currentSpeed;
}
