using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace DHFH.Parental
{
    /// <summary>
    /// Math question gate to prevent accidental access by children.
    /// Displays simple addition question (e.g., 7 + 3 = ?)
    /// </summary>
    public class ParentalGate : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject gatePanel;
        [SerializeField] private TMP_Text questionText;
        [SerializeField] private TMP_InputField answerInput;
        [SerializeField] private Button submitButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private TMP_Text errorText;
        
        private int correctAnswer;
        private Action onSuccess;
        
        void Start()
        {
            submitButton.onClick.AddListener(CheckAnswer);
            cancelButton.onClick.AddListener(Cancel);
            gatePanel.SetActive(false);
        }
        
        public void Show(Action successCallback)
        {
            onSuccess = successCallback;
            GenerateQuestion();
            
            gatePanel.SetActive(true);
            answerInput.text = "";
            errorText.text = "";
            answerInput.Select();
        }
        
        private void GenerateQuestion()
        {
            int a = UnityEngine.Random.Range(5, 15);
            int b = UnityEngine.Random.Range(5, 15);
            correctAnswer = a + b;
            
            bool isEnglish = Core.LanguageManager.Instance.IsEnglish();
            questionText.text = isEnglish 
                ? $"What is {a} + {b}?"
                : $"{a} + {b} = ?";
        }
        
        private void CheckAnswer()
        {
            if (int.TryParse(answerInput.text, out int userAnswer))
            {
                if (userAnswer == correctAnswer)
                {
                    onSuccess?.Invoke();
                    gatePanel.SetActive(false);
                    Debug.Log("[ParentalGate] Access granted");
                }
                else
                {
                    ShowError();
                }
            }
            else
            {
                ShowError();
            }
        }
        
        private void ShowError()
        {
            bool isEnglish = Core.LanguageManager.Instance.IsEnglish();
            errorText.text = isEnglish 
                ? "Incorrect answer. Try again!" 
                : "答案错误。再试一次！";
            
            errorText.color = Core.DHFHColors.CoralRed;
            answerInput.text = "";
            
            // Shake animation
            StartCoroutine(ShakeInput());
        }
        
        private System.Collections.IEnumerator ShakeInput()
        {
            Vector3 originalPos = answerInput.transform.localPosition;
            float duration = 0.5f;
            float elapsed = 0f;
            
            while (elapsed < duration)
            {
                float x = Mathf.Sin(elapsed * 30f) * 10f;
                answerInput.transform.localPosition = originalPos + new Vector3(x, 0, 0);
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            answerInput.transform.localPosition = originalPos;
        }
        
        private void Cancel()
        {
            gatePanel.SetActive(false);
            Debug.Log("[ParentalGate] Cancelled");
        }
    }
}
