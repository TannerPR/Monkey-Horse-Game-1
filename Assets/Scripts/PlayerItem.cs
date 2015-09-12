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
            Debug.Log("Did not set ItemID enum on PlayerItem " + GetInstanceID());
        }
    }

    public void MakeVisible(bool aVisible)
    {
        Renderer rend = GetComponent<Renderer>();
        if (null != rend)
        {
            rend.enabled = aVisible;
        }
    }

}
