using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private PlayerItem[] m_PlayerItems;

	// Use this for initialization
	void Start () 
    {
        SetAllItemVisibility(false);
	}

	// Update is called once per frame
	void Update () 
    {

	}

    private void SetAllItemVisibility(bool aVisible)
    {
        for (int i = 0 ; i < m_PlayerItems.Length ; i++)
        {
            if (m_PlayerItems[i] != null)
            {
                m_PlayerItems[i].MakeVisible(aVisible);
            }
        }
    }

    private PlayerItem FindPlayerItem(ItemID aItemID)
    {
        for (int i = 0; i < m_PlayerItems.Length; i++)
        {
            if (m_PlayerItems[i] != null && m_PlayerItems[i].ItemID == aItemID)
            {
                return m_PlayerItems[i];
            }
        }
        return null;
    }

    private PlayerItem[] FindAllPlayerItemOfType(ItemID aItemID)
    {
        List<PlayerItem> foundItems = new List<PlayerItem>();

        for (int i = 0; i < m_PlayerItems.Length; i++)
        {
            if (m_PlayerItems[i] != null && m_PlayerItems[i].ItemID == aItemID)
            {
                foundItems.Add(m_PlayerItems[i]);
            }
        }

        return foundItems.ToArray();
    }

    public void SetItemVisibility(ItemID aItemID, bool aVisible = true)
    {
        PlayerItem item = FindPlayerItem(aItemID);
        
        if (item != null)
        {
            item.MakeVisible(aVisible);
        }
    }

    public void SetAllItemOfSameTypeVisibility(ItemID aItemID, bool aVisible = true)
    {
        PlayerItem[] items = FindAllPlayerItemOfType(aItemID);

        if (items != null)
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].MakeVisible(aVisible);
            }
        }
    }

}
