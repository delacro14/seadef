using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{

    Button selectedButton;
    Color blueColour = Color.blue;
    Color whiteColour = Color.white;

    public void ButtonClicked(Button ButtonTest)
    {
        if (selectedButton == ButtonTest)
        {
            ButtonTest.GetComponent<Image>().color = whiteColour;
            selectedButton = null;
        }
        else
        {
            if (selectedButton != null)
                selectedButton.GetComponent<Image>().color = whiteColour;
            ButtonTest.GetComponent<Image>().color = new Color32(255, 255, 225, 100);
            selectedButton = ButtonTest;
        }
    }
}