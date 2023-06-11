using System;
using _Scripts.CodeSugar;
using _Scripts.Gameplay.FSM;
using _Scripts.Gameplay.States;
using Animancer;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.AI
{
    public class HumanEnemy : EnemyBase
    {
        [TabGroup("Animations")][SerializeField] private AnimancerTransition _idleClip;
        [TabGroup("Animations")][SerializeField] private AnimancerTransition _moveClip;
        [TabGroup("Animations")][SerializeField] private AnimancerTransition _attackClip;
        [TabGroup("Params")][SerializeField] private float _getUpDelay;

        private float _getUpTimer;
        private float _maxPinWeight = 1;
        
        protected override void Init()
        {
            _getUpTimer = _getUpDelay;
            
            var idleState = new IdleState(_animancer, _idleClip);
            var moveState = new EnemyMoveState(transform, _animancer, _moveClip, _target, _config.Speed);
            var ragdollState = new RagdollState(_animancer, _puppetMaster);
            var attackState = new HumanoidAttackState(transform, _animancer, _attackClip, _target);
            
            _fsm = new FSM();
            _fsm.SetState(idleState);

            _health.OnHealthChanged += () =>
            {
                _puppetMaster.pinWeight = 0f;
            };
            
            _fsm.AddAnyTransition(ragdollState, () => _puppetMaster.pinWeight < _maxPinWeight);
            
            _fsm.AddAnyTransition(idleState, () => _target == null 
                                                   && _fsm.CurrentState.IsAnimationEnded);
            
            _fsm.AddAnyTransition(moveState, () => _target != null
                                                   && _fsm.CurrentState.IsAnimationEnded
                                                   && !transform.IsTargetNearby(_target.GetTarget(), _config.AttackDistance)
                                                   && _puppetMaster.pinWeight >= _maxPinWeight);
            
            _fsm.AddAnyTransition(attackState, () => _target != null 
                                                     && transform.IsTargetNearby(_target.GetTarget(), _config.AttackDistance));
        }
    }
}