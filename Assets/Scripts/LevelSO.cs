using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Scriptable Objects/LevelSO", order = 0)]
public class LevelSO : ScriptableObject
{
    public Chunk[] chunks;
}
