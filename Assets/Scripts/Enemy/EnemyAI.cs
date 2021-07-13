using UnityEngine;

public class EnemyAI : Singleton<EnemyAI>
{
    public GameObject[] Hands = new GameObject[4];

    public void ArrangeHands()
    {
        for (int i = 0; i < Hands.Length; i++)
        {
            Hands[i] = PrefabManager.Instance.HandPrefabs[Random.Range(0, 3)];
        }

        TableManager.Instance.AddEnemyHands(Hands);
    }
}
