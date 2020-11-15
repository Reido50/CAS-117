using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicTrigger : MonoBehaviour
{

    [Tooltip("Audio source to change the audio clip in")]
    public AudioSource source;
    [Tooltip("String to insert into the textbox")]
    public AudioClip newClip;

    private void OnTriggerExit2D(Collider2D collision)
    {
        source.clip = newClip;
        source.Play();
        Destroy(this.gameObject);
    }
}
