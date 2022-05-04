using UnityEngine;

namespace BettingRace.Code.UI
{
    public interface ILayoutElement
    {
        int Id { get; }
        void Construct(Sprite view, string name, int id);
    }
}