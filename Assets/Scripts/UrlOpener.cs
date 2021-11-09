using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrlOpener : MonoBehaviour
{
    public string url;

    /// <summary>
    /// Opens the given url
    /// </summary>
    public void Open()
    {
        if (url != null && url.StartsWith("http")) Application.OpenURL(url);
    }
}
