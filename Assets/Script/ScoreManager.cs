using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Unity.Netcode;
using System.Collections;

public class ScoreManager : NetworkBehaviour
{
    public NetworkVariable<int> score = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    [SerializeField] private TextMeshProUGUI scoreText;
    private int scoreValue;

    public UnityEvent<int> onScoreChanged;
    void Awake()
    {
        scoreValue = 0;
        UpdateScoreUI();
    }

    // Method to add score
    public void AddScore(int amount)
    {
        scoreValue += amount;
        onScoreChanged.Invoke(scoreValue);
        UpdateScoreUI();
    }
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + scoreValue;
        }
    }
}
