using UnityEngine;

public class ToggleTextButton : MonoBehaviour
{
    public GameObject onText;
    public GameObject offText;

    private bool isOn = false;

    public void Toggle(){
        isOn =!isOn;

        onText.SetActive(isOn);
        offText.SetActive(!isOn);
    }
}