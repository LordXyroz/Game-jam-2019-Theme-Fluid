using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="FluidTanks", menuName = "Data/FluidTanks", order = 1)]
public class FluidTanks : ScriptableObject
{
    public float maxStorage;

    public float waterAmount;
    public float gelAmount;
    public float superFluidAmount;
    
}
