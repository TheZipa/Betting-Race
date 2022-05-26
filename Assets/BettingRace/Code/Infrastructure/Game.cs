using BettingRace.Code.Infrastructure.StateMachine;
using BettingRace.Code.Services.Sound;

namespace BettingRace.Code.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;
        
        public Game(ICoroutineRunner coroutineRunner, SoundService soundService)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), soundService);
        }
    }
}