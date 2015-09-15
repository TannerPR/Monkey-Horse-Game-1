using UnityEngine;
using System.Collections;

public class SoundCup : MonoBehaviour
{

    // Use this for initialization
    public AudioClip m_Impact;
    private AudioSource m_AudioSource;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.transform.tag == "floor" || other.transform.tag == "grass")
        {
            //if (m_AudioSource != null && m_AudioSource.clip != null)
            //{
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.PlayOneShot(m_Impact);
            }
            //}
        }
    }
}
