using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTrigger : MonoBehaviour
{
    [Tooltip("Text object to place the text in")]
    public Text textBox;
    [Tooltip("String to insert into the textbox")]
    public string message;

    private void OnTriggerExit2D(Collider2D collision)
    {
        textBox.text = message;
        Destroy(this.gameObject);
    }
}
