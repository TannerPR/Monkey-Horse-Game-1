using UnityEngine;
using System.Collections;


public class RoomObject : MonoBehaviour, IObjectInteraction
{
    [SerializeField]
    private ItemID m_ItemID = ItemID.UNINITIALIZED;
    public ItemID ItemID { get { return m_ItemID; } }

    private bool m_Visisble = true;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void ActivatedByPlayer()
    {

    }

    #region INTERFACE
    public void OnInteraction(GameObject aPlayer)
    {
        Debug.Log("now imagine that this is on the player.");
    }
    #endregion
}
