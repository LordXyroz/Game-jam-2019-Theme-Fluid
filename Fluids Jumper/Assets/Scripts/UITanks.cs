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

    public Image waterIndicator;
    public Image gelIdicator;
    public Image superFluidIndicator;

    [Header("Text Elements")]
    public Text waterText;
    public Text gelText;
    public Text superFluidText;

    [Header("Data")]
    public FluidTanks data;

    [Header("Variables")]
    public float counter;

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        waterTank.fillAmount = data.waterAmount / data.maxStorage;
        gelTank.fillAmount = data.gelAmount / data.maxStorage;
        superFluidTank.fillAmount = data.superFluidAmount / data.maxStorage;

        waterIndicator.color = Color.Lerp(Color.red, Color.green, waterTank.fillAmount);
        gelIdicator.color = Color.Lerp(Color.red, Color.green, gelTank.fillAmount);
        superFluidIndicator.color = Color.Lerp(Color.red, Color.green, superFluidTank.fillAmount);

        float sine = (Mathf.Sin(counter * 5f) + 1) / 2f;

        if (waterTank.fillAmount < 0.2f)
        {
            waterText.color = new Color(waterText.color.r, waterText.color.g, waterText.color.b, sine);
        }
        else
        {
            waterText.color = new Color(waterText.color.r, waterText.color.g, waterText.color.b, 0);
        }
        if (gelTank.fillAmount < 0.2f)
        {
            gelText.color = new Color(gelText.color.r, gelText.color.g, gelText.color.b, sine);
        }
        else
        {
            gelText.color = new Color(gelText.color.r, gelText.color.g, gelText.color.b, 0);
        }
        if (superFluidTank.fillAmount < 0.2f)
        {
            superFluidText.color = new Color(superFluidText.color.r, superFluidText.color.g, superFluidText.color.b, sine);
        }
        else
        {
            superFluidText.color = new Color(superFluidText.color.r, superFluidText.color.g, superFluidText.color.b, 0);
        }
    }
}
