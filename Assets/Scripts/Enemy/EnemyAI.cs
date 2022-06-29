using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private int _directionCount;
    [SerializeField] private Vector2 _forwardMoveRange;
    [SerializeField] private Vector2 _sideMoveRange;
    [SerializeField] private float _distanceToObstacle;

    private Transform _target;
    private EnemyState _currentState;
    private Vector2 _moveDirection;

    private void Start()
    {
        _lineSight.DetectPlayer += DetectTargetTransform;
    }

    private void FixedUpdate()
    {
        switch(_currentState)
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

    private void DetectTargetTransform(object sender, Transform target)
    {
        _target = target;
    }

    private void CalmState()
    {
        if(_target != null)
        {
            _currentState = EnemyState.ChangePosition;
            StartCoroutine(MakeReadyToShoot());
        }
    }

    private void ShootState()
    {
        if(_gunShooter.isReolading())
        {
            return;
        }

        _gunShooter.TryShoot();
        _currentState = EnemyState.ChangePosition;
        StartCoroutine(SetDirections());
        StartCoroutine(BackToCalm());
    }

    private void ChangePositionState()
    {
        if(HasObstacles())
        {
            _moveDirection *= -1;
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

    private IEnumerator BackToCalm()
    {
        yield return new WaitForSeconds(_moveTime);
        _currentState = EnemyState.Calm;
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
        _lineSight.DetectPlayer -= DetectTargetTransform;
    }
}
