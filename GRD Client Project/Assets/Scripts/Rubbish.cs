using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubbish : MonoBehaviour
{
    public rubbishTypes type;
}

public enum rubbishTypes
{
    landfill,
    industrialCompostable,
    plastic
}