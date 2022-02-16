using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI placeText;
    [SerializeField]
    private TextMeshProUGUI playerNameText;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void SetContent(int place, string playerName, int score)
    {
        placeText.text = place.ToString("D2");
        playerNameText.text = playerName;
        scoreText.text = score.ToString();
    }
}
