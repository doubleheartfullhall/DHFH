using UnityEngine;
using System;

namespace DHFH.Core
{
    /// <summary>
    /// Manages bilingual switching between English and Mandarin.
    /// Persists preference across sessions.
    /// </summary>
    public class LanguageManager : MonoBehaviour
    {
        public static LanguageManager Instance { get; private set; }
        
        public enum Language { English, Mandarin }
        
        [Header("Settings")]
        [SerializeField] private Language startingLanguage = Language.English;
        
        public Language CurrentLanguage { get; private set; }
        
        /// <summary>
        /// Fired when language changes. Subscribe to update UI text.
        /// </summary>
        public event Action<Language> OnLanguageChanged;
        
        void Awake()
        {
            // Singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            LoadLanguagePreference();
        }
        
        private void LoadLanguagePreference()
        {
            string savedLang = PlayerPrefs.GetString("PreferredLanguage", startingLanguage.ToString());
            
            if (Enum.TryParse(savedLang, out Language language))
            {
                CurrentLanguage = language;
            }
            else
            {
                CurrentLanguage = startingLanguage;
            }
            
            Debug.Log($"[LanguageManager] Loaded language: {CurrentLanguage}");
        }
        
        public void SetLanguage(Language language)
        {
            if (CurrentLanguage == language) return;
            
            CurrentLanguage = language;
            PlayerPrefs.SetString("PreferredLanguage", language.ToString());
            PlayerPrefs.Save();
            
            OnLanguageChanged?.Invoke(language);
            Debug.Log($"[LanguageManager] Changed to: {language}");
        }
        
        public void ToggleLanguage()
        {
            Language newLang = CurrentLanguage == Language.English 
                ? Language.Mandarin 
                : Language.English;
            SetLanguage(newLang);
        }
        
        /// <summary>
        /// Returns appropriate text based on current language.
        /// </summary>
        public string GetText(string english, string mandarin)
        {
            return CurrentLanguage == Language.English ? english : mandarin;
        }
        
        public bool IsEnglish() => CurrentLanguage == Language.English;
        public bool IsMandarin() => CurrentLanguage == Language.Mandarin;
    }
}
