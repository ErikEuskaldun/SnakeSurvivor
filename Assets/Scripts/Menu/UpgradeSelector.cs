using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSelector : MonoBehaviour
{
    [SerializeField] GameObject upgradeCardPrefab;
    [SerializeField] Transform cardHolder;

    public void GenerateUpgradeSelection(List<UpgradeScriptable> upgradeList)
    {
        cardHolder.gameObject.SetActive(true);

        FindObjectOfType<GameManager>().LockInteraction();

        foreach (UpgradeScriptable upgrade in upgradeList)
        {
            UpgradeCard card = Instantiate(upgradeCardPrefab, cardHolder).GetComponent<UpgradeCard>();
            card.SetInfo(upgrade);
        }
    }

    public void CloseSelector()
    {
        foreach (Transform child in cardHolder)
        {
            Destroy(child.gameObject);
        }

        cardHolder.gameObject.SetActive(false);
    }
}
