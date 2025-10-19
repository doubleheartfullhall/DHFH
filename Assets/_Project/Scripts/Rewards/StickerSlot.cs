using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace DHFH.Rewards
{
    /// <summary>
    /// Individual sticker slot with locked/unlocked states.
    /// Shows silhouette when locked, full color when unlocked.
    /// </summary>
    public class StickerSlot : MonoBehaviour, IPointerClickHandler
    {
        [Header("UI References")]
        [SerializeField] private Image stickerImage;
        [SerializeField] private Image frameImage;
        [SerializeField] private GameObject lockIcon;
        [SerializeField] private GameObject newBadge;
        
        private RewardsManager.StickerDefinition data;
        
        public void Initialize(RewardsManager.StickerDefinition stickerData)
        {
            data = stickerData;
            
            bool isUnlocked = RewardsManager.Instance.IsUnlocked(data.id);
            bool isNew = RewardsManager.Instance.IsNew(data.id);
            
            // Display sticker
            stickerImage.sprite = data.icon;
            stickerImage.color = isUnlocked ? Color.white : new Color(0.3f, 0.3f, 0.3f, 0.5f);
            
            // Frame color
            if (frameImage)
            {
                frameImage.color = isUnlocked 
                    ? Core.DHFHColors.SoftGold 
                    : Core.DHFHColors.DeepCharcoal;
            }
            
            // Lock state
            if (lockIcon) lockIcon.SetActive(!isUnlocked);
            if (newBadge) newBadge.SetActive(isNew);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!RewardsManager.Instance.IsUnlocked(data.id))
            {
                Debug.Log("🔒 Keep playing to unlock this sticker!");
                return;
            }
            
            // Mark as seen
            RewardsManager.Instance.MarkAsSeen(data.id);
            if (newBadge) newBadge.SetActive(false);
            
            // Show detail popup
            ShowStickerDetail();
        }
        
        private void ShowStickerDetail()
        {
            bool isEnglish = Core.LanguageManager.Instance.IsEnglish();
            string name = isEnglish ? data.nameEN : data.name中文;
            string desc = isEnglish ? data.descriptionEN : data.description中文;
            
            Debug.Log($"📌 {name}\n{desc}");
            // TODO: Show proper popup UI
        }
    }
}
