using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>, IManager
{
    public int PlayerScore { get; private set; } = 0;
    public int EnemyScore { get; private set; } = 0;

    [SerializeField, Min(1)] private int scoreForVictory;

    private Text playerScoreText, enemyScoreText;

    private void Awake()
    {
        playerScoreText = UIManager.Instance.PlayerScoreText;
        enemyScoreText = UIManager.Instance.EnemyScoreText;

        playerScoreText.text = PlayerScore.ToString();
        enemyScoreText.text = EnemyScore.ToString();
    }

    public void AddScoreToPlayer()
    {
        ++PlayerScore;
        playerScoreText.text = PlayerScore.ToString();

        AnimationManager.PlayerAnimator.PlayScoreAnimation();

        EndRound();
    }

    public void AddScoreToEnemy()
    {
        ++EnemyScore;
        enemyScoreText.text = EnemyScore.ToString();

        AnimationManager.EnemyAnimator.PlayScoreAnimation();

        EndRound();
    }

    public void EndRoundAsDraw() =>
        EndRound();

    private void EndRound()
    {
        CheckGameStatus();

        TableManager.Instance.ClearTable();
        RoundManager.Instance.ResetRound();
        PlayerInventory.Instance.ResetHands();
    }

    private void CheckGameStatus()
    {
        if (PlayerScore == scoreForVictory)
            SceneManager.LoadScene("WinScene");
        if (EnemyScore == scoreForVictory)
            SceneManager.LoadScene("LoseScene");
    }
}
