using UnityEngine;
using Yarn.Unity;

namespace DHFH.Dialogue
{
    /// <summary>
    /// Custom Yarn commands for DHFH app.
    /// Handles character display, SFX, sticker unlocks.
    /// </summary>
    public class DHFHYarnCommands : MonoBehaviour
    {
        [SerializeField] private DialogueRunner dialogueRunner;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private CharacterDisplay characterDisplay;
        
        void Start()
        {
            if (!dialogueRunner)
            {
                Debug.LogError("[YarnCommands] DialogueRunner not assigned!");
                return;
            }
            
            RegisterCommands();
        }
        
        private void RegisterCommands()
        {
            dialogueRunner.AddCommandHandler<string>("character", HandleCharacter);
            dialogueRunner.AddCommandHandler<string>("sfx", PlaySoundEffect);
            dialogueRunner.AddCommandHandler<string>("unlock_sticker", UnlockSticker);
            dialogueRunner.AddCommandHandler("return_home", ReturnHome);
        }
        
        private void HandleCharacter(string characterAndExpression)
        {
            var parts = characterAndExpression.Split(' ');
            string character = parts[0];
            string expression = parts.Length > 1 ? parts[1] : "neutral";
            
            if (characterDisplay)
            {
                characterDisplay.ShowCharacter(character, expression);
            }
            
            Debug.Log($"[Yarn] Character: {character} ({expression})");
        }
        
        private void PlaySoundEffect(string sfxName)
        {
            if (!sfxSource) return;
            
            AudioClip clip = Resources.Load<AudioClip>($"Audio/SFX/{sfxName}");
            if (clip)
            {
                sfxSource.PlayOneShot(clip);
                Debug.Log($"[Yarn] Playing SFX: {sfxName}");
            }
            else
            {
                Debug.LogWarning($"[Yarn] SFX not found: {sfxName}");
            }
        }
        
        private void UnlockSticker(string stickerId)
        {
            Rewards.RewardsManager.Instance.UnlockSticker(stickerId);
            Debug.Log($"[Yarn] Unlocked sticker: {stickerId}");
        }
        
        private void ReturnHome()
        {
            Debug.Log("[Yarn] Returning to Home...");
            Core.StoryLauncher.ReturnToHome();
        }
        
        void OnDestroy()
        {
            if (dialogueRunner)
            {
                dialogueRunner.RemoveCommandHandler("character");
                dialogueRunner.RemoveCommandHandler("sfx");
                dialogueRunner.RemoveCommandHandler("unlock_sticker");
                dialogueRunner.RemoveCommandHandler("return_home");
            }
        }
    }
}
