using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] Image[] imgUpgrades;
    [SerializeField] TMP_Text[] txtUpgradeLevels;
    
    public void ChangeUpgradeInfo(Upgrade upgrade, int position)
    {
        UpgradeScriptable scriptable = upgrade.upgradeInfoScriptable;

        ChangeImage(scriptable, position);
        ChangeLevel(upgrade, scriptable, position);
    }

    private void ChangeImage(UpgradeScriptable info, int position)
    {
        imgUpgrades[position].sprite = info.image;
    }

    private void ChangeLevel(Upgrade upgrade, UpgradeScriptable info, int position)
    {
        txtUpgradeLevels[position].text = info.level.ToString();

        if (upgrade.IsMaxLevel)
            txtUpgradeLevels[position].color = StaticVariables.redColor;
    }
}
