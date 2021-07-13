using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private HandType type;
    public HandType Type
    {
        get
        {
            return type;
        }
    }
    public HandType Victim
    {   // Which hand does it hit.
        get
        {
            switch (type)
            {
                case HandType.Rock: return HandType.Scissors;
                case HandType.Paper: return HandType.Rock;
                case HandType.Scissors: return HandType.Paper;

                default: return HandType.Null;
            }
        }
    }

    public enum HandType
    {
        Null,
        Rock,
        Scissors,
        Paper
    }
}
