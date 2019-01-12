using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FluidSelector : MonoBehaviour
{
    [Header("Managers")]
    public InputManager inputManager;

    [Header("UI Elements")]
    public Image waterTankBG;
    public Image gelTankBG;
    public Image superFluidTankBG;

    [Header("Other")]
    public FluidType fluidType;

    // Start is called before the first frame update
    void Start()
    {
        fluidType = FluidType.Water;

        waterTankBG.color = Color.white;
        gelTankBG.color = Color.black;
        superFluidTankBG.color = Color.black;

        inputManager.RegisterAction(InputManager.Keys.actionbar1, SelectWater, GetInstanceID());
        inputManager.RegisterAction(InputManager.Keys.actionbar2, SelectGel, GetInstanceID());
        inputManager.RegisterAction(InputManager.Keys.actionbar3, SelectSuperFluid, GetInstanceID());
    }

    
    public void SelectWater()
    {
        waterTankBG.color = Color.white;
        gelTankBG.color = Color.black;
        superFluidTankBG.color = Color.black;
        fluidType = FluidType.Water;
    }

    public void SelectGel()
    {
        waterTankBG.color = Color.black;
        gelTankBG.color = Color.white;
        superFluidTankBG.color = Color.black;
        fluidType = FluidType.Gel;
    }

    public void SelectSuperFluid()
    {
        waterTankBG.color = Color.black;
        gelTankBG.color = Color.black;
        superFluidTankBG.color = Color.white;
        fluidType = FluidType.SuperFluid;
    }
}
