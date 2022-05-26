using System;
using BettingRace.Code.Data.Sound;
using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.Services.Sound;
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

        private SoundService _soundService;

        private void Awake()
        {
            _soundService = AllServices.Container.Single<SoundService>();
            _selectButton.onClick.AddListener(SendSelectAction);
            _selectButton.onClick.AddListener(() => _soundService.PlaySound(SoundType.Click));
        }

        private void OnDestroy() => _selectButton.onClick.RemoveAllListeners();

        public void Construct(Sprite view, string name, int id)
        {
            _horseImage.sprite = view;
            _horseNameText.text = name;
            Id = id;
        }

        public void SetSprite(Sprite sprite) =>
            _elementImage.sprite = sprite;

        public Sprite GetSprite() => _elementImage.sprite;

        public Sprite GetCarSprite() => _horseImage.sprite;

        private void SendSelectAction() =>
            OnSelect?.Invoke(this);
    }
}
