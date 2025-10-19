using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

namespace DHFH.UI
{
    /// <summary>
    /// Painterly story card matching reference images aesthetic.
    /// Soft hover effects, warm colors, hand-painted feel.
    /// </summary>
    public class StoryCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("UI References")]
        [SerializeField] private Image thumbnail;
        [SerializeField] private TMP_Text titleEN;
        [SerializeField] private TMP_Text title中文;
        [SerializeField] private Button playButton;
        [SerializeField] private Image brushDotAccent;
        [SerializeField] private GameObject lockOverlay;
        [SerializeField] private CanvasGroup canvasGroup;
        
        private StoryCardData data;
        private Vector3 originalScale;
        
        void Awake()
        {
            originalScale = transform.localScale;
            if (!canvasGroup) canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        
        public void Initialize(StoryCardData cardData)
        {
            data = cardData;
            UpdateDisplay();
            
            playButton.onClick.AddListener(OnPlayClicked);
            Core.LanguageManager.Instance.OnLanguageChanged += OnLanguageChanged;
            
            AnimateIn();
        }
        
        private void UpdateDisplay()
        {
            thumbnail.sprite = data.thumbnail;
            titleEN.text = data.titleEN;
            title中文.text = data.title中文;
            
            OnLanguageChanged(Core.LanguageManager.Instance.CurrentLanguage);
            
            lockOverlay.SetActive(data.isLocked);
            playButton.interactable = !data.isLocked;
            
            // Completion brush dot
            if (brushDotAccent)
            {
                brushDotAccent.color = data.isCompleted 
                    ? Core.DHFHColors.SoftGold 
                    : new Color(1, 1, 1, 0.3f);
            }
        }
        
        private void OnLanguageChanged(Core.LanguageManager.Language lang)
        {
            bool isEnglish = lang == Core.LanguageManager.Language.English;
            titleEN.gameObject.SetActive(isEnglish);
            title中文.gameObject.SetActive(!isEnglish);
            
            TMP_Text buttonText = playButton.GetComponentInChildren<TMP_Text>();
            if (buttonText) buttonText.text = isEnglish ? "Play" : "玩";
        }
        
        private void OnPlayClicked()
        {
            if (data.isLocked)
            {
                ShowUnlockPrompt();
                return;
            }
            
            Core.StoryLauncher.Launch(data.yarnNode, data.id);
        }
        
        private void ShowUnlockPrompt()
        {
            string message = data.unlockMethod switch
            {
                UnlockType.QR => "Scan QR code to unlock!\n扫描二维码解锁！",
                UnlockType.Purchase => "Available for purchase\n可供购买",
                UnlockType.Reward => "Complete stories to unlock!\n完成故事解锁！",
                _ => "Locked\n已锁定"
            };
            Debug.Log($"[StoryCard] {message}");
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (data.isLocked) return;
            transform.localScale = originalScale * 1.02f;
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = originalScale;
        }
        
        private void AnimateIn()
        {
            canvasGroup.alpha = 0f;
            transform.localScale = originalScale * 0.9f;
            
            // Simple fade in (replace with DOTween for smoother animations)
            StartCoroutine(FadeInRoutine());
        }
        
        private System.Collections.IEnumerator FadeInRoutine()
        {
            float elapsed = 0f;
            float duration = 0.4f;
            
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                canvasGroup.alpha = t;
                transform.localScale = Vector3.Lerp(originalScale * 0.9f, originalScale, t);
                yield return null;
            }
            
            canvasGroup.alpha = 1f;
            transform.localScale = originalScale;
        }
        
        void OnDestroy()
        {
            playButton.onClick.RemoveListener(OnPlayClicked);
            if (Core.LanguageManager.Instance)
                Core.LanguageManager.Instance.OnLanguageChanged -= OnLanguageChanged;
        }
    }
    
    [Serializable]
    public class StoryCardData
    {
        public string id;
        public string titleEN;
        public string title中文;
        public string yarnNode;
        public Sprite thumbnail;
        public bool isLocked;
        public bool isCompleted;
        public UnlockType unlockMethod;
    }
    
    public enum UnlockType { Free, QR, Purchase, Reward }
}
