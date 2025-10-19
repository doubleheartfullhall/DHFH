using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DHFH.Dialogue
{
    /// <summary>
    /// Displays character sprites during dialogue.
    /// Supports multiple expressions (happy, thinking, excited, etc.)
    /// </summary>
    public class CharacterDisplay : MonoBehaviour
    {
        [Header("Character Slots")]
        [SerializeField] private Image leftCharacterSlot;
        [SerializeField] private Image rightCharacterSlot;
        [SerializeField] private CanvasGroup leftCanvasGroup;
        [SerializeField] private CanvasGroup rightCanvasGroup;
        
        [Header("Character Sprites")]
        [SerializeField] private CharacterSpriteSet miaSprites;
        [SerializeField] private CharacterSpriteSet baoBaoSprites;
        
        private string currentLeftCharacter = "";
        private string currentRightCharacter = "";
        
        [System.Serializable]
        public class CharacterSpriteSet
        {
            public string characterName;
            public Sprite neutral;
            public Sprite happy;
            public Sprite thinking;
            public Sprite excited;
            public Sprite silly;
            public Sprite brushing;
            public Sprite cooking;
        }
        
        public void ShowCharacter(string character, string expression)
        {
            CharacterSpriteSet spriteSet = GetSpriteSet(character);
            if (spriteSet == null)
            {
                Debug.LogWarning($"[CharacterDisplay] Unknown character: {character}");
                return;
            }
            
            Sprite sprite = GetSprite(spriteSet, expression);
            
            // Determine slot (alternate between left/right)
            bool useLeft = currentRightCharacter == character || currentLeftCharacter != character;
            
            if (useLeft)
            {
                leftCharacterSlot.sprite = sprite;
                StartCoroutine(FadeIn(leftCanvasGroup));
                currentLeftCharacter = character;
            }
            else
            {
                rightCharacterSlot.sprite = sprite;
                StartCoroutine(FadeIn(rightCanvasGroup));
                currentRightCharacter = character;
            }
        }
        
        private CharacterSpriteSet GetSpriteSet(string character)
        {
            return character.ToLower() switch
            {
                "mia" => miaSprites,
                "baobao" => baoBaoSprites,
                _ => null
            };
        }
        
        private Sprite GetSprite(CharacterSpriteSet set, string expression)
        {
            return expression.ToLower() switch
            {
                "happy" => set.happy,
                "thinking" => set.thinking,
                "excited" => set.excited,
                "silly" => set.silly,
                "brushing" => set.brushing,
                "cooking" => set.cooking,
                _ => set.neutral
            };
        }
        
        private IEnumerator FadeIn(CanvasGroup group)
        {
            group.alpha = 0f;
            float duration = 0.3f;
            float elapsed = 0f;
            
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                group.alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
                yield return null;
            }
            
            group.alpha = 1f;
        }
        
        public void ClearCharacters()
        {
            leftCanvasGroup.alpha = 0f;
            rightCanvasGroup.alpha = 0f;
            currentLeftCharacter = "";
            currentRightCharacter = "";
        }
    }
}
