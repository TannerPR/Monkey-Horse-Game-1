using UnityEngine;
using System.Collections;

public class SoundFloor : MonoBehaviour {

	// Use this for initialization
    public AudioClip m_Impact;
    private AudioSource m_AudioSource;

	void Start () {
        m_AudioSource = GetComponent<AudioSource>();
        
	}
	
    void OnCollisionEnter(Collision other)
    {
        //PlayerControl m_PlayerControl = other.transform.root.GetComponent<PlayerControl>();
        //if (m_PlayerControl == null)
        //{
        //    return;
        //}
        //
        //if (other.transform.root.GetComponent<PlayerControl>())
        //{
        //    //if (m_AudioSource != null && m_AudioSource.clip != null)
        //    //{
        //    if (!m_AudioSource.isPlaying)
        //    {
        //        m_AudioSource.PlayOneShot(m_Impact);
        //    }
        //    //}
        //}
    }
}
