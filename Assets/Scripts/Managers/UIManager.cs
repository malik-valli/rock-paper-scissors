using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>, IManager
{
    [SerializeField] private Canvas canvas;
    public Canvas Canvas { get { return canvas; } }

    [SerializeField] private HandHandler[] handHandlers = new HandHandler[4];
    public HandHandler[] HandHandlers { get { return handHandlers; } }

    [SerializeField] private GameObject messageIndicator;
    public bool MessageIsShowing { get; private set; }

    [SerializeField] private Text timeIndicatorText;
    public Text TimeIndicatorText { get { return timeIndicatorText; } }

    [SerializeField] private Text playerScoreText, enemyScoreText;
    public Text PlayerScoreText { get { return playerScoreText; } }
    public Text EnemyScoreText { get { return enemyScoreText; } }

    public void Update()
    {
        CheckMessage(this);
    }

    public void PrintMessage(string text)
    {
        messageIndicator.SetActive(true);
        messageIndicator.GetComponent<Text>().text = text;
        messageIndicator.GetComponent<Animator>().Play("SkiddingUp");
        MessageIsShowing = true;
    }

    private void CheckMessage(MonoBehaviour monoBehaviour)
    {
        if (MessageIsShowing == true)
            monoBehaviour.StartCoroutine(MessageSetActiveFalseDelay(monoBehaviour));
    }

    private IEnumerator MessageSetActiveFalseDelay(MonoBehaviour monoBehaviour)
    {
        yield return new WaitForSeconds(1);
        messageIndicator.SetActive(false);
        MessageIsShowing = false;

        monoBehaviour.StopCoroutine(MessageSetActiveFalseDelay(monoBehaviour));
    }
}
