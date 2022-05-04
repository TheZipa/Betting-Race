using BettingRace.Code.Infrastructure.StateMachine.States;
using UnityEngine;

namespace BettingRace.Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        //[SerializeField] private LoadingCurtain loadingCurtain;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this);
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}