using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SkinButton[] skinButtons;

    [Header("Skins")]
    [SerializeField] private Sprite[] skins;

    private void Start()
    {
        ConfigureButtons();

        SelectSkin(0);
    }

    private void ConfigureButtons()
    {
        for(int i = 0; i < skinButtons.Length; i++)
        {
            skinButtons[i].Configure(skins[i], true);

            int skinIndex = i;

            skinButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
        }
    }

    private void SelectSkin(int skinIndex)
    {
        for(int i = 0; i < skinButtons.Length; i++)
        {
            if(skinIndex == i)
            {
                skinButtons[i].Select();
            }
            else
            {
                skinButtons[i].DeSelect();
            }
        }
    }
}
