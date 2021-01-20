using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class FP_UIManager : FP_Singleton<FP_UIManager>
{
    [SerializeField, Header("Player HealthBar ")] Slider playerHealthSlider = null;
    [SerializeField, Header("Player HealthBar Image ")] Image playerHealthFillImageSlider = null;
    [SerializeField, Header("Current Weapon Capacity Text")] TMP_Text weaponCapacityText = null;
    [SerializeField, Header("Current Weapon Capacity Image")] Image weaponCapacityImage = null;
    [SerializeField, Header("Settings Button")] Button settingsButton = null;
    [SerializeField, Header("PlayerHead Image")] RawImage playerHeadImage = null;
    
    public override bool IsValid => base.IsValid && playerHealthSlider && playerHealthFillImageSlider
                                                 && weaponCapacityText && weaponCapacityImage 
                                                 && settingsButton     && playerHeadImage;







    public void UpdatePlayerHealthSlider(float _value)
    {
        if (!IsValid) return;
        playerHealthSlider.value = _value;
        playerHealthFillImageSlider.color = Color.Lerp(Color.red, Color.green, _value / 100f);
    }

    public void UpdateWeaponCapacityUI(int _currentCapacity,int _maxCapacity)
    {
        if (!IsValid) return;
        string _weaponCapacity = $"{_currentCapacity}/{_maxCapacity}";
        Debug.Log(_weaponCapacity);
        weaponCapacityText.text = _weaponCapacity;    
    }
  

}
