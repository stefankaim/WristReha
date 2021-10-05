using UnityEngine;

public class DropDownHandler : MonoBehaviour
{
    /// <summary>
    /// The DropDown element for the wrist selection
    /// </summary>
    public TMPro.TMP_Dropdown ddWrist;

    /// <summary>
    /// On the first call
    /// </summary>
    private void Start()
    {
        if (Menu.wrist == 1)
        {
            ddWrist.value = 0;
        }
        else if (Menu.wrist == -1)
        {
            ddWrist.value = 1;
        }
        else
        {
            ddWrist.value = Menu.wrist;
        }
    }

    /// <summary>
    /// When the wrist selection changed
    /// </summary>
    /// <param name="sender">The DropDown that has changed</param>
    public void ddWristValueChanged(TMPro.TMP_Dropdown sender)
    {
        Menu.ChangeWrist(sender.value);
    }
}
