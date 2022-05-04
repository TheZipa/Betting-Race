using System.Collections;
using UnityEngine;

namespace BettingRace.Code.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}