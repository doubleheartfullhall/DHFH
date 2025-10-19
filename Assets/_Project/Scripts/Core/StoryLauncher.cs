using UnityEngine;
using UnityEngine.SceneManagement;

namespace DHFH.Core
{
    /// <summary>
    /// Launches story scenes with proper context.
    /// Stores current story ID and Yarn node for the StoryScene to use.
    /// </summary>
    public static class StoryLauncher
    {
        private static string currentStoryId;
        private static string currentYarnNode;
        
        public static string CurrentStoryId => currentStoryId;
        public static string CurrentYarnNode => currentYarnNode;
        
        public static void Launch(string yarnNode, string storyId)
        {
            currentYarnNode = yarnNode;
            currentStoryId = storyId;
            
            Debug.Log($"[StoryLauncher] Launching: {storyId} at node: {yarnNode}");
            
            // Update playtime
            var progress = SaveManager.LoadProgress();
            var storyProgress = progress.GetStoryProgress(storyId);
            storyProgress.playCount++;
            SaveManager.SaveProgress(progress);
            
            SceneManager.LoadScene("StoryScene");
        }
        
        public static void ReturnToHome()
        {
            currentStoryId = null;
            currentYarnNode = null;
            SceneManager.LoadScene("HomeScene");
        }
    }
}
