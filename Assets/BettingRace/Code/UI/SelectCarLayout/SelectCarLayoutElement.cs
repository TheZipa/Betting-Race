using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BettingRace.Code.UI.SelectCarLayout
{
    public class SelectCarLayoutElement : MonoBehaviour, ILayoutElement
    {
        public int Id { get; private set; }

        public event Action<SelectCarLayoutElement> OnSelect;

        [SerializeField] private Image _elementImage;
        [SerializeField] private Image _carImage;
        [SerializeField] private TextMeshProUGUI _carNameText;
        [SerializeField] private Button _selectButton;

        private void Awake() =>
            _selectButton.onClick.AddListener(SendSelectAction);

        private void OnDestroy() =>
            _selectButton.onClick.RemoveListener(SendSelectAction);

        public void Construct(Sprite car, string name, int id)
        {
            _carImage.sprite = car;
            _carNameText.text = name;
            Id = id;
        }

        public void SetColor(Color color) =>
            _elementImage.color = color;

        public Color GetColor() => _elementImage.color;

        public Sprite GetCarSprite() => _carImage.sprite;

        private void SendSelectAction() =>
            OnSelect?.Invoke(this);
    }
}
