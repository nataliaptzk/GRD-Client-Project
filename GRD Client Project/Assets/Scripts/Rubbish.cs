using UnityEngine;

/// <summary>
/// This class contains the defition of the rubbish types.
/// - Natalia Pietrzak
/// </summary>
public class Rubbish : MonoBehaviour
{
    public rubbishTypes type;
}

public enum rubbishTypes
{
    landfill,
    industrialCompostable,
    plastic,
    likelyRecycled,
    notLikelyRecycled
}