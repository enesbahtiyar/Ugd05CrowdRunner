using UnityEngine;
using TMPro;
using System;

public class Doors : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SpriteRenderer rightDoorRenderer;
    [SerializeField] private SpriteRenderer leftDoorRenderer;
    [SerializeField] private TextMeshPro rightDoorText;
    [SerializeField] private TextMeshPro leftDoorText;

    [Header("Settings")]
    [SerializeField] private BonusType rightDoorBonusType;
    [SerializeField] private int rightDoorBonusAmount;
    [SerializeField] private BonusType leftDoorBonusType;
    [SerializeField] private int leftDoorBonusAmount;

    [SerializeField] private Color bonusColor;
    [SerializeField] private Color penaltyColor;


    private void Start()
    {
        ConfigureDoors();
    }

    private void ConfigureDoors()
    {
        //RightDoor
        if(rightDoorBonusType == BonusType.Addition)
        {
            rightDoorRenderer.color = bonusColor;
            rightDoorText.text = "+" + rightDoorBonusAmount;
        }
        else if(rightDoorBonusType == BonusType.Difference)
        {
            rightDoorRenderer.color = penaltyColor;
            rightDoorText.text = "-" + rightDoorBonusAmount;
        }
        else if(rightDoorBonusType == BonusType.Multiplication)
        {
            rightDoorRenderer.color = bonusColor;
            rightDoorText.text = "X" + rightDoorBonusAmount;
        }
        else if(rightDoorBonusType == BonusType.Division)
        {
            rightDoorRenderer.color = penaltyColor;
            rightDoorText.text = "/" + rightDoorBonusAmount;
        }

        //LeftDoor
        if (leftDoorBonusType == BonusType.Addition)
        {
            leftDoorRenderer.color = bonusColor;
            leftDoorText.text = "+" + leftDoorBonusAmount;
        }
        else if (leftDoorBonusType == BonusType.Difference)
        {
            leftDoorRenderer.color = penaltyColor;
            leftDoorText.text = "-" + leftDoorBonusAmount;
        }
        else if (leftDoorBonusType == BonusType.Multiplication)
        {
            leftDoorRenderer.color = bonusColor;
            leftDoorText.text = "X" + leftDoorBonusAmount;
        }
        else if (leftDoorBonusType == BonusType.Division)
        {
            leftDoorRenderer.color = penaltyColor;
            leftDoorText.text = "/" + leftDoorBonusAmount;
        }
    }

    public int GetBonusAmount(float xPosition)
    {
        if (xPosition > 0)
            return rightDoorBonusAmount;
        else
            return leftDoorBonusAmount;
    }

    public BonusType GetBonusType(float xPosition)
    {
        if (xPosition > 0)
            return rightDoorBonusType;
        else
            return leftDoorBonusType;
    }
}
