using UnityEngine;
using UnityEngine.UI;

public class RoundManager : Singleton<RoundManager>, IManager
{
    private Text timeIndicatorText;

    [SerializeField, Min(1f)] private float initialExitTime = 21f;
    private float exitTime;

    public bool RoundFinishes { get; private set; }

    private void Awake() =>
        timeIndicatorText = UIManager.Instance.TimeIndicatorText;

    private void Start() =>
        ResetRound();

    private void Update() =>
        TimeCountdown();

    private void TimeCountdown()
    {
        exitTime -= Time.deltaTime;
        timeIndicatorText.text = ((int)exitTime).ToString();

        if (RoundFinishes == false)
        {
            if (exitTime <= initialExitTime / 3f)
                AnimationManager.Instance.PlayTimeIndicatorAnimation();

            if (exitTime <= 0)
                FinishRound();
        }
        if (RoundFinishes == true && exitTime <= 0)
            JudgeRound();
    }

    public void ResetRound()
    {
        RoundFinishes = false;
        exitTime = initialExitTime;
        AnimationManager.Instance.ResetAllAnimations();
    }

    public void FinishRound()
    {
        if (!RoundFinishes)
        {
            RoundFinishes = true;
            exitTime = 6f;
            EnemyAI.Instance.ArrangeHands();
        }
    }

    public void JudgeRound()
    {
        if (TableManager.Instance.IsPlayerTableEmpty())
        {
            GameManager.Instance.AddScoreToEnemy();
            return;
        }

        switch (DoPlayerHandsHit())
        {
            case RoundState.Win:
                GameManager.Instance.AddScoreToPlayer();
                return;
            case RoundState.Lose:
                GameManager.Instance.AddScoreToEnemy();
                return;
            default:
                GameManager.Instance.EndRoundAsDraw();
                UIManager.Instance.PrintMessage("Ничья!");
                return;
        }
    }

    private RoundState DoPlayerHandsHit()
    {
        Hand[] playerHands;
        Hand[] enemyHands;
        TableManager.Instance.GetHands(out playerHands, out enemyHands);

        int winRows = 0;
        int loseRows = 0;

        for (int i = 0; i < 4; i++)
        {
            if (playerHands[i] == null)
                ++loseRows;
            else if (playerHands[i].Victim == enemyHands[i].Type)
                ++winRows;
            else if (enemyHands[i].Victim == playerHands[i].Type)
                ++loseRows;
        }

        if (winRows > loseRows)
            return RoundState.Win;
        if (winRows < loseRows)
            return RoundState.Lose;
        else
            return RoundState.Draw;
    }

    private enum RoundState
    {
        Win,
        Draw,
        Lose
    }
}
