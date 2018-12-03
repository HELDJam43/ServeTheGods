using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionSystem
{
    static Dictionary<string, InputAction> actions;
    static ActionSystem()
    {
        actions = new Dictionary<string, InputAction>();
    }
    public static void Clear()
    {
        actions.Clear();
    }
    public static void RegisterAction(string actionName, Buttons input, params GameStateManager.GameState[] state)
    {
        if (actions.ContainsKey(actionName))
        {
            actions[actionName].Add(input);
            actions[actionName].AddValidState(state);
        }
        else
        {
            actions.Add(actionName, new InputAction());
            RegisterAction(actionName, input, state);
        }
    }
    public static void RegisterAction(string actionName, Axis input, params GameStateManager.GameState[] state)
    {
        if (actions.ContainsKey(actionName))
        {
            actions[actionName].Add(input);
            actions[actionName].AddValidState(state);
        }
        else
        {
            actions.Add(actionName, new InputAction());
            RegisterAction(actionName, input, state);
        }
    }
    public static void RegisterAction(string actionName, KeyCode input, params GameStateManager.GameState[] state)
    {
        if (actions.ContainsKey(actionName))
        {
            actions[actionName].Add(input);
            actions[actionName].AddValidState(state);
        }
        else
        {
            actions.Add(actionName, new InputAction());
            RegisterAction(actionName, input, state);
        }
    }
    public static bool OnActionUp(string actionName)
    {
        if (actions.ContainsKey(actionName))
        {
            if (!actions[actionName].IsStateValid()) return false;
            foreach (Buttons b in actions[actionName].Buttons)
            {
                if (InputHandler.GetButtonUp(b))
                    return true;
            }
            foreach (KeyCode kc in actions[actionName].Keycodes)
            {
                if (Input.GetKeyUp(kc))
                    return true;
            }
            return false;
        }
        return false;
    }

    public static bool OnActionHeld(string actionName)
    {
        if (actions.ContainsKey(actionName))
        {
            if (!actions[actionName].IsStateValid()) return false;
            foreach (Buttons b in actions[actionName].Buttons)
            {
                if (InputHandler.GetButton(b))
                    return true;
            }
            foreach (KeyCode kc in actions[actionName].Keycodes)
            {
                if (Input.GetKey(kc))
                    return true;
            }
            return false;
        }
        return false;
    }

    public static bool OnActionDown(string actionName)
    {
        if (actions.ContainsKey(actionName))
        {
            if (!actions[actionName].IsStateValid()) return false;
            foreach (Buttons b in actions[actionName].Buttons)
            {
                if (InputHandler.GetButtonDown(b))
                    return true;
            }
            foreach (KeyCode kc in actions[actionName].Keycodes)
            {
                if (Input.GetKeyDown(kc))
                    return true;
            }
            return false;
        }
        return false;
    }
    public static float GetActionAxis(string actionName)
    {
        float val = 0;
        if (actions.ContainsKey(actionName))
        {
            if (!actions[actionName].IsStateValid()) return 0;
            foreach (Axis a in actions[actionName].Axis)
            {
                if (Mathf.Abs(InputHandler.GetAxis(a)) > Mathf.Abs(val))
                    val = InputHandler.GetAxis(a);
            }
        }
        else
        {
            throw new System.Exception(actionName + "Not Found");
        }
        return val;
    }
    public static Buttons GetButton(string actionName, int index = 0)
    {
        if (!actions.ContainsKey(actionName))
            throw new System.Exception();
        return actions[actionName].Buttons[index];
    }
    public static KeyCode GetKey(string actionName, int index = 0)
    {
        if (!actions.ContainsKey(actionName))
            throw new System.Exception();
        return actions[actionName].Keycodes[index];
    }

    public const string
        NONE = "",
        PROCEEDTHROUGHDIALOGUE = "PROCEED",
        INTERACT = "INTERACT",
        MOVEAXIS_X = "MOVE_X",
        MOVEAXIS_Y = "MOVE_Y",
        JUMP = "JUMP",
        STARTGAME = "STARTGAME",
        PAUSE = "PAUSE",
        THROW = "THROW",
        MENUHORZ = "MENUHorizontal",
        MENUVERT = "MENUVertical",
        MENUSELECT = "MENUSelect",
        MENUQUIT = "MENUQuit",
        RUN = "run",
        CAMERAAXIS = "camaxis",
        MENUDPADUP="dpadUP",
        MENUDPADDOWN="dpadDOWN",
        MENUDPADLEFT="dpadLEFT",
        MENUDPADRIGHT="dpadRIGHT",
        RETRY="retry",
        NEXT="next"
        ;
}
class InputAction
{
    List<GameStateManager.GameState> validStates;
    List<Buttons> buttons;
    List<Axis> axis;
    List<KeyCode> keycodes;

    public List<Buttons> Buttons
    {
        get
        {
            return buttons;
        }
    }

    public List<Axis> Axis
    {
        get
        {
            return axis;
        }
    }

    public List<KeyCode> Keycodes
    {
        get
        {
            return keycodes;
        }
    }

    public List<GameStateManager.GameState> ValidStates
    {
        get
        {
            return validStates;
        }
    }

    public InputAction()
    {
        validStates = new List<GameStateManager.GameState>();
        buttons = new List<Buttons>();
        axis = new List<Axis>();
        keycodes = new List<KeyCode>();
    }
    public bool IsStateValid()
    {
        return ValidStates.Contains(GameStateManager.GameState.ANY) || ValidStates.Contains(GameStateManager.State);
    }
    public void AddValidState(GameStateManager.GameState[] states)
    {
        foreach (GameStateManager.GameState item in states)
        {
            if (!validStates.Contains(item))
                validStates.Add(item);
        }
    }
    public void Add(Buttons input)
    {
        if (!Contains(input))
            buttons.Add(input);
    }

    public void Add(Axis input)
    {
        if (!Contains(input))
            axis.Add(input);
    }

    public void Add(KeyCode input)
    {
        if (!Contains(input))
            keycodes.Add(input);
    }
    bool Contains(Buttons input)
    {
        return buttons.Contains(input);
    }

    bool Contains(Axis input)
    {
        return axis.Contains(input);
    }

    bool Contains(KeyCode input)
    {
        return keycodes.Contains(input);
    }
}
