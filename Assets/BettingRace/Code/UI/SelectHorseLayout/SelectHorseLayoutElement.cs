using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BettingRace.Code.UI.SelectHorseLayout
{
    public class SelectHorseLayoutElement : MonoBehaviour, ILayoutElement
    {
        public int Id { get; private set; }

        public event Action<SelectHorseLayoutElement> OnSelect;

        [SerializeField] private Image _elementImage;
        [SerializeField] private Image _horseImage;
        [SerializeField] private TextMeshProUGUI _horseNameText;
        [SerializeField] private Button _selectButton;

        private void Awake() =>
            _selectButton.onClick.AddListener(SendSelectAction);

        private void OnDestroy() =>
            _selectButton.onClick.RemoveListener(SendSelectAction);

        public void Construct(Sprite view, string name, int id)
        {
            _horseImage.sprite = view;
            _horseNameText.text = name;
            Id = id;
        }

        public void SetColor(Color color) =>
            _elementImage.color = color;

        public Color GetColor() => _elementImage.color;

        public Sprite GetCarSprite() => _horseImage.sprite;

        private void SendSelectAction() =>
            OnSelect?.Invoke(this);
    }
}
