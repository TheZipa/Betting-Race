using System.Collections.Generic;
using BettingRace.Code.Game.Horse;
using UnityEngine;

namespace BettingRace.Code.Game.Environment.BackgroundParallax
{
    public class BackgroundParallax
    {
        private readonly ParallaxLayer[] _layers;
        private readonly List<IHorse> _horses;

        private Transform _toFollow;

        public BackgroundParallax(ParallaxLayer[] layers, List<IHorse> horses)
        {
            _layers = layers;
            _horses = horses;
            _toFollow = horses[0].GetTransform();
        }

        public void SetFollowing(int horseId) =>
            _toFollow = _horses[horseId].GetTransform();

        public void Follow()
        {
            foreach (ParallaxLayer layer in _layers)
                layer.SetFollowing(_toFollow, _toFollow.position);
        }
    }
}