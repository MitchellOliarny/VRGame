using UnityEngine;

public class EnemyArrayIndex : MonoBehaviour
{
    [SerializeField] private int index;

    public int Index
    {
        get { return index; }
        set { index = value; }
    }
}
