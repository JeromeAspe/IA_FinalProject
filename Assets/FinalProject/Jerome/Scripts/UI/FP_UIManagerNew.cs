using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FP_UIManagerNew : MonoBehaviour
{
    [SerializeField] FP_Player player = null;
    [SerializeField] FP_IAPlayer enemy = null;
    [SerializeField] Image playerLifeBar = null;
    [SerializeField] TMP_Text playerMunitions = null;
    [SerializeField] TMP_Text playerScore = null;
    [SerializeField] TMP_Text enemyScore = null;

    //J ai mis la parceque c est super tard et il reste plein de choses a faire
    int scoreAlly = 0;
    int scoreEnemy = 0;

    public bool IsValid => player && enemy;

    private void Start()
    {
        Cursor.visible = false;
        if (!IsValid) return;
        if (playerLifeBar)
        {
            player.OnLife += (_life) =>
            {
                playerLifeBar.fillAmount = _life / player.MaxLife;
            };
        }
        if(playerScore && enemyScore)
        {
            player.OnDie += () =>
             {
                 scoreEnemy++;
                 enemyScore.text = scoreEnemy.ToString();
             };
            enemy.OnDie += () =>
            {
                scoreAlly++;
                playerScore.text = scoreAlly.ToString();
            };
            enemyScore.text = scoreEnemy.ToString();
            playerScore.text = scoreAlly.ToString();
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
