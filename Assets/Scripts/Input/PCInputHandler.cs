using UnityEngine;
using System.Collections;

public class PCInputHandler : InputHandler
{

    private KeyCode FIRE0key = KeyCode.Return,
    FIRE1key = KeyCode.Space,
    FIRE2key = KeyCode.J,
    FIRE3key = KeyCode.K,
    STARTkey = KeyCode.P,
    SELECTkey = KeyCode.Backspace,
    RBkey = KeyCode.O,
    LBkey = KeyCode.I,
    LTkey = KeyCode.LeftShift,
    RTkey = KeyCode.RightShift,
    LSkey = KeyCode.LeftAlt,
    RSkey = KeyCode.RightAlt,
    LEFTkey = KeyCode.LeftArrow,
    RIGHTkey = KeyCode.RightArrow,
    DOWNkey = KeyCode.DownArrow,
    UPkey = KeyCode.UpArrow;

    protected override bool ButtonDown(Buttons b)
    {
        bool val = false;
        switch (b)
        {
            case Buttons.FIRE_0:
                if (Input.GetKeyDown(FIRE0key))
                    val = true;
                break;
            case Buttons.FIRE_1:
                if (Input.GetKeyDown(FIRE1key))
                    val = true;
                break;
            case Buttons.FIRE_2:
                if (Input.GetKeyDown(FIRE2key))
                    val = true;
                break;
            case Buttons.FIRE_3:
                if (Input.GetKeyDown(FIRE3key))
                    val = true;
                break;
            case Buttons.START:
                if (Input.GetKeyDown(STARTkey))
                    val = true;
                break;
            case Buttons.SELECT:
                if (Input.GetKeyDown(SELECTkey))
                    val = true;
                break;
            case Buttons.RIGHT_BUMPER:
                if (Input.GetKeyDown(RBkey))
                    val = true;
                break;
            case Buttons.LEFT_BUMPER:
                if (Input.GetKeyDown(LBkey))
                    val = true;
                break;
            case Buttons.LEFT_STICK:
                if (Input.GetKeyDown(LSkey))
                    val = true;
                break;
            case Buttons.RIGHT_STICK:
                if (Input.GetKeyDown(RSkey))
                    val = true;
                break;
            case Buttons.DPAD_LEFT:
                if (Input.GetKeyDown(LEFTkey))
                    val = true;
                break;
            case Buttons.DPAD_RIGHT:
                if (Input.GetKeyDown(RIGHTkey))
                    val = true;
                break;
            case Buttons.DPAD_UP:
                if (Input.GetKeyDown(UPkey))
                    val = true;
                break;
            case Buttons.DPAD_DOWN:
                if (Input.GetKeyDown(DOWNkey))
                    val = true;
                break;
            default:
                break;
        }
        return val;
    }
    protected override bool ButtonUp(Buttons b)
    {
        bool val = false;
        switch (b)
        {
            case Buttons.FIRE_0:
                if (Input.GetKeyUp(FIRE0key))
                    val = true;
                break;
            case Buttons.FIRE_1:
                if (Input.GetKeyUp(FIRE1key))
                    val = true;
                break;
            case Buttons.FIRE_2:
                if (Input.GetKeyUp(FIRE2key))
                    val = true;
                break;
            case Buttons.FIRE_3:
                if (Input.GetKeyUp(FIRE3key))
                    val = true;
                break;
            case Buttons.START:
                if (Input.GetKeyUp(STARTkey))
                    val = true;
                break;
            case Buttons.SELECT:
                if (Input.GetKeyUp(SELECTkey))
                    val = true;
                break;
            case Buttons.RIGHT_BUMPER:
                if (Input.GetKeyUp(RBkey))
                    val = true;
                break;
            case Buttons.LEFT_BUMPER:
                if (Input.GetKeyUp(LBkey))
                    val = true;
                break;
            case Buttons.LEFT_STICK:
                if (Input.GetKeyUp(LSkey))
                    val = true;
                break;
            case Buttons.RIGHT_STICK:
                if (Input.GetKeyUp(RSkey))
                    val = true;
                break;
            case Buttons.DPAD_LEFT:
                if (Input.GetKeyUp(LEFTkey))
                    val = true;
                break;
            case Buttons.DPAD_RIGHT:
                if (Input.GetKeyUp(RIGHTkey))
                    val = true;
                break;
            case Buttons.DPAD_UP:
                if (Input.GetKeyUp(UPkey))
                    val = true;
                break;
            case Buttons.DPAD_DOWN:
                if (Input.GetKeyUp(DOWNkey))
                    val = true;
                break;
            default:
                break;
        }
        return val;
    }
    protected override bool Button(Buttons b)
    {
        bool val = false;
        switch (b)
        {
            case Buttons.FIRE_0:
                if (Input.GetKey(FIRE0key))
                    val = true;
                break;
            case Buttons.FIRE_1:
                if (Input.GetKey(FIRE1key))
                    val = true;
                break;
            case Buttons.FIRE_2:
                if (Input.GetKey(FIRE2key))
                    val = true;
                break;
            case Buttons.FIRE_3:
                if (Input.GetKey(FIRE3key))
                    val = true;
                break;
            case Buttons.START:
                if (Input.GetKey(STARTkey))
                    val = true;
                break;
            case Buttons.SELECT:
                if (Input.GetKey(SELECTkey))
                    val = true;
                break;
            case Buttons.RIGHT_BUMPER:
                if (Input.GetKey(RBkey))
                    val = true;
                break;
            case Buttons.LEFT_BUMPER:
                if (Input.GetKey(LBkey))
                    val = true;
                break;
            case Buttons.LEFT_STICK:
                if (Input.GetKey(LSkey))
                    val = true;
                break;
            case Buttons.RIGHT_STICK:
                if (Input.GetKey(RSkey))
                    val = true;
                break;
            case Buttons.DPAD_LEFT:
                if (Input.GetKey(LEFTkey))
                    val = true;
                break;
            case Buttons.DPAD_RIGHT:
                if (Input.GetKey(RIGHTkey))
                    val = true;
                break;
            case Buttons.DPAD_UP:
                if (Input.GetKey(UPkey))
                    val = true;
                break;
            case Buttons.DPAD_DOWN:
                if (Input.GetKey(DOWNkey))
                    val = true;
                break;
            default:
                break;
        }
        return val;
    }

    protected override float AxisValue(Axis a)
    {
        float val = 0;
        switch (a)
        {
            case Axis.LEFT_HORIZONTAL:
                if (Input.GetKey(KeyCode.A))
                    val = -1;
                else if (Input.GetKey(KeyCode.D))
                    val = 1;
                break;
            case Axis.LEFT_VERTICAL:
                if (Input.GetKey(KeyCode.S))
                    val = -1;
                else if (Input.GetKey(KeyCode.W))
                    val = 1;
                break;
            case Axis.RIGHT_HORIZONTAL:
                if (Input.GetKey(KeyCode.LeftArrow))
                    val = -1;
                else if (Input.GetKey(KeyCode.RightArrow))
                    val = 1;
                break;
            case Axis.RIGHT_VERTICAL:
                if (Input.GetKey(KeyCode.DownArrow))
                    val = -1;
                else if (Input.GetKey(KeyCode.UpArrow))
                    val = 1;
                break;
            case Axis.RIGHT_TRIGGER:
                if (Input.GetKey(RTkey))
                    val = 1;
                break;
            case Axis.LEFT_TRIGGER:
                if (Input.GetKey(LTkey))
                    val = 1;
                break;
            case Axis.DPAD_HORIZ:
                if (Input.GetKey(RIGHTkey))
                    val = 1;
                else if (Input.GetKey(LEFTkey))
                    val = -1;
                break;
            case Axis.DPAD_VERTICAL:
                if (Input.GetKey(UPkey))
                    val = 1;
                else if (Input.GetKey(DOWNkey))
                    val = -1;
                break;
            default:
                break;
        }
        return val;
    }
    public override Sprite GetImage(string action)
    {
        return Global.LoadKey(ActionSystem.GetKey(action));
    }
    public override void RegisterActions()
    {
         base.RegisterActions();
        ActionSystem.RegisterAction(ActionSystem.PROCEEDTHROUGHDIALOGUE, KeyCode.Space, GameStateManager.GameState.DIALOUGE);
        ActionSystem.RegisterAction(ActionSystem.PROCEEDTHROUGHDIALOGUE, KeyCode.Mouse0, GameStateManager.GameState.DIALOUGE);
        ActionSystem.RegisterAction(ActionSystem.INTERACT, KeyCode.Mouse1, GameStateManager.GameState.GAMEPLAY);
        ActionSystem.RegisterAction(ActionSystem.INTERACT, KeyCode.Q, GameStateManager.GameState.GAMEPLAY);

        ActionSystem.RegisterAction(ActionSystem.MOVEAXIS_X, Axis.LEFT_HORIZONTAL, GameStateManager.GameState.GAMEPLAY);
        ActionSystem.RegisterAction(ActionSystem.MOVEAXIS_Y, Axis.LEFT_VERTICAL, GameStateManager.GameState.GAMEPLAY);


        ActionSystem.RegisterAction(ActionSystem.CAMERAAXIS, Axis.LEFT_VERTICAL, GameStateManager.GameState.GAMEPLAY);
        ActionSystem.RegisterAction(ActionSystem.JUMP, KeyCode.Space, GameStateManager.GameState.GAMEPLAY);
        ActionSystem.RegisterAction(ActionSystem.STARTGAME, KeyCode.Mouse0, GameStateManager.GameState.STARTSCREEN);
        ActionSystem.RegisterAction(ActionSystem.STARTGAME, KeyCode.Space, GameStateManager.GameState.STARTSCREEN);
        ActionSystem.RegisterAction(ActionSystem.PAUSE, KeyCode.Escape, GameStateManager.GameState.GAMEPLAY, GameStateManager.GameState.PAUSE);
        ActionSystem.RegisterAction(ActionSystem.RUN, KeyCode.LeftShift, GameStateManager.GameState.GAMEPLAY);
        ActionSystem.RegisterAction(ActionSystem.MENUHORZ, Axis.LEFT_HORIZONTAL, GameStateManager.GameState.MENU, GameStateManager.GameState.PAUSE);
        ActionSystem.RegisterAction(ActionSystem.MENUVERT, Axis.LEFT_VERTICAL, GameStateManager.GameState.MENU, GameStateManager.GameState.PAUSE);
        ActionSystem.RegisterAction(ActionSystem.MENUSELECT, KeyCode.Space, GameStateManager.GameState.MENU, GameStateManager.GameState.PAUSE);
        ActionSystem.RegisterAction(ActionSystem.MENUQUIT, KeyCode.Escape, GameStateManager.GameState.MENU, GameStateManager.GameState.PAUSE);

    }
}
