using Unity.VisualScripting;
using UnityEngine;


public class ChunkManager : MonoBehaviour
{
    #region Singleton
    public static ChunkManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    [Header("Elements")]
    [SerializeField] private LevelSO[] levels;
    private GameObject finishLine;

    private void Start()
    {
        GenerateLevel();

        finishLine = GameObject.FindGameObjectWithTag("Finish");
    }

    /// <summary>
    /// Bulunduğumuz leveli ve modunu alarak sınırsız bir level döngüsü oluştur bulunduğumuz levelin içindeki level platformlarını oluştur
    /// </summary>
    private void GenerateLevel()
    {
        int currentLevel = GetLevel();

        //Sınırsız level döngüsü oluşturma hilesi
        currentLevel = currentLevel % levels.Length;

        LevelSO level = levels[currentLevel];

        CreateLevel(level.chunks);
    }

    private void CreateLevel(Chunk[] levelChunks)
    {
        Vector3 chunkPosition = Vector3.zero;
        for (int i = 0; i < levelChunks.Length; i++)
        {
            Chunk chunkToCreate = levelChunks[i];

            if (i > 0)
            {
                chunkPosition.z += chunkToCreate.GetLength() / 2;
            }

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);

            chunkPosition.z += chunkInstance.GetLength() / 2;
        }
    }

    /*
    private void CreateRandomLevel()
    {
        Vector3 chunkPosition = Vector3.zero;
        for (int i = 0; i < 5; i++)
        {
            Chunk chunkToCreate = levelChunks[Random.Range(0, levelChunks.Length)];

            if (i > 0)
            {
                chunkPosition.z += chunkToCreate.GetLength() / 2;
            }

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);

            chunkPosition.z += chunkInstance.GetLength() / 2;
        }
    }
    */

    public float GetFinishZ()
    {
        return finishLine.transform.position.z;
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt("Level");
    }

}
