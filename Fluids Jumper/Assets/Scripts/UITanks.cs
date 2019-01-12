using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITanks : MonoBehaviour
{
    [Header("UI Elements")]
    public Image waterTank;
    public Image gelTank;
    public Image superFluidTank;

    [Header("Data")]
    public FluidTanks data;


    // Start is called before the first frame update
    void Start()
    {
        waterTank.fillMethod = Image.FillMethod.Vertical;
        gelTank.fillMethod = Image.FillMethod.Vertical;
        superFluidTank.fillMethod = Image.FillMethod.Vertical;
    }

    // Update is called once per frame
    void Update()
    {
        waterTank.fillAmount = data.waterAmount / data.maxStorage;
        gelTank.fillAmount = data.gelAmount / data.maxStorage;
        superFluidTank.fillAmount = data.superFluidAmount / data.maxStorage;
    }
}
