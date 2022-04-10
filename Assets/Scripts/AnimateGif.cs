using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateGif : MonoBehaviour
{
    public Sprite[] images;
    public float framesPerSecond = 1f;

    // Update is called once per frame
    void Update()
    {
        int index = (int)(Time.time * framesPerSecond);
        index = index % images.Length;
        this.GetComponent<Image>().sprite = images[index];
    }
}
