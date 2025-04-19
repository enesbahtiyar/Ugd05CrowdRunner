using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform runnersParent;
    [SerializeField] private RunnerSelector RunnerSelectorPrefab;


    private void OnEnable()
    {
        ShopManager.onSkinSelected += SelectSkin;
    }

    private void OnDisable()
    {
        ShopManager.onSkinSelected -= SelectSkin;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SelectSkin(Random.Range(0, 8));
    }
    public void SelectSkin(int skinIndex)
    {
        for(int i = 0; i < runnersParent.childCount; i++)
        {
            runnersParent.GetChild(i).GetComponent<RunnerSelector>().SelectRunner(skinIndex);

            RunnerSelectorPrefab.SelectRunner(skinIndex);
        }
    }
}
