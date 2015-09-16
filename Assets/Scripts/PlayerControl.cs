using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour, IObjectInteraction, IResetable
{
    [SerializeField]
    private PlayerItem[] m_PlayerItems;


    /// <summary>
    /// This needs to be the rigidbody at the chest or perhaps pelvis (marine pelvis OR marine spine 3s)
    /// </summary>
    [SerializeField]
    private Rigidbody m_RigidBodyAtCenterOfMass;

    [SerializeField]
    private float m_FlingForceMultiplier = 1.0f;

    [SerializeField]
    private float m_MaxFlingForce = 10.0f;

    [SerializeField]
    private float m_MinFlingForce = 0.0f;

    private Vector3 m_InitialPositionOfCenterOfMass;

	// Use this for initialization
	void Start () 
    {
        SetAllItemVisibility(false);

        if (m_RigidBodyAtCenterOfMass == null)
        {
            //m_RigidBodyAtCenterOfMass = GetComponent<Rigidbody>();
            if (m_RigidBodyAtCenterOfMass == null)
            {
                Debug.Log("Cannot find rigidbody on PLayerControl " + GetInstanceID());
                this.enabled = false;
                return;
            }
        }

        m_InitialPositionOfCenterOfMass = m_RigidBodyAtCenterOfMass.transform.position;

        //lets try without contraints..
        //m_RigidBodyAtCenterOfMass.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
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

    private void ResetChildRigidbodies()
    {
        Rigidbody[] rbInChildren = GetComponentsInChildren<Rigidbody>();
        
        if (rbInChildren != null)
        {
            for (int i = 0 ; i < rbInChildren.Length ; i++)
            {
                if (rbInChildren[i] != null)
                {
                    rbInChildren[i].velocity = Vector3.zero;
                    rbInChildren[i].angularVelocity = Vector3.zero;
                }
            }
        }
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
        float force = Mathf.Clamp(aForcePower * m_FlingForceMultiplier, m_MinFlingForce, m_MaxFlingForce);
        m_RigidBodyAtCenterOfMass.AddForce(aForceDirection * force, ForceMode.Impulse);
    }


    public void OnInteraction(GameObject aPlayer)
    {
        // do nothing :P
    }

    public void OnReset()
    {
        SetAllItemVisibility(false);
        ResetChildRigidbodies();
        m_RigidBodyAtCenterOfMass.transform.position = m_InitialPositionOfCenterOfMass;
    }
}
