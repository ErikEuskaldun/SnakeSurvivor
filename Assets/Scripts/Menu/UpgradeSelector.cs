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

        upgradeCards = new List<GameObject>();
        FindObjectOfType<GameManager>().LockInteraction();
        FindObjectOfType<SnakeController>().isPlayerStill = true;

        bool selectedFirstSlot = false;
        foreach (UpgradeScriptable upgrade in upgradeList)
        {
            UpgradeCard card = Instantiate(upgradeCardPrefab, cardHolder).GetComponent<UpgradeCard>();
            card.SetInfo(upgrade);
            upgradeCards.Add(card.gameObject);
            if(!selectedFirstSlot)
            {
                card.GetComponent<Button>().Select();
                selectedFirstSlot = true;
            }
        }

        for (int i = 0; i < upgradeCards.Count; i++)
        {
            Button uButton = upgradeCards[i].GetComponent<Button>();
            Navigation navigation = new Navigation();
            navigation.mode = Navigation.Mode.Explicit;

            if (i==0)
                navigation.selectOnLeft = upgradeCards[upgradeCards.Count-1].GetComponent<Button>();
            else
                navigation.selectOnLeft = upgradeCards[i - 1].GetComponent<Button>();
            if(i== upgradeCards.Count - 1)
                navigation.selectOnRight = upgradeCards[0].GetComponent<Button>();
            else
                navigation.selectOnRight = upgradeCards[i + 1].GetComponent<Button>();

            uButton.navigation = navigation;
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
