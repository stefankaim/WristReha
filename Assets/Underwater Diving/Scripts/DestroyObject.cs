using System.Collections;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
	public bool spaceBubble = false;

	public void DestroyEvent()
	{
		if (spaceBubble) Destroy (gameObject); 
	}
}