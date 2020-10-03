using UnityEngine;

[CreateAssetMenu(fileName = "FishData", menuName = "Fish", order = 1)]

public class FishData : ScriptableObject
{

    [SerializeField] int fishPrice = default;

    [SerializeField] int fishCount = default;

    [SerializeField] float minLength = default;

    [SerializeField] float maxLength = default;

    [SerializeField] float collierRadius = default;


    public int FishPrice
    {
        get
        {
            return fishPrice;
        }
    }

    public int FishCount
    {
        get
        {
            return fishCount;
        }
    }

    public float MinLength
    {
        get
        {
            return minLength;
        }
    }

    public float MaxLength
    {
        get
        {
            return maxLength;
        }
    }

    public float ColliderRadius
    {
        get
        {
            return collierRadius;
        }
    }
}
