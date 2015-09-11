using UnityEngine;
using System.Collections;


public class RoomObject : PlayerItem, IObjectInteraction
{

    [SerializeField]
    private float m_PickUpDistance = 0.5f;
    public float PickUpDistance { get { return m_PickUpDistance; } }

    private bool m_UsedByPlayer = false;
    public bool UsedByPlayer { get { return m_UsedByPlayer; } }

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

    void OnTriggerEnter(Collider aCollider)
    {
        // check if sender is player, if so
        ActivatedByPlayer();

        // call some function on player which tells it what to make visible
        // something like player.MakeVisible(ItemID m_ItemID);
        Debug.Log("now imagine that this is on the player.");
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
        MakeVisible(aState);
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
