using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public TabButton selectedTab;
    public GameManager gameManager;

    public List<GameObject> objectsToSwap;
    public List<TabButton> tabButtons;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }
    public void Subscribe(TabButton button)
    {
        if (tabButtons == null){
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(button);
    }
    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.sprite = tabHover;
        }
    }
    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }
    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        gameManager.store.clickToSeeMorePanel.SetActive(false);
        ResetTabs();
        button.background.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i<objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }
    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if (selectedTab!= null & button == selectedTab) { continue; }
            button.background.sprite = tabIdle;
        }
    }
    
}
