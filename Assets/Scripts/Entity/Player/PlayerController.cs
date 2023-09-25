using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IMousePositionVisitor
{
    private enum SpriteState
    {
        Right,
        Left
    }
    public enum PlayerAnimatorState
    {
        Idle,
        Walk,
        Jump,
        Fall,
        Sit,
        Crawl,
        Climb,
        Capture,
        IdleReload,
        WalkReload,
    }

    [SerializeField] private EnemyModel _model;

    [SerializeField] private MovingController _movingController;
    [SerializeField] private JumpingController _jumpingController;
    [SerializeField] private ShootingController _shootingController;
    [SerializeField] private PlayerAnimatorController _animatorController;
    [SerializeField] private StairsController _stairsController;
    [SerializeField] private PlayerSitController _sitController;
    [SerializeField] private LifeController _lifeController;
    [SerializeField] private PlayerUI _playerUI;

    [SerializeField] private List<ToTargetRotator2D> _rotators;
    [SerializeField] private ModeSwitcher _spriteSwitcher;
    [SerializeField] private Animator _animator;
    [SerializeField] public CompositeCollider2D _collider;

    private Rigidbody2D _rbPlayer;
    private Transform _captureTarget;

    public bool stopInput;
    public bool isBusy;
    private bool _isDayMode;

    private PlayerAnimatorState _state;
    public PlayerAnimatorState State
    {
        get { return _state; }
        set
        {
            _state = value;
            _animatorController.SetAnimation(value);
        }
    }

    private const string layerPlayer = "Player";
    private const string layerPlatform = "Platform";

    public CompositeCollider2D Collider => _collider;

    private void Awake()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();
        _lifeController.Init(_model);
    }
    public void Restart(Vector2 position, bool isDayMode)
    {
        _isDayMode = isDayMode;
        transform.position = position;
    }

    public void CheckMouse(Vector2 mousePos)
    {
        if (isBusy || _isDayMode || stopInput)
            return;

        foreach(ToTargetRotator2D rotator in _rotators)
        {
            SetRotator(rotator, mousePos);
        }

        if (mousePos.x > transform.position.x)
            _spriteSwitcher.State = (int)SpriteState.Right;
        else
            _spriteSwitcher.State = (int)SpriteState.Left;

        void SetRotator(ToTargetRotator2D rotator, Vector2 mousePos)
        {
            float xBias = mousePos.x - rotator.transform.position.x;

            if (_spriteSwitcher.State == (int)SpriteState.Right && xBias < 0)
                mousePos -= new Vector2(xBias * 2, 0);

            if (_spriteSwitcher.State == (int)SpriteState.Left)
            {
                float yBias = mousePos.y - rotator.transform.position.y;

                mousePos -= new Vector2(0, yBias * 2);
                if (xBias < 0)
                    mousePos -= new Vector2(xBias * 2, 0);
            }

            rotator.Rotate(mousePos);
        }
    }

    public void Shoot()
    {
        if (stopInput || isBusy || _isDayMode)
            return;

        if (State == PlayerAnimatorState.Idle || 
            State == PlayerAnimatorState.Walk)
            _shootingController.Shoot();
    }
    public void SetWeapon(int weapon)
    {

        if (stopInput || isBusy || _isDayMode)
            return;

        _shootingController.SetWeapon((WeaponModel.WeaponType)weapon);
    }
}
