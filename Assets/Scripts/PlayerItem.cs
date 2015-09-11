using UnityEngine;
using System.Collections;

public class PlayerItem : MonoBehaviour
{
    [SerializeField]
    private ItemID m_ItemID = ItemID.UNINITIALIZED;
    public ItemID ItemID { get { return m_ItemID; } }

    void Start()
    {
        if (m_ItemID == ItemID.UNINITIALIZED)
        {
            Debug.Log("Did not set ItemID enum on " + gameObject.GetInstanceID());
        }
    }

    public void MakeVisible(bool aVisible)
    {
        if (aVisible)
        {
            // turn on render
        }
        else
        {
            // turn off renderer
        }
        
    }
}
