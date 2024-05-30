using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSelector : MonoBehaviour
{
    [SerializeField] GameObject upgradeCardPrefab;
    [SerializeField] Transform cardHolder;
    List<GameObject> upgradeCards = new List<GameObject>();

    public void GenerateUpgradeSelection(List<UpgradeScriptable> upgradeList)
    {
        cardHolder.gameObject.SetActive(true);

        FindObjectOfType<GameManager>().LockInteraction();

        bool selectedFirstSlot = false;
        foreach (UpgradeScriptable upgrade in upgradeList)
        {
            UpgradeCard card = Instantiate(upgradeCardPrefab, cardHolder).GetComponent<UpgradeCard>();
            card.SetInfo(upgrade);
            if(!selectedFirstSlot)
            {
                card.GetComponent<Button>().Select();
                selectedFirstSlot = true;
            }
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
