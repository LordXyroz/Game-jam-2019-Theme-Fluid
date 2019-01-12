using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidSelector : MonoBehaviour
{
    [Header("Managers")]
    public InputManager inputManager;

    public FluidType fluidType;

    // Start is called before the first frame update
    void Start()
    {
        inputManager.RegisterAction(InputManager.Keys.actionbar1, SelectWater, GetInstanceID());
        inputManager.RegisterAction(InputManager.Keys.actionbar2, SelectGel, GetInstanceID());
        inputManager.RegisterAction(InputManager.Keys.actionbar3, SelectSuperFluid, GetInstanceID());
    }

    
    public void SelectWater()
    {
        fluidType = FluidType.Water;
    }

    public void SelectGel()
    {
        fluidType = FluidType.Gel;
    }

    public void SelectSuperFluid()
    {
        fluidType = FluidType.SuperFluid;
    }
}
