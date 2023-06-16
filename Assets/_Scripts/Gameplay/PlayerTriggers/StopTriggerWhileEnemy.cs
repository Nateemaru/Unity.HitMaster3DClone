using _Scripts.Gameplay.Camera;
using _Scripts.Player;
using _Scripts.Services.EventBusService;
using _Scripts.Services.EventBusService.EventsInterfaces;
using UnityEngine;

namespace _Scripts.Gameplay.PlayerTriggers
{
    public class StopTriggerWhileEnemy : MonoBehaviour
    {
        [SerializeField] private TargetGroupContainer _targetGroupContainer;

        private bool _isAlreadyTriggered;

        private void Start()
        {
            _targetGroupContainer.OnContainerIsEmpty += MovePlayer;
        }

        private void MovePlayer()
        {
            _isAlreadyTriggered = true;
            EventBus.RaiseEvent<IPlayerMoveSubscriber>(item => item.OnPlayerMove());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isAlreadyTriggered)
            {
                if(other.TryGetComponent(out MovementBase movementBase))
                    EventBus.RaiseEvent<IPlayerStopSubscriber>(item => item.OnPlayerStopped());
            }
        }
    }
}