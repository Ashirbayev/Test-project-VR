using TMPro;
using UnityEngine;
using VRTraining.Scenario; // <-- ОБЯЗАТЕЛЬНО ПРОВЕРЬТЕ ЭТУ СТРОКУ

namespace VRTraining.UI
{
    public class UIResultRow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textDescription;
        [SerializeField] private TextMeshProUGUI textStatus;

        [Header("Настройки цвета статусов")]
        [SerializeField] private Color successColor = Color.green;
        [SerializeField] private Color failedColor = Color.red;
        [SerializeField] private Color skippedColor = Color.gray;

        public void Initialize(StepResult result) // Теперь ошибка тут исчезнет
        {
            textDescription.text = $"[{result.GroupName}] {result.StepDescription}";

            switch (result.Status)
            {
                case StepStatus.Success:
                    textStatus.text = "Успешно";
                    textStatus.color = successColor;
                    break;
                case StepStatus.Failed:
                    textStatus.text = "Ошибка";
                    textStatus.color = failedColor;
                    break;
                case StepStatus.Skipped:
                    textStatus.text = "Пропущен";
                    textStatus.color = skippedColor;
                    break;
            }
        }
    }
}
