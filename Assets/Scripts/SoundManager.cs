using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    public AudioSource m_AudioSource;

	void Start ()
	{
	//	m_AaudioSource = GetComponent <AudioSource>();
	}

	//void OnCollisionEnter(Collision collision)
	//{
    //    PlayerControl playerControl = collision.transform.root.GetComponent<PlayerControl>();
    //
    //    if (playerControl != null)
    //    {
    //        return;
    //    }
    //
    //    if (collision.gameObject.name == "m_RoomFloor")
    //    {
    //        if (m_AaudioSource != null && m_AaudioSource.clip == null)
    //        {
    //            m_AaudioSource.PlayOneShot(impactTest);
    //            Debug.Log("player landed");
    //        }
    //    }
	//}
}
