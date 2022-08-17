using System.Collections;
using UnityEngine;

public enum EnemyState
{
    Calm,
    ReadyToShoot,
    ChangePosition,
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private LineSight _lineSight;
    [SerializeField] private TankMovement _tankMovement;
    [SerializeField] private GunShooter _gunShooter;
    [SerializeField] private float _moveTime;
    [SerializeField] private float _reactionTime;
    [SerializeField] private float _forgetTargetTime;
    [SerializeField] private int _directionCount;
    [SerializeField] private Vector2 _forwardMoveRange;
    [SerializeField] private Vector2 _sideMoveRange;
    [SerializeField] private float _distanceToObstacle;

    private Transform _target;
    private EnemyState _currentState;
    private Vector2 _moveDirection;
    private bool _targetIsVisible;

    private void Start()
    {
        _lineSight.DetectPlayer += OnDetectTargetTransform;
        _lineSight.LostPlayer += OnLostTargetTransform;
    }

    private void FixedUpdate()
    {
        switch (_currentState)
        {
            case EnemyState.Calm:
                CalmState();
                break;
            case EnemyState.ReadyToShoot:
                ShootState();
                break;
            case EnemyState.ChangePosition:
                ChangePositionState();
                break;
        }
    }

    private void OnDetectTargetTransform(object sender, Transform target)
    {
        _targetIsVisible = true;
        _target = target;
    }

    private void OnLostTargetTransform(object sender, Transform target)
    {
        _targetIsVisible = false;
        StopCoroutine(ForgetTarget());
        StartCoroutine(ForgetTarget());
    }

    private IEnumerator ForgetTarget()
    {
        yield return new WaitForSeconds(_forgetTargetTime);
        _currentState = EnemyState.Calm;
        _tankMovement.StopMovement();;
    }

    private void CalmState()
    {
        if (_target != null && _targetIsVisible)
        {
            _currentState = EnemyState.ChangePosition;
            StartCoroutine(MakeReadyToShoot());
        }
    }

    private void ShootState()
    {
        if (_gunShooter.isReolading())
        {
            _currentState = EnemyState.ChangePosition;
            StartCoroutine(MakeReadyToShoot());
            return;
        }

        if (_targetIsVisible) 
        {
            _gunShooter.TryShoot();
        }
       
        _currentState = EnemyState.ChangePosition;
        StartCoroutine(SetDirections());
        StartCoroutine(BackToShoot());
    }

    private void ChangePositionState()
    {
        if (HasObstacles())
        {
            _moveDirection.x *= -1;
        }

       Vector3 relativePosition = _target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(relativePosition, Vector3.zero);
        _tankMovement.Move(_moveDirection.x, _moveDirection.y, targetRotation);
    }

    private void UpdateMoveDirection()
    {
        _moveDirection.x = Random.Range(_forwardMoveRange.x, _forwardMoveRange.y);
        _moveDirection.y = Random.Range(_sideMoveRange.x, _sideMoveRange.y);
    }

    private IEnumerator BackToShoot()
    {
        yield return new WaitForSeconds(_moveTime);
        _currentState = EnemyState.ReadyToShoot;
    }

    private IEnumerator SetDirections()
    {
        for(int i = 0; i < _directionCount; i++)
        {
            UpdateMoveDirection();
            yield return new WaitForSeconds(_moveTime / _directionCount);
        }
    }

    private IEnumerator MakeReadyToShoot()
    {
        yield return new WaitForSeconds(_reactionTime);
        _currentState = EnemyState.ReadyToShoot;
    }

    private bool HasObstacles()
    {
        Vector3 rayDirection = transform.forward;
        rayDirection *= Mathf.Sign(_moveDirection.x);
        return Physics.Raycast(transform.position, rayDirection, _distanceToObstacle);
    }

    private void OnDestroy()
    {
        _lineSight.DetectPlayer -= OnDetectTargetTransform;
        _lineSight.LostPlayer -= OnLostTargetTransform;
    }
}
