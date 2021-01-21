using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FP_UIManagerNew : MonoBehaviour
{
    [SerializeField] FP_Player player = null;
    [SerializeField] Image playerLifeBar = null;

    public bool IsValid => player;

    private void Start()
    {
        if (!IsValid) return;
        player.OnLife += (_life) =>
        {
            playerLifeBar.fillAmount = _life / player.MaxLife;
        };
    }

    
}
