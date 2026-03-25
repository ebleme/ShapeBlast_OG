using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    int score = 0;

    public void IncreaseScore(int value)
    {
        score += value;

        scoreText.text = score.ToString();
    }
}
