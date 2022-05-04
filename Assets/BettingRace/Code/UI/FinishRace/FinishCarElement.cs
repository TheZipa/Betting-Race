using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BettingRace.Code.UI.FinishRace
{
    public class FinishCarElement : MonoBehaviour, ILayoutElement
    {
        public int Id { get; private set; }
        
        [SerializeField] private Image _elementImage;
        [SerializeField] private Image _carImage;
        [SerializeField] private TextMeshProUGUI _carName;
        [SerializeField] private TextMeshProUGUI _finishIndex;

        public void Construct(Sprite car, string name, int id)
        {
            _carImage.sprite = car;
            _carName.text = name;
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