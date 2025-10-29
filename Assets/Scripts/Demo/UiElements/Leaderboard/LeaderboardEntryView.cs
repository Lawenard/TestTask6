namespace Demo.UiElements
{
    using Leaderboard;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class LeaderboardEntryView : MonoBehaviour
    {
        [SerializeField] private Image mainContainer;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Image avatar;

        public void Init(LeaderboardEntry item, Color color, float height)
        {
            nameText.text = item.name;
            scoreText.text = item.score.ToString();
            
            mainContainer.color = color;
            mainContainer.rectTransform.sizeDelta = new Vector2(
                mainContainer.rectTransform.sizeDelta.x,
                height);
            
            gameObject.SetActive(true);
        }
        
        public void SetAvatar(Sprite sprite) => avatar.sprite = sprite;
    }
}
