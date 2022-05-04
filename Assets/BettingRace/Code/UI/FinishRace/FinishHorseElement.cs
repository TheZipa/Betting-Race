using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BettingRace.Code.UI.FinishRace
{
    public class FinishHorseElement : MonoBehaviour, ILayoutElement
    {
        public int Id { get; private set; }
        
        [SerializeField] private Image _elementImage;
        [SerializeField] private Image _horseImage;
        [SerializeField] private TextMeshProUGUI _horseName;
        [SerializeField] private TextMeshProUGUI _finishIndex;

        public void Construct(Sprite view, string name, int id)
        {
            _horseImage.sprite = view;
            _horseName.text = name;
            Id = id;
        }

        public void SetFinishIndex(int finishIndex) =>
            _finishIndex.text = "#" + finishIndex;

        public void SetColor(Color color) =>
            _elementImage.color = color;

        public Color GetColor() =>
            _elementImage.color;
    }
}