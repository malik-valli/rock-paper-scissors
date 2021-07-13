using UnityEngine;

public class AnimationManager : Singleton<AnimationManager>, IManager
{
    private Animator playerScoreTextAnimator, enemyScoreTextAnimator, timeIndicatorAnimator;

    private void Awake()
    {
        playerScoreTextAnimator = UIManager.Instance.PlayerScoreText.GetComponent<Animator>();
        enemyScoreTextAnimator = UIManager.Instance.EnemyScoreText.GetComponent<Animator>();

        timeIndicatorAnimator = UIManager.Instance.TimeIndicatorText.GetComponent<Animator>();
    }

    public void PlayTimeIndicatorAnimation() =>
        timeIndicatorAnimator.Play("SkiddingDown");

    public class PlayerAnimator
    {
        public static void PlayScoreAnimation() =>
            AnimationManager.Instance.playerScoreTextAnimator.Play("Snaking");
    }

    public static class EnemyAnimator
    {
        public static void PlayScoreAnimation() =>
            AnimationManager.Instance.enemyScoreTextAnimator.Play("Snaking");
    }

    public void ResetAllAnimations() =>
        timeIndicatorAnimator.Play("Empty");
}
