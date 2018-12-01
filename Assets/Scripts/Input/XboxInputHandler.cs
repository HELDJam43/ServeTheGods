using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
public class XboxInputHandler : InputHandler
{
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    public int playerNumber = 1;
    float trigerTreshold = .5f;
    public override void Start()
    {
        base.Start();
        if (playerNumber == 0)
            throw new System.Exception("Player Must be greater than 0");
        playerNumber--;
    }
    private void Update()
    {

        if (!playerIndexSet || !prevState.IsConnected)
        {

            PlayerIndex testPlayerIndex = (PlayerIndex)playerNumber;
            GamePadState testState = GamePad.GetState(testPlayerIndex);
            if (testState.IsConnected)
            {
                //Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                playerIndex = testPlayerIndex;
                playerIndexSet = true;
            }

        }
        prevState = state;
        state = GamePad.GetState(playerIndex);
    }
    public override void RegisterActions()
    {
        base.RegisterActions();
        ActionSystem.RegisterAction(ActionSystem.PROCEEDTHROUGHDIALOGUE, Buttons.FIRE_0, GameStateManager.GameState.DIALOUGE);
        ActionSystem.RegisterAction(ActionSystem.INTERACT, Buttons.FIRE_0, GameStateManager.GameState.GAMEPLAY);

        ActionSystem.RegisterAction(ActionSystem.MOVEAXIS_X, Axis.LEFT_HORIZONTAL, GameStateManager.GameState.GAMEPLAY);
        ActionSystem.RegisterAction(ActionSystem.MOVEAXIS_Y, Axis.LEFT_VERTICAL, GameStateManager.GameState.GAMEPLAY);

        ActionSystem.RegisterAction(ActionSystem.MOVEAXIS_X, Axis.DPAD_VERTICAL, GameStateManager.GameState.GAMEPLAY);
        ActionSystem.RegisterAction(ActionSystem.MOVEAXIS_Y, Axis.DPAD_VERTICAL, GameStateManager.GameState.GAMEPLAY);


        ActionSystem.RegisterAction(ActionSystem.CAMERAAXIS, Axis.LEFT_VERTICAL, GameStateManager.GameState.GAMEPLAY);
        ActionSystem.RegisterAction(ActionSystem.CAMERAAXIS, Axis.DPAD_VERTICAL, GameStateManager.GameState.GAMEPLAY);

        ActionSystem.RegisterAction(ActionSystem.JUMP, Buttons.FIRE_0, GameStateManager.GameState.GAMEPLAY);
        ActionSystem.RegisterAction(ActionSystem.STARTGAME, Buttons.FIRE_0, GameStateManager.GameState.STARTSCREEN);
        ActionSystem.RegisterAction(ActionSystem.PAUSE, Buttons.START, GameStateManager.GameState.GAMEPLAY, GameStateManager.GameState.PAUSE);
        ActionSystem.RegisterAction(ActionSystem.RUN, Buttons.RIGHT_TRIGGER, GameStateManager.GameState.GAMEPLAY);
        ActionSystem.RegisterAction(ActionSystem.MENUHORZ, Axis.DPAD_HORIZ, GameStateManager.GameState.MENU, GameStateManager.GameState.PAUSE);
        ActionSystem.RegisterAction(ActionSystem.MENUVERT, Axis.DPAD_VERTICAL, GameStateManager.GameState.MENU, GameStateManager.GameState.PAUSE);
        ActionSystem.RegisterAction(ActionSystem.MENUHORZ, Axis.LEFT_HORIZONTAL, GameStateManager.GameState.MENU, GameStateManager.GameState.PAUSE);
        ActionSystem.RegisterAction(ActionSystem.MENUVERT, Axis.LEFT_VERTICAL, GameStateManager.GameState.MENU, GameStateManager.GameState.PAUSE);
        ActionSystem.RegisterAction(ActionSystem.MENUDPADDOWN, Buttons.DPAD_DOWN, GameStateManager.GameState.MENU, GameStateManager.GameState.PAUSE);
        ActionSystem.RegisterAction(ActionSystem.MENUDPADUP, Buttons.DPAD_UP, GameStateManager.GameState.MENU, GameStateManager.GameState.PAUSE);
        ActionSystem.RegisterAction(ActionSystem.MENUDPADLEFT, Buttons.DPAD_LEFT, GameStateManager.GameState.MENU, GameStateManager.GameState.PAUSE);
        ActionSystem.RegisterAction(ActionSystem.MENUDPADRIGHT, Buttons.DPAD_RIGHT, GameStateManager.GameState.MENU, GameStateManager.GameState.PAUSE);
        ActionSystem.RegisterAction(ActionSystem.MENUSELECT, Buttons.FIRE_0, GameStateManager.GameState.MENU, GameStateManager.GameState.PAUSE);
        ActionSystem.RegisterAction(ActionSystem.MENUQUIT, Buttons.FIRE_3, GameStateManager.GameState.MENU, GameStateManager.GameState.PAUSE);
    }

    protected override float AxisValue(Axis a)
    {
        if (!playerIndexSet)
            return 0;
        float val = 0;
        float sensitivity = 0.1f;
        switch (a)
        {
            case Axis.LEFT_HORIZONTAL:
                val = state.ThumbSticks.Left.X;
                if (val < sensitivity && val > -sensitivity)
                    val = 0;
                break;
            case Axis.LEFT_VERTICAL:
                val = state.ThumbSticks.Left.Y;
                if (val < sensitivity && val > -sensitivity)
                    val = 0;
                break;
            case Axis.RIGHT_HORIZONTAL:
                val = state.ThumbSticks.Right.X;
                if (val < sensitivity && val > -sensitivity)
                    val = 0;
                break;
            case Axis.RIGHT_VERTICAL:
                val = state.ThumbSticks.Right.Y;
                if (val < sensitivity && val > -sensitivity)
                    val = 0;
                break;
            case Axis.RIGHT_TRIGGER:
                val = state.Triggers.Right;
                break;
            case Axis.LEFT_TRIGGER:
                val = state.Triggers.Left;
                break;
            case Axis.DPAD_HORIZ:
                if (state.DPad.Right == ButtonState.Pressed)
                    val = 1;
                else if (state.DPad.Left == ButtonState.Pressed)
                    val = -1;
                break;
            case Axis.DPAD_VERTICAL:
                if (state.DPad.Up == ButtonState.Pressed)
                    val = 1;
                else if (state.DPad.Down == ButtonState.Pressed)
                    val = -1;
                break;
            default:
                break;
        }
        return val;
    }
    protected override bool Button(Buttons b)
    {
        if (!playerIndexSet)
            return false;
        bool val = false;
        switch (b)
        {
            case Buttons.FIRE_0:
                if (state.Buttons.A == ButtonState.Pressed)
                    val = true;
                break;
            case Buttons.FIRE_1:
                if (state.Buttons.X == ButtonState.Pressed)
                    val = true;
                break;
            case Buttons.FIRE_2:
                if (state.Buttons.Y == ButtonState.Pressed)
                    val = true;
                break;
            case Buttons.FIRE_3:
                if (state.Buttons.B == ButtonState.Pressed)
                    val = true;
                break;
            case Buttons.START:
                if (state.Buttons.Start == ButtonState.Pressed)
                    val = true;
                break;
            case Buttons.SELECT:
                if (state.Buttons.Back == ButtonState.Pressed)
                    val = true;
                break;
            case Buttons.RIGHT_BUMPER:
                if (state.Buttons.RightShoulder == ButtonState.Pressed)
                    val = true;
                break;
            case Buttons.LEFT_BUMPER:
                if (state.Buttons.LeftShoulder == ButtonState.Pressed)
                    val = true;
                break;
            case Buttons.LEFT_STICK:
                if (state.Buttons.LeftStick == ButtonState.Pressed)
                    val = true;
                break;
            case Buttons.RIGHT_STICK:
                if (state.Buttons.RightStick == ButtonState.Pressed)
                    val = true;
                break;
            case Buttons.DPAD_LEFT:
                if (state.DPad.Left == ButtonState.Pressed)
                    val = true;
                break;
            case Buttons.DPAD_RIGHT:
                if (state.DPad.Right == ButtonState.Pressed)
                    val = true;
                break;
            case Buttons.DPAD_UP:
                if (state.DPad.Up == ButtonState.Pressed)
                    val = true;
                break;
            case Buttons.DPAD_DOWN:
                if (state.DPad.Down == ButtonState.Pressed)
                    val = true;
                break;
            case Buttons.RIGHT_TRIGGER:
                if (state.Triggers.Right >= trigerTreshold)
                    val = true;
                break;
            case Buttons.LEFT_TRIGGER:
                if (state.Triggers.Left >= trigerTreshold)
                    val = true;
                break;
            default:
                break;
        }
        return val;
    }
    protected override bool ButtonDown(Buttons b)
    {
        if (!playerIndexSet)
            return false;
        bool val = false;
        switch (b)
        {
            case Buttons.FIRE_0:
                if ((prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.FIRE_1:
                if ((prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.FIRE_2:
                if ((prevState.Buttons.Y == ButtonState.Released && state.Buttons.Y == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.FIRE_3:
                if ((prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.START:
                if ((prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.SELECT:
                if ((prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.RIGHT_BUMPER:
                if ((prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.LEFT_BUMPER:
                if ((prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.LEFT_STICK:
                if ((prevState.Buttons.LeftStick == ButtonState.Released && state.Buttons.LeftStick == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.RIGHT_STICK:
                if ((prevState.Buttons.RightStick == ButtonState.Released && state.Buttons.RightStick == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.DPAD_LEFT:
                if ((prevState.DPad.Left == ButtonState.Released && state.DPad.Left == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.DPAD_RIGHT:
                if ((prevState.DPad.Right == ButtonState.Released && state.DPad.Right == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.DPAD_UP:
                if ((prevState.DPad.Up == ButtonState.Released && state.DPad.Up == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.DPAD_DOWN:
                if ((prevState.DPad.Down == ButtonState.Released && state.DPad.Down == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.RIGHT_TRIGGER:
                if (prevState.Triggers.Right < trigerTreshold && state.Triggers.Right >= trigerTreshold)
                    val = true;
                break;
            case Buttons.LEFT_TRIGGER:
                if (prevState.Triggers.Left < trigerTreshold && state.Triggers.Left >= trigerTreshold)
                    val = true;
                break;
            default:
                break;
        }
        return val;
    }
    protected override bool ButtonUp(Buttons b)
    {
        if (!playerIndexSet)
            return false;
        bool val = false;
        switch (b)
        {
            case Buttons.FIRE_0:
                if ((state.Buttons.A == ButtonState.Released && prevState.Buttons.A == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.FIRE_1:
                if ((state.Buttons.X == ButtonState.Released && prevState.Buttons.X == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.FIRE_2:
                if ((state.Buttons.Y == ButtonState.Released && prevState.Buttons.Y == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.FIRE_3:
                if ((state.Buttons.B == ButtonState.Released && prevState.Buttons.B == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.START:
                if ((state.Buttons.Start == ButtonState.Released && prevState.Buttons.Start == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.SELECT:
                if ((state.Buttons.Back == ButtonState.Released && prevState.Buttons.Back == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.RIGHT_BUMPER:
                if ((state.Buttons.RightShoulder == ButtonState.Released && prevState.Buttons.RightShoulder == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.LEFT_BUMPER:
                if ((state.Buttons.LeftShoulder == ButtonState.Released && prevState.Buttons.LeftShoulder == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.LEFT_STICK:
                if ((state.Buttons.LeftStick == ButtonState.Released && prevState.Buttons.LeftStick == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.RIGHT_STICK:
                if ((state.Buttons.RightStick == ButtonState.Released && prevState.Buttons.RightStick == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.DPAD_LEFT:
                if ((state.DPad.Left == ButtonState.Released && prevState.DPad.Left == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.DPAD_RIGHT:
                if ((state.DPad.Right == ButtonState.Released && prevState.DPad.Right == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.DPAD_UP:
                if ((state.DPad.Up == ButtonState.Released && prevState.DPad.Up == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.DPAD_DOWN:
                if ((state.DPad.Down == ButtonState.Released && prevState.DPad.Down == ButtonState.Pressed))
                    val = true;
                break;
            case Buttons.RIGHT_TRIGGER:
                if (state.Triggers.Right < trigerTreshold && prevState.Triggers.Right >= trigerTreshold)
                    val = true;
                break;
            case Buttons.LEFT_TRIGGER:
                if (state.Triggers.Left < trigerTreshold && prevState.Triggers.Left >= trigerTreshold)
                    val = true;
                break;
            default:
                break;
        }
        return val;
    }
    protected override void HapticFeedBack(HapticIntensity h)
    {
        if (!playerIndexSet)
            return;
        float strength = 0;
        switch (h)
        {
            case HapticIntensity.LOW:
                strength = .1f;
                break;
            case HapticIntensity.MEDIUM:
                strength = .2f;
                break;
            case HapticIntensity.HIGH:
                strength = .5f;
                break;
            default:
                break;
        }

        GamePad.SetVibration(playerIndex, strength, strength);
    }
    public override void StopVibration()
    {
        if (!playerIndexSet)
            return;
        base.StopVibration();
        GamePad.SetVibration(playerIndex, 0, 0);
    }
}
