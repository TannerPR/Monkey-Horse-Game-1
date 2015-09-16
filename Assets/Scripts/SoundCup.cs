using UnityEngine;
using System.Collections;

public class SoundCup : MonoBehaviour
{

    // Use this for initialization
    public AudioClip[] m_Impact = new AudioClip[5];
    private AudioSource m_AudioSource;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        int rand = Random.Range(0, m_Impact.Length);

        if (other.transform.tag == "floor" || other.transform.tag == "grass")
        {
            if (m_AudioSource != null)
            {
                if (!m_AudioSource.isPlaying)
                {
                    m_AudioSource.PlayOneShot(m_Impact[rand]);
                }
            }
        }
    }
}
