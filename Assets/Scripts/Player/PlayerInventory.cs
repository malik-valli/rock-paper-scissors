using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>
{
    [SerializeField, Range(0, 4)] private int initialRockCount;
    public int RockCount { get; private set; }

    [SerializeField, Range(0, 4)] private int initialPaperCount;
    public int PaperCount { get; private set; }

    [SerializeField, Range(0, 4)] private int initialScissorsCount;
    public int ScissorsCount { get; private set; }

    private void Awake()
    {
        RockCount = initialRockCount;
        PaperCount = initialPaperCount;
        ScissorsCount = initialScissorsCount;
    }

    public bool HandRemained(Hand.HandType handType)
    {
        switch (handType)
        {
            case Hand.HandType.Rock:
                if (RockCount > 0)
                {
                    return true;
                }
                else return false;
            case Hand.HandType.Paper:
                if (PaperCount > 0)
                {
                    return true;
                }
                else return false;
            case Hand.HandType.Scissors:
                if (ScissorsCount > 0)
                {
                    return true;
                }
                else return false;
            default: return false;
        }
    }

    public GameObject GetHand(Hand.HandType handType)
    {
        switch (handType)
        {
            case Hand.HandType.Rock:
                if (RockCount > 0)
                {
                    --RockCount;
                    return PrefabManager.Instance.RockPrefab;
                }
                else return null;
            case Hand.HandType.Paper:
                if (PaperCount > 0)
                {
                    --PaperCount;
                    return PrefabManager.Instance.PaperPrefab;
                }
                else return null;
            case Hand.HandType.Scissors:
                if (ScissorsCount > 0)
                {
                    --ScissorsCount;
                    return PrefabManager.Instance.ScissorsPrefab;
                }
                else return null;
            default: return null;
        }
    }

    public void ResetHands()
    {
        RockCount = initialRockCount;
        PaperCount = initialPaperCount;
        ScissorsCount = initialScissorsCount;
    }
}
