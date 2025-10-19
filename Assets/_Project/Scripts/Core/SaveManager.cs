using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

namespace DHFH.Core
{
    /// <summary>
    /// Handles saving and loading player progress.
    /// Stores completion status, unlocked stickers, and playtime.
    /// </summary>
    public static class SaveManager
    {
        private static string SaveFilePath => 
            Path.Combine(Application.persistentDataPath, "dhfh_player_progress.json");
        
        public static void SaveProgress(PlayerProgress progress)
        {
            try
            {
                string json = JsonUtility.ToJson(progress, true);
                File.WriteAllText(SaveFilePath, json);
                Debug.Log($"[SaveManager] Progress saved: {progress.childName}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveManager] Save failed: {e.Message}");
            }
        }
        
        public static PlayerProgress LoadProgress()
        {
            if (!File.Exists(SaveFilePath))
            {
                Debug.Log("[SaveManager] No save file, creating new progress");
                return new PlayerProgress();
            }
            
            try
            {
                string json = File.ReadAllText(SaveFilePath);
                PlayerProgress progress = JsonUtility.FromJson<PlayerProgress>(json);
                Debug.Log($"[SaveManager] Loaded: {progress.childName}");
                return progress;
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveManager] Load failed: {e.Message}");
                return new PlayerProgress();
            }
        }
        
        public static void DeleteSaveFile()
        {
            if (File.Exists(SaveFilePath))
            {
                File.Delete(SaveFilePath);
                Debug.Log("[SaveManager] Save file deleted");
            }
        }
    }
    
    [Serializable]
    public class PlayerProgress
    {
        public string childName = "";
        public string preferredLanguage = "English";
        public List<StoryProgress> stories = new List<StoryProgress>();
        public List<string> unlockedStickers = new List<string>();
        public int totalPlaytimeSeconds = 0;
        public string lastPlayedDate = DateTime.Now.ToString("yyyy-MM-dd");
        
        [Serializable]
        public class StoryProgress
        {
            public string storyId;
            public bool completed;
            public List<string> visitedNodes = new List<string>();
            public int playCount;
        }
        
        public StoryProgress GetStoryProgress(string storyId)
        {
            var progress = stories.Find(s => s.storyId == storyId);
            if (progress == null)
            {
                progress = new StoryProgress { storyId = storyId };
                stories.Add(progress);
            }
            return progress;
        }
    }
}
