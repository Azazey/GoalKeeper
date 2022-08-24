using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI _money;

    public void WriteMoney()
    {
        if (_money)
        {
            _money.text = PlayerPrefs.GetInt(PlayerBelongs.Money).ToString();
        }
    }

    public override void AttachToAction(Item item)
    {
        item.OnItemHit += WriteMoney;
    }

    private void Start()
    {
        WriteMoney();
    }
}