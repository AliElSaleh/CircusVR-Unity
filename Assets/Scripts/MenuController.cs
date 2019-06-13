using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Does an action based off of what menu button that was clicked on.
    // We don't have any specific behavior, but we could add them if we needed to.
    public void MenuButtonClick(int index)
    {
        switch (index)
        {
            case 0:
                ScoreManager.SetText("Button: " + index);
                break;
            case 1:
                ScoreManager.SetText("Button: " + index);
                break;
            case 2:
                ScoreManager.SetText("Button: " + index);
                break;
            case 3:
                ScoreManager.SetText("Button: " + index);
                break;
            case 4:
                print("Clicked button " + index);
                break;
            case 5:
                print("Clicked button " + index);
                break;
            case 6:
                print("Clicked button " + index);
                break;
            default:
                break;
        }
    }
}