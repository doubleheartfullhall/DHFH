using UnityEngine;
using System;
using System.Collections.Generic;

namespace DHFH.Rewards
{
    /// <summary>
    /// Manages sticker collection and unlocking.
    /// Persists across sessions via SaveManager.
    /// </summary>
    public class RewardsManager : MonoBehaviour
    {
        public static RewardsManager Instance { get; private set; }
        
        [Serializable]
        public class StickerDefinition
        {
            public string id;
            public string nameEN;
            public string name中文;
            public Sprite icon;
            public string descriptionEN;
            public string description中文;
        }
        
        [Header("Sticker Database")]
        [SerializeField] private List<StickerDefinition> allStickers = new List<StickerDefinition>();
        
        private HashSet<string> unlockedStickers = new HashSet<string>();
        private HashSet<string> newStickers = new HashSet<string>();
        
        public event Action<StickerDefinition> OnStickerUnlocked;
        
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            LoadUnlockedStickers();
        }
        
        public void UnlockSticker(string stickerId)
        {
            if (unlockedStickers.Contains(stickerId))
            {
                Debug.Log($"[Rewards] Sticker already unlocked: {stickerId}");
                return;
            }
            
            unlockedStickers.Add(stickerId);
            newStickers.Add(stickerId);
            SaveUnlockedStickers();
            
            var sticker = allStickers.Find(s => s.id == stickerId);
            if (sticker != null)
            {
                Debug.Log($"[Rewards] Unlocked: {sticker.nameEN}");
                OnStickerUnlocked?.Invoke(sticker);
                ShowCelebration(sticker);
            }
        }
        
        public bool IsUnlocked(string stickerId) => unlockedStickers.Contains(stickerId);
        public bool IsNew(string stickerId) => newStickers.Contains(stickerId);
        public void MarkAsSeen(string stickerId) => newStickers.Remove(stickerId);
        
        public List<StickerDefinition> GetAllStickers() => allStickers;
        public int GetUnlockedCount() => unlockedStickers.Count;
        public int GetTotalCount() => allStickers.Count;
        
        private void ShowCelebration(StickerDefinition sticker)
        {
            // TODO: Show celebration popup with confetti
            Debug.Log($"🎉 New sticker: {sticker.nameEN}!");
        }
        
        private void LoadUnlockedStickers()
        {
            var progress = Core.SaveManager.LoadProgress();
            unlockedStickers = new HashSet<string>(progress.unlockedStickers);
            Debug.Log($"[Rewards] Loaded {unlockedStickers.Count} stickers");
        }
        
        private void SaveUnlockedStickers()
        {
            var progress = Core.SaveManager.LoadProgress();
            progress.unlockedStickers = new List<string>(unlockedStickers);
            Core.SaveManager.SaveProgress(progress);
        }
    }
}
