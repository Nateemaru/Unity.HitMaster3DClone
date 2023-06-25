using _Scripts.Services.Database;
using _Scripts.Services.SceneLoadService;
using _Scripts.UI;
using Zenject;

namespace _Scripts.Services.StateMachines.GameStateMachine.GameStates
{
    public class GameLoadState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly IFadeScreen _fadeScreen;

        public GameLoadState(IGameStateMachine gameStateMachine, ISceneLoadService sceneLoadService, IFadeScreen fadeScreen)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoadService = sceneLoadService;
            _fadeScreen = fadeScreen;
        }
        
        public void Enter()
        {
            _fadeScreen.FadeIn(() =>
            {
                _sceneLoadService.Load(GlobalConstants.GAME_SCENE, () =>
                {
                    _fadeScreen.FadeOut();
                    _gameStateMachine.ChangeState<GameRunState>();
                });
            });
        }

        public class Factory : PlaceholderFactory<IStateMachine, GameLoadState>
        {
        }
    }
}