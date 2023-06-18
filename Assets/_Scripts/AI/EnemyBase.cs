using System;
using _Scripts.Gameplay;
using _Scripts.Gameplay.FSM;
using _Scripts.Player;
using _Scripts.Services;
using _Scripts.Services.EventBusService.EventsInterfaces;
using _Scripts.Services.PauseHandlerService;
using _Scripts.SO;
using Animancer;
using RootMotion.Dynamics;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using EventBus = _Scripts.Services.EventBusService.EventBus;

namespace _Scripts.AI
{
    public abstract class EnemyBase : GameBehaviour, IEnemy, IGameLoseSubscriber
    {
        [TabGroup("Params")][SerializeField] protected UnitConfig _config;
        [TabGroup("Components")][SerializeField] protected AnimancerComponent _animancer;

        protected FSM _fsm;
        protected HealthComponent _health;
        protected ITarget _target;
        protected bool _isActive;
        
        private EnemiesHasher _enemiesHasher;

        [Inject]
        private void Construct(EnemiesHasher enemiesHasher, ITarget target, PauseHandler pauseHandler)
        {
            _enemiesHasher = enemiesHasher;
            _target = target;
            pauseHandler.Register(this);
        }

        protected abstract void Init();

        protected virtual void Start()
        {
            _health = GetComponent<HealthComponent>();
            _health.Initialize(_config.Hp);
            _health.OnDeadAction += () =>
            {
                _enemiesHasher.Unregister(this);
                EventBus.RaiseEvent<IEnemyDiedSubscriber>(item => item.OnEnemyDied(transform.parent));
            };
            _enemiesHasher.Register(this);
            EventBus.Subscribe(this);
            
            Init();
        }

        private void OnEnable()
        {
            _enemiesHasher.Register(this);
            EventBus.Subscribe(this);
        }

        private void OnDisable()
        {
            _enemiesHasher.Unregister(this);
            EventBus.Unsubscribe(this);
        }

        protected virtual void Update()
        {
            if (IsPaused || _health.IsDead)
                return;
            
            _fsm?.UpdateMachine();
        }

        public override void SetPause(bool isPaused)
        {
            base.SetPause(isPaused);
            
            if (_animancer != null)
            {
                if (isPaused)
                    _animancer.Playable.PauseGraph();
                else
                    _animancer.Playable.UnpauseGraph();
            }
        }

        public void OnGameLost()
        {
            _isActive = true;
        }
    }
}