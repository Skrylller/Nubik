using UnityEngine;
using UnityEngine.UI;

namespace YG.Example
{
    public class RewardedItem : MonoBehaviour
    {
        [SerializeField] int AdID;
        [SerializeField] ItemModel item;
        [SerializeField] uint _count;

        int moneyCount = 0;

        void Awake()
        {
            AdMoney(0);
        }

        private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
        private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

        void Rewarded(int id)
        {
            if (id == AdID)
                AdMoney(5);
        }

        void AdMoney(int count)
        {
            PlayerInventory.Inventory.AddItem(item, _count);
        }
    }
}
