using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance;
    public Sprite FIRE0, FIRE1, FIRE2, FIRE3, START, SELECT, RB, LB, LT, RT, LS, RS, LEFT, RIGHT, DOWN, UP;

    // Use this for initialization
    public virtual void Awake()
    {
        Instance = this;
        RegisterActions();
    }
    public virtual void Start()
    {
        
    }
    public virtual void RegisterActions()
    {
        ActionSystem.Clear();


    }
    public virtual Sprite GetImage(string action)
    {
        Buttons b = ActionSystem.GetButton(action);
        Sprite val = Global.TransparentImage;
        if (Instance == null)
            return val;
        switch (b)
        {
            case Buttons.FIRE_0:
                val = Instance.FIRE0;
                break;
            case Buttons.FIRE_1:
                val = Instance.FIRE1;
                break;
            case Buttons.FIRE_2:
                val = Instance.FIRE2;
                break;
            case Buttons.FIRE_3:
                val = Instance.FIRE3;
                break;
            case Buttons.START:
                val = Instance.START;
                break;
            case Buttons.SELECT:
                val = Instance.SELECT;
                break;
            case Buttons.RIGHT_BUMPER:
                val = Instance.RB;
                break;
            case Buttons.LEFT_BUMPER:
                val = Instance.LB;
                break;
            case Buttons.LEFT_STICK:
                val = Instance.LS;
                break;
            case Buttons.RIGHT_STICK:
                val = Instance.RS;
                break;
            case Buttons.DPAD_LEFT:
                val = Instance.LEFT;
                break;
            case Buttons.DPAD_RIGHT:
                val = Instance.RIGHT;
                break;
            case Buttons.DPAD_UP:
                val = Instance.UP;
                break;
            case Buttons.DPAD_DOWN:
                val = Instance.DOWN;
                break;
            case Buttons.RIGHT_TRIGGER:
                val = Instance.RT;
                break;
            case Buttons.LEFT_TRIGGER:
                val = Instance.LT;
                break;
            default:
                break;
        }
        return val;
    }
    public static Sprite GetButtonImage(string a)
    {
       return Instance.GetImage(a);
    }

    public static bool GetButtonDown(Buttons b)
    {
        return Instance.ButtonDown(b);
    }

    public static bool GetButtonUp(Buttons b)
    {
        return Instance.ButtonUp(b);
    }
    /// <summary>
    /// Button Held
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool GetButton(Buttons b)
    {
        return Instance.Button(b);
    }
    public static float GetAxis(Axis a)
    {
        return Instance.AxisValue(a);
    }
    public static void StartHaptic(HapticIntensity h,float dur=.25f)
    {
        Instance.HapticFeedBack(h);
        Instance.StartCoroutine("HapticCo",dur);
    }
    protected virtual void HapticFeedBack(HapticIntensity h)
    {
       
    }
    public IEnumerator HapticCo(float dur)
    {
        yield return new WaitForSeconds(dur);
        StopVibration();
    }

    public virtual void StopVibration()
    {

    }

    protected virtual bool ButtonDown(Buttons b)
    {
        bool val = false;
        switch (b)
        {
            case Buttons.FIRE_0:
                if (Input.GetKeyDown(KeyCode.Return))
                    val = true;
                break;
            case Buttons.FIRE_1:
                if (Input.GetKeyDown(KeyCode.Space))
                    val = true;
                break;
            case Buttons.FIRE_2:
                if (Input.GetKeyDown(KeyCode.J))
                    val = true;
                break;
            case Buttons.FIRE_3:
                if (Input.GetKeyDown(KeyCode.K))
                    val = true;
                break;
            case Buttons.START:
                if (Input.GetKeyDown(KeyCode.P))
                    val = true;
                break;
            case Buttons.SELECT:
                if (Input.GetKeyDown(KeyCode.Backspace))
                    val = true;
                break;
            case Buttons.RIGHT_BUMPER:
                if (Input.GetKeyDown(KeyCode.O))
                    val = true;
                break;
            case Buttons.LEFT_BUMPER:
                if (Input.GetKeyDown(KeyCode.I))
                    val = true;
                break;
            case Buttons.LEFT_STICK:
                if (Input.GetKeyDown(KeyCode.LeftAlt))
                    val = true;
                break;
            case Buttons.RIGHT_STICK:
                if (Input.GetKeyDown(KeyCode.RightAlt))
                    val = true;
                break;
            case Buttons.DPAD_LEFT:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    val = true;
                break;
            case Buttons.DPAD_RIGHT:
                if (Input.GetKeyDown(KeyCode.RightArrow))
                    val = true;
                break;
            case Buttons.DPAD_UP:
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    val = true;
                break;
            case Buttons.DPAD_DOWN:
                if (Input.GetKeyDown(KeyCode.DownArrow))
                    val = true;
                break;
            default:
                break;
        }
        return val;
    }
    protected virtual bool ButtonUp(Buttons b)
    {
        bool val = false;
        switch (b)
        {
            case Buttons.FIRE_0:
                if (Input.GetKeyUp(KeyCode.Return))
                    val = true;
                break;
            case Buttons.FIRE_1:
                if (Input.GetKeyUp(KeyCode.Space))
                    val = true;
                break;
            case Buttons.FIRE_2:
                if (Input.GetKeyUp(KeyCode.J))
                    val = true;
                break;
            case Buttons.FIRE_3:
                if (Input.GetKeyUp(KeyCode.K))
                    val = true;
                break;
            case Buttons.START:
                if (Input.GetKeyUp(KeyCode.P))
                    val = true;
                break;
            case Buttons.SELECT:
                if (Input.GetKeyUp(KeyCode.Backspace))
                    val = true;
                break;
            case Buttons.RIGHT_BUMPER:
                if (Input.GetKeyUp(KeyCode.O))
                    val = true;
                break;
            case Buttons.LEFT_BUMPER:
                if (Input.GetKeyUp(KeyCode.I))
                    val = true;
                break;
            case Buttons.LEFT_STICK:
                if (Input.GetKeyUp(KeyCode.LeftAlt))
                    val = true;
                break;
            case Buttons.RIGHT_STICK:
                if (Input.GetKeyUp(KeyCode.RightAlt))
                    val = true;
                break;
            case Buttons.DPAD_LEFT:
                if (Input.GetKeyUp(KeyCode.LeftArrow))
                    val = true;
                break;
            case Buttons.DPAD_RIGHT:
                if (Input.GetKeyUp(KeyCode.RightArrow))
                    val = true;
                break;
            case Buttons.DPAD_UP:
                if (Input.GetKeyUp(KeyCode.UpArrow))
                    val = true;
                break;
            case Buttons.DPAD_DOWN:
                if (Input.GetKeyUp(KeyCode.DownArrow))
                    val = true;
                break;
            default:
                break;
        }
        return val;
    }
    protected virtual bool Button(Buttons b)
    {
        bool val = false;
        switch (b)
        {
            case Buttons.FIRE_0:
                if (Input.GetKey(KeyCode.Return))
                    val = true;
                break;
            case Buttons.FIRE_1:
                if (Input.GetKey(KeyCode.Space))
                    val = true;
                break;
            case Buttons.FIRE_2:
                if (Input.GetKey(KeyCode.J))
                    val = true;
                break;
            case Buttons.FIRE_3:
                if (Input.GetKey(KeyCode.K))
                    val = true;
                break;
            case Buttons.START:
                if (Input.GetKey(KeyCode.P))
                    val = true;
                break;
            case Buttons.SELECT:
                if (Input.GetKey(KeyCode.Backspace))
                    val = true;
                break;
            case Buttons.RIGHT_BUMPER:
                if (Input.GetKey(KeyCode.O))
                    val = true;
                break;
            case Buttons.LEFT_BUMPER:
                if (Input.GetKey(KeyCode.I))
                    val = true;
                break;
            case Buttons.LEFT_STICK:
                if (Input.GetKey(KeyCode.LeftAlt))
                    val = true;
                break;
            case Buttons.RIGHT_STICK:
                if (Input.GetKey(KeyCode.RightAlt))
                    val = true;
                break;
            case Buttons.DPAD_LEFT:
                if (Input.GetKey(KeyCode.LeftArrow))
                    val = true;
                break;
            case Buttons.DPAD_RIGHT:
                if (Input.GetKey(KeyCode.RightArrow))
                    val = true;
                break;
            case Buttons.DPAD_UP:
                if (Input.GetKey(KeyCode.UpArrow))
                    val = true;
                break;
            case Buttons.DPAD_DOWN:
                if (Input.GetKey(KeyCode.DownArrow))
                    val = true;
                break;
            default:
                break;
        }
        return val;
    }

    protected virtual float AxisValue(Axis a)
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
                if (Input.GetKey(KeyCode.LeftShift))
                    val = 1;
                break;
            case Axis.LEFT_TRIGGER:
                if (Input.GetKey(KeyCode.RightShift))
                    val = 1;
                break;
            default:
                break;
        }
        return val;
    }
    private void OnDestroy()
    {
        StopVibration();
    }
}
public enum Buttons
{
    FIRE_0, FIRE_1, FIRE_2, FIRE_3, START, SELECT, RIGHT_BUMPER, LEFT_BUMPER, LEFT_STICK, RIGHT_STICK, DPAD_LEFT, DPAD_RIGHT, DPAD_UP, DPAD_DOWN,RIGHT_TRIGGER,LEFT_TRIGGER
}

public enum Axis
{
    LEFT_HORIZONTAL, LEFT_VERTICAL, RIGHT_HORIZONTAL, RIGHT_VERTICAL, RIGHT_TRIGGER, LEFT_TRIGGER,DPAD_HORIZ,DPAD_VERTICAL
}

public enum HapticIntensity
{
    LOW, MEDIUM, HIGH
}
