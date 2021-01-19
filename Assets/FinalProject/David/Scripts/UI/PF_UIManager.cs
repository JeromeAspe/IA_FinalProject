using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PF_UIManager : FP_Singleton<PF_UIManager>
{
    [SerializeField, Header("Player HealthBar ")] Slider playerHealthSlider = null;
    [SerializeField, Header("Enemy HealthBar ")] Slider enemyHealthSlider = null;
    [SerializeField, Header("Current Weapon Capacity Text")] TMP_Text weaponCapacityText = null;
    [SerializeField, Header("RemainingLife Text")] TMP_Text remainingLifeText = null;

    [SerializeField, Header("Current Weapon Capacity Image")] Image weaponCapacityImage = null;
    [SerializeField, Header("RemainingLife Image")] Image remainingLifeImage = null;

    public override bool IsValid => base.IsValid 
                                                && playerHealthSlider && enemyHealthSlider && weaponCapacityText
                                                && remainingLifeText && weaponCapacityImage && remainingLifeImage;



}
