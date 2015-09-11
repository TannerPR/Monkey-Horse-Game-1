using UnityEngine;
using System.Collections;


public class RoomObject : MonoBehaviour, IObjectInteraction
{
    [SerializeField]
    private ItemID m_ItemID = ItemID.UNINITIALIZED;
    public ItemID ItemID { get { return m_ItemID; } }

    private bool m_UsedByPlayer = false;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_UsedByPlayer == true) { return; }
	}

    private void Reset()
    {
        m_UsedByPlayer = false;
        ToggleObject(m_UsedByPlayer);
    }

    private void ActivatedByPlayer()
    {
        m_UsedByPlayer = true;
        ToggleObject(m_UsedByPlayer);
    }

    private void ToggleObject(bool aCurrentState)
    {
        SetObjectVisibility(!aCurrentState);
        SetObjectCollision(!aCurrentState);
    }

    private void SetObjectVisibility(bool aState)
    {
        if (aState)
        {
            // enable any renderer
        }
        else
        {
            // disable any renderer
        }
    }

    private void SetObjectCollision(bool aState)
    {
        if (aState)
        {
            // enable any collision
        }
        else
        {
            // disable any collision
        }
    }

    #region INTERFACE
    public void OnInteraction(GameObject aPlayer)
    {
        // check if sender is player, if so
        ActivatedByPlayer();

        // call some function on player which tells it what to make visible
        // something like player.MakeVisible(ItemID m_ItemID);
        Debug.Log("now imagine that this is on the player.");
    }
    #endregion
}
