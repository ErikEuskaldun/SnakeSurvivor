using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] Image imgIcon;
    [SerializeField] TMP_Text txtLevel, txtDescription;
    [SerializeField] UpgradeScriptable upgradeInfo;

    Button button;
    private void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(Click);
    }

    public void SetInfo(UpgradeScriptable upgradeInfo)
    {
        this.upgradeInfo = upgradeInfo;

        imgIcon.sprite = upgradeInfo.image;
        txtLevel.text = upgradeInfo.level.ToString();
        txtDescription.text = upgradeInfo.description;
    }

    private void Click()
    {
        UpgradesManager upgradeManager = FindObjectOfType<UpgradesManager>();

        if (upgradeInfo.level == 1)
            upgradeManager.NewUpgrade(upgradeInfo.upgradeName);
        else
            upgradeManager.LevelUpUpgrade(upgradeInfo.upgradeName);

        FindObjectOfType<GameManager>().ResumeInteraction();

        FindObjectOfType<UpgradeSelector>().CloseSelector();
    }
}
