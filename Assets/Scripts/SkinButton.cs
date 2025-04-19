using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Button thisButton;
    [SerializeField] private Image skinImage;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private GameObject selector;

    private bool unlocked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Configure(Sprite skinSprite, bool unlocked)
    {
        skinImage.sprite = skinSprite;
        this.unlocked = unlocked;

        if (unlocked)
            Unlock();
        else
            Lock();
    }

    public void Unlock()
    {
        thisButton.interactable = true;
        skinImage.gameObject.SetActive(true);
        lockImage.SetActive(false);

        unlocked = true;
    }

    public void Lock()
    {
        thisButton.interactable = false;
        skinImage.gameObject.SetActive(false);
        lockImage.SetActive(true);

        unlocked = false;
    }

    public void Select()
    {
        selector.SetActive(true);
    }

    public void DeSelect()
    {
        selector.SetActive(false);
    }

    public bool IsUnlocked()
    {
        return unlocked;
    }

    public Button GetButton() 
    { 
        return thisButton; 
    }
}
