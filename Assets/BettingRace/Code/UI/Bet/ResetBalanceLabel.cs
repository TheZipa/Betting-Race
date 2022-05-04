using System.Collections;
using UnityEngine;

namespace BettingRace.Code.UI.Bet
{
    public class ResetBalanceLabel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        private const float FadeStep = 0.02f;
        private const float ToFadeTime = 3f;

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1f;
            StartCoroutine(FadeHide());
        }

        private IEnumerator FadeHide()
        {
            yield return new WaitForSeconds(ToFadeTime);
            
            while (_canvasGroup.alpha > 0)
            {
                yield return new WaitForSeconds(FadeStep);
                _canvasGroup.alpha -= FadeStep;
            }
            
            gameObject.SetActive(false);
        }
    }
}