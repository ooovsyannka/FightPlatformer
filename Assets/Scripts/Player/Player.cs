using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(Health), typeof(Ammunition))]

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private PlayerCombat _playerCombat;
    [SerializeField] private PlayerAnimation _playerAnimation;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Sounds _sound;
    [SerializeField] private float _respawnDelay;

    private bool _isDie = false;
    private bool _isMove = false;

    private State _state;
    private Coroutine _respawnDelayCoroutine;
    private PlayerMover _playerMover;
    private Health _playerHealth;
    private Ammunition _playerAmmunition;
    private CapsuleCollider2D _collider;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
        _playerAmmunition = GetComponent<Ammunition>();
        _playerMover = GetComponent<PlayerMover>();
        _collider = GetComponent<CapsuleCollider2D>();
        transform.position = _startPosition.position;
    }

    private void OnEnable()
    {
        _state = State.AnyState;
        _playerHealth.Died += Die;
    }

    private void Update()
    {
        if (_isDie == false)
        {
            if (_inputReader.IsShoot)
            {
                _playerCombat.Attack();
            }

            if (_inputReader.IsReload)
            {
                _playerCombat.Reload();
            }

            _playerCombat.LookAtTarget(_inputReader.MousePosition);
            _isMove = _inputReader.HorizontalInput != 0 || _inputReader.VerticalInput != 0;
        }

        _sound.PlaySound(_state);
        _playerAnimation.PlayAnimation(_isMove, _isDie, _playerMover.IsDash);
    }

    private void FixedUpdate()
    {
        if (_isDie == false)
        {
            _playerMover.Move(_inputReader.HorizontalInput, _inputReader.VerticalInput);

            if (_isMove)
            {
                _state = State.Move;

                if (_inputReader.IsDash)
                {
                    _playerMover.Dash(_inputReader.HorizontalInput, _inputReader.VerticalInput);
                }

                if (_playerMover.IsDash)
                {
                    _state = State.Dash;
                }
            }
            else
            {
                _state = State.AnyState;
            }
        }
    }

    private void Die()
    {
        _isDie = true;
        _state = State.Die;

        _collider.enabled = false;

        if (_respawnDelayCoroutine != null)
            StopCoroutine(_respawnDelayCoroutine);

        _respawnDelayCoroutine = StartCoroutine(RespawnDelay());
    }

    private IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(_respawnDelay);

        _playerHealth.Recovery();
        transform.position = _startPosition.position;
        _playerAmmunition.ReplenishmentBulletsCount();
        _collider.enabled = true;

        _isDie = false;
    }
}