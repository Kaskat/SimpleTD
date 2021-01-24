using System;
using UnityEngine;
using GameCore;


namespace Enemy
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        #region Events

        public event Action<BaseEnemy> Died;

        #endregion Events

        #region Properties

        public float Succesfull
        {
            get
            {
                return _success;
            }
            set
            {
                _success = value;

                if (value >= 1)
                {
                    Debug.Log("FINISH");
                }
            }
        }

        public int Cost => UnityEngine.Random.Range(_enemyData.minCost, _enemyData.maxCost);

        public bool IsReadyMove;

        protected int Health
        {
            get
            {
                return _currentHealth;
            }

            set
            {
                if (value != _currentHealth)
                {
                    _currentHealth = value;

                    if (value <= 0)
                    {
                        Deactivate();
                    }
                }
            }
        }


        #endregion Properties

        #region Fields

        [SerializeField] protected float _success;
        [SerializeField] protected int _currentHealth;

        protected Gate _gate;
        protected EnemyData _enemyData;

        protected Vector3[] _waypoints;
        protected int nextWaypoint = 1;
        protected float _wayLength;
        protected float _currentDistance;

        private Vector3 _offsetY;
        private bool isFreezeEffect;
        private float freezeDiviser;

        #endregion Fields

        #region Private Methods

        private void GateAttack()
        {
            _gate.Damage(_enemyData.damage);
            Deactivate();
        }

        #endregion Private Methods

        #region Public Methods

        public virtual void Activate(Gate gate, Vector3[] waypoints, float wayLength, EnemyData enemyData, Vector3 offsetY)
        {
            _gate = gate;
            _waypoints = waypoints;
            _enemyData = enemyData;
            _wayLength = wayLength;
            _offsetY = offsetY;

            Health = _enemyData.health;

            transform.position = _waypoints[0] + _offsetY;
            gameObject.SetActive(true);
            IsReadyMove = true;
        }

        public Vector3 GetPositionThroughTime(float shellMoveTime)
        {
            var toMoveDirection = (_waypoints[nextWaypoint] + _offsetY) - transform.position;
            toMoveDirection.z = 0;

            return transform.position + toMoveDirection.normalized * _enemyData.speed * shellMoveTime;
        }

        public virtual void ApplyEffect(EffectType effectType, int value)
        {
            switch (effectType)
            {
                case EffectType.Damage:
                    Health -= value;
                    break;
                case EffectType.Freeze:
                    isFreezeEffect = true;
                    freezeDiviser = value;
                    break;
            }
        }

        public virtual void Deactivate()
        {
            IsReadyMove = false;
            Died?.Invoke(this);
        }

        public void Update()
        {
            if (_waypoints != null && IsReadyMove)
            {
                if (nextWaypoint <= _waypoints.Length && (transform.position - (_waypoints[nextWaypoint] + _offsetY)).magnitude <= 0.1f)
                {
                    nextWaypoint += 1;

                    if (nextWaypoint == _waypoints.Length)
                    {
                        GateAttack();
                        return;
                    }
                }

                var toMoveDirection = (_waypoints[nextWaypoint] + _offsetY) - transform.position;
                toMoveDirection.z = 0;

                var speed = _enemyData.speed;

                if (isFreezeEffect)
                {
                    speed /= freezeDiviser;
                    isFreezeEffect = false;
                }

                var moveVector = toMoveDirection.normalized * Time.deltaTime * speed;
                _currentDistance += moveVector.magnitude;

                transform.Translate(moveVector);

                Succesfull = _currentDistance / _wayLength;
            }
        }

        #endregion Public Methods
    }
}
