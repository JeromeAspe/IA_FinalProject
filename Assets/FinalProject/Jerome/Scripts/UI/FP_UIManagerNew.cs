using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FP_UIManagerNew : MonoBehaviour
{
    [SerializeField] FP_Player player = null;
    [SerializeField] Image playerLifeBar = null;
    [SerializeField] TMP_Text playerMunitions = null;

    public bool IsValid => player;

    private void Start()
    {
        if (!IsValid) return;
        if (playerLifeBar)
        {
            player.OnLife += (_life) =>
            {
                playerLifeBar.fillAmount = _life / player.MaxLife;
            };
        }
        
    }

    private void Update()
    {
        UpdateMunitions();
    }

    public void UpdateMunitions()
    {
        if (!playerMunitions) return;
        playerMunitions.text = $"{player.PlayerShooter.CurrentBulletsNumber} / {player.PlayerShooter.BulletsNumberMax}";
    }


}
