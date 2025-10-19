using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DHFH.Rewards
{
    /// <summary>
    /// Displays sticker collection grid in Rewards scene.
    /// Shows locked/unlocked states with painterly frames.
    /// </summary>
    public class RewardsShelf : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Transform gridContainer;
        [SerializeField] private GameObject stickerSlotPrefab;
        [SerializeField] private TMP_Text progressText;
        [SerializeField] private TMP_Text headerText;
        
        void Start()
        {
            BuildStickerGrid();
            UpdateProgressText();
            UpdateHeaderText();
        }
        
        private void BuildStickerGrid()
        {
            var allStickers = RewardsManager.Instance.GetAllStickers();
            
            foreach (var sticker in allStickers)
            {
                GameObject slotObj = Instantiate(stickerSlotPrefab, gridContainer);
                StickerSlot slot = slotObj.GetComponent<StickerSlot>();
                if (slot) slot.Initialize(sticker);
            }
        }
        
        private void UpdateProgressText()
        {
            int unlocked = RewardsManager.Instance.GetUnlockedCount();
            int total = RewardsManager.Instance.GetTotalCount();
            
            bool isEnglish = Core.LanguageManager.Instance.IsEnglish();
            progressText.text = isEnglish 
                ? $"{unlocked} / {total} Stickers"
                : $"{unlocked} / {total} 贴纸";
        }
        
        private void UpdateHeaderText()
        {
            bool isEnglish = Core.LanguageManager.Instance.IsEnglish();
            headerText.text = isEnglish ? "My Sticker Collection" : "我的贴纸收藏";
        }
    }
}
