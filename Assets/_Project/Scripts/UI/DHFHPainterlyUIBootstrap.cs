using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DHFH.UI
{
    /// <summary>
    /// Applies painterly storybook aesthetic to all UI elements.
    /// Runs on scene load for consistent styling.
    /// </summary>
    [ExecuteInEditMode]
    public class DHFHPainterlyUIBootstrap : MonoBehaviour
    {
        [Header("Textures")]
        [SerializeField] private Sprite paperBackground;
        [SerializeField] private Sprite cardFrame;
        [SerializeField] private Sprite buttonRounded;
        [SerializeField] private Sprite brushDotAccent;
        
        [Header("Effects")]
        [SerializeField] private Texture2D canvasGrainTexture;
        [SerializeField] private float grainOpacity = 0.12f;
        [SerializeField] private bool applyVignette = true;
        
        void Start()
        {
            ApplyGlobalStyles();
        }
        
        private void ApplyGlobalStyles()
        {
            ApplyPaperBackground();
            ApplyCanvasGrain();
            if (applyVignette) ApplyVignette();
            
            StyleAllButtons();
            StyleAllPanels();
            StyleAllText();
        }
        
        private void ApplyPaperBackground()
        {
            Canvas canvas = GetComponent<Canvas>();
            if (!canvas) return;
            
            GameObject bgObj = new GameObject("Background");
            bgObj.transform.SetParent(transform, false);
            bgObj.transform.SetAsFirstSibling();
            
            RectTransform rect = bgObj.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
            
            Image bgImage = bgObj.AddComponent<Image>();
            bgImage.sprite = paperBackground;
            bgImage.color = Core.DHFHColors.WarmCream;
            bgImage.raycastTarget = false;
        }
        
        private void ApplyCanvasGrain()
        {
            if (!canvasGrainTexture) return;
            
            GameObject grainObj = new GameObject("CanvasGrain");
            grainObj.transform.SetParent(transform, false);
            grainObj.transform.SetAsLastSibling();
            
            RectTransform rect = grainObj.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
            
            RawImage grain = grainObj.AddComponent<RawImage>();
            grain.texture = canvasGrainTexture;
            grain.color = new Color(1, 1, 1, grainOpacity);
            grain.raycastTarget = false;
        }
        
        private void ApplyVignette()
        {
            GameObject vignetteObj = new GameObject("Vignette");
            vignetteObj.transform.SetParent(transform, false);
            vignetteObj.transform.SetAsLastSibling();
            
            RectTransform rect = vignetteObj.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
            
            Image vignette = vignetteObj.AddComponent<Image>();
            vignette.color = new Color(0.15f, 0.13f, 0.12f, 0.1f);
            vignette.raycastTarget = false;
            
            // Radial gradient effect (requires custom material/shader)
            // For now, simple darkening at edges
        }
        
        private void StyleAllButtons()
        {
            Button[] buttons = GetComponentsInChildren<Button>(true);
            foreach (Button button in buttons)
            {
                StyleButton(button);
            }
        }
        
        private void StyleButton(Button button)
        {
            Image img = button.GetComponent<Image>();
            if (img && buttonRounded)
            {
                img.sprite = buttonRounded;
                img.type = Image.Type.Sliced;
            }
            
            ColorBlock colors = button.colors;
            colors.normalColor = Core.DHFHColors.CoralRed;
            colors.highlightedColor = new Color(0.92f, 0.52f, 0.42f);
            colors.pressedColor = new Color(0.72f, 0.38f, 0.32f);
            colors.selectedColor = Core.DHFHColors.SoftGold;
            colors.disabledColor = new Color(0.7f, 0.7f, 0.7f, 0.5f);
            colors.fadeDuration = 0.2f;
            button.colors = colors;
            
            // Soft shadow
            Shadow shadow = button.GetComponent<Shadow>();
            if (!shadow) shadow = button.gameObject.AddComponent<Shadow>();
            shadow.effectColor = Core.DHFHColors.SoftShadow;
            shadow.effectDistance = new Vector2(0, -4);
            
            // Style text
            TMP_Text text = button.GetComponentInChildren<TMP_Text>();
            if (text)
            {
                text.color = Core.DHFHColors.WarmCream;
                text.fontSize = 44;
                text.fontStyle = FontStyles.Bold;
                text.alignment = TextAlignmentOptions.Center;
            }
        }
        
        private void StyleAllPanels()
        {
            Image[] images = GetComponentsInChildren<Image>(true);
            foreach (Image img in images)
            {
                if (img.gameObject.name.Contains("Panel") || 
                    img.gameObject.name.Contains("Card"))
                {
                    StylePanel(img);
                }
            }
        }
        
        private void StylePanel(Image panel)
        {
            if (cardFrame)
            {
                panel.sprite = cardFrame;
                panel.type = Image.Type.Sliced;
            }
            
            panel.color = new Color(1f, 0.98f, 0.95f);
            
            Shadow shadow = panel.GetComponent<Shadow>();
            if (!shadow) shadow = panel.gameObject.AddComponent<Shadow>();
            shadow.effectColor = new Color(0.15f, 0.13f, 0.12f, 0.15f);
            shadow.effectDistance = new Vector2(0, -6);
        }
        
        private void StyleAllText()
        {
            TMP_Text[] allText = GetComponentsInChildren<TMP_Text>(true);
            foreach (TMP_Text text in allText)
            {
                if (text.GetComponentInParent<Button>()) continue;
                StyleText(text);
            }
        }
        
        private void StyleText(TMP_Text text)
        {
            text.color = Core.DHFHColors.DeepCharcoal;
            text.enableWordWrapping = true;
            
            Outline outline = text.GetComponent<Outline>();
            if (!outline) outline = text.gameObject.AddComponent<Outline>();
            outline.effectColor = new Color(1f, 1f, 1f, 0.3f);
            outline.effectDistance = new Vector2(1, -1);
        }
    }
}
