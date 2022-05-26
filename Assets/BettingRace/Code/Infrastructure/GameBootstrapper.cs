using BettingRace.Code.Infrastructure.StateMachine.States;
using BettingRace.Code.Services.Sound;
using UnityEngine;

namespace BettingRace.Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private SoundService _soundService;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, _soundService);
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}