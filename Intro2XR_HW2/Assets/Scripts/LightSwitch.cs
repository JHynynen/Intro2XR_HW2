using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public Light light1;
    public Light light2;
    public InputActionReference action;
    Color[] colors = { Color.red, Color.green, Color.blue, Color.white };
    int colorCounter = 0;
    //bool disco = false;
    //int frames = 25;
    int colorNum;

    void Start()
    {
        colorNum = colors.Length;

        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            //disco = !disco;


            colorCounter += 1;
            if (colorCounter >= colorNum)
            {
                colorCounter = 0;
            }
            light1.color = colors[colorCounter];
            light2.color = colors[colorCounter];
        };
    }


    /*
    void Update()
    {
        
        if (disco)
        {
            colorCounter += 1;
            if (colorCounter >= frames * 4)
            {
                colorCounter = 0;
            }
            light1.color = colors[colorCounter / frames];
            light2.color = colors[colorCounter / frames];
        }
        
    }
    */
}
