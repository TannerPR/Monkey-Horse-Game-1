using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour, IObjectInteraction
{
    [SerializeField]
    private PlayerItem[] m_PlayerItems;

    [SerializeField]
    private Rigidbody m_RigidBody;

    [SerializeField]
    private float m_FlingForceMultiplier = 1.0f;

    [SerializeField]
    private float m_MaxFlingForce = 10.0f;

	// Use this for initialization
	void Start () 
    {
        SetAllItemVisibility(false);

        if (m_RigidBody == null)
        {
            m_RigidBody = GetComponent<Rigidbody>();
            if (m_RigidBody == null)
            {
                Debug.Log("Cannot find rigidbody on PLayerControl " + GetInstanceID());
                this.enabled = false;
                return;
            }
        }

        m_RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
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

    public void ApplyFling(Vector3 aForceDirection, float aForcePower)
    {
        float force = Mathf.Clamp(aForcePower * m_FlingForceMultiplier, 0.0f, m_MaxFlingForce);
        m_RigidBody.AddForce(aForceDirection * force, ForceMode.Impulse);
    }


    public void OnInteraction(GameObject aPlayer)
    {
        // do nothing :P
    }
}
