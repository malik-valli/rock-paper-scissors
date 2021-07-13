using UnityEngine;

public class TableManager : Singleton<TableManager>, IManager
{
    private HandHandler[] handHandlers; // Player part.

    [SerializeField] private GameObject[] enemyHandPlaces = new GameObject[4]; // Enemy part.
    public GameObject[] EnemyHandPlaces { get { return enemyHandPlaces; } }

    private void Awake() =>
        handHandlers = UIManager.Instance.HandHandlers;

    public void AddEnemyHands(GameObject[] hands)
    {
        for (int i = 0; i < EnemyHandPlaces.Length; i++)
        {
            Instantiate(hands[i], EnemyHandPlaces[i].transform);
        }
    }

    public void ClearTable()
    {
        foreach (HandHandler handler in handHandlers)
            handler?.Clear();

        foreach (GameObject place in EnemyHandPlaces)
            if (place.transform.childCount != 0)
                Destroy(place.transform.GetChild(0).gameObject);
    }

    public bool IsPlayerTableEmpty()
    {
        foreach (HandHandler handler in handHandlers)
            if (handler.CurrentHand != null)
                return false;
        return true;
    }

    public void GetHands(out Hand[] playerHands, out Hand[] enemyHands)
    {
        playerHands = new Hand[4];
        enemyHands = new Hand[4];

        for (int i = 0; i < 4; i++)
        {
            if (handHandlers[i].TablePlace.transform.childCount != 0)
                playerHands[i] = handHandlers[i].TablePlace.transform.GetChild(0).GetComponent<Hand>();
            if (EnemyHandPlaces[i].transform.childCount != 0)
                enemyHands[i] = EnemyHandPlaces[i].transform.GetChild(0).GetComponent<Hand>();
        }
    }
}
