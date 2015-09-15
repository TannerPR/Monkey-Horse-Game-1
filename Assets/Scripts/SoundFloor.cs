using UnityEngine;
using System.Collections;

public class SoundFloor : MonoBehaviour {

	// Use this for initialization
    public AudioClip m_Impact;
    private AudioSource m_AudioSource;

	void Start () {
        m_AudioSource = GetComponent<AudioSource>();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        PlayerControl m_PlayerControl = transform.root.GetComponent<PlayerControl>();
        if (m_PlayerControl == null)
        {
            return;
        }
        if (m_AudioSource != null && m_AudioSource.clip != null)
        {
            m_AudioSource.PlayOneShot(m_Impact);

        }
        if (other.transform.root.GetComponent<PlayerControl>())
        {
            Debug.Log("PlayerLanded");
        }

        if (other.transform.tag == "m_TeddyBear")
        {
            Debug.Log("Turdylanded");
        }
    }
}
