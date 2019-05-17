using System.IO;
using System.Threading;
using Tao.Sdl;
using System;

class SdlHardware
{
    static IntPtr hiddenScreen;
    static short width, height;

    static short startX, startY; // For Scroll

    static bool isThereJoystick;
    static IntPtr joystick;

    static int mouseClickLapse;
    static int lastMouseClick;


    public static void Init(short w, short h, int colors, bool fullScreen)
    {
        width = w;
        height = h;

        int flags = Sdl.SDL_HWSURFACE | Sdl.SDL_DOUBLEBUF | Sdl.SDL_ANYFORMAT;
        if (fullScreen)
            flags |= Sdl.SDL_FULLSCREEN;
        Sdl.SDL_Init(Sdl.SDL_INIT_EVERYTHING);
        hiddenScreen = Sdl.SDL_SetVideoMode(
            width,
            height,
            colors,
            flags);

        Sdl.SDL_Rect rect2 =
            new Sdl.SDL_Rect(0, 0, (short)width, (short)height);
        Sdl.SDL_SetClipRect(hiddenScreen, ref rect2);

        SdlTtf.TTF_Init();

        // Joystick initialization
        isThereJoystick = true;
        if (Sdl.SDL_NumJoysticks() < 1)
            isThereJoystick = false;

        if (isThereJoystick)
        {
            joystick = Sdl.SDL_JoystickOpen(0);
            if (joystick == IntPtr.Zero)
                isThereJoystick = false;
        }

        // Time lapse between two consecutive mouse clicks,
        // so that they are not too near
        mouseClickLapse = 10;
        lastMouseClick = Sdl.SDL_GetTicks();
    }

    public static void ClearScreen()
    {
        Sdl.SDL_Rect origin = new Sdl.SDL_Rect(0, 0, width, height);
        Sdl.SDL_FillRect(hiddenScreen, ref origin, 0);
    }

    public static void DrawHiddenImage(Image image, int x, int y)
    {
        drawHiddenImage(image.GetPointer(), x + startX, y + startY);
    }

    public static void ShowHiddenScreen()
    {
        Sdl.SDL_Flip(hiddenScreen);
    }

    public static bool KeyPressed(int c)
    {
        bool pressed = false;
        Sdl.SDL_PumpEvents();
        Sdl.SDL_Event myEvent;
        Sdl.SDL_PollEvent(out myEvent);
        int numkeys;
        byte[] keys = Tao.Sdl.Sdl.SDL_GetKeyState(out numkeys);
        if (keys[c] == 1)
            pressed = true;
        return pressed;
    }

    public static void Pause(int milisegundos)
    {
        Thread.Sleep(milisegundos);
    }

    public static int GetWidth()
    {
        return width;
    }

    public static int GetHeight()
    {
        return height;
    }

    public static void FatalError(string text)
    {
        StreamWriter sw = File.AppendText("errors.log");
        sw.WriteLine(text);
        sw.Close();
        Console.WriteLine(text);
        System.Environment.Exit(1);
    }

    public static void WriteHiddenText(string txt,
        short x, short y, byte r, byte g, byte b, Font f)
    {
        Sdl.SDL_Color color = new Sdl.SDL_Color(r, g, b);
        IntPtr textoComoImagen = SdlTtf.TTF_RenderText_Solid(
            f.GetPointer(), txt, color);
        if (textoComoImagen == IntPtr.Zero)
            System.Environment.Exit(5);

        Sdl.SDL_Rect origen = new Sdl.SDL_Rect(0, 0, width, height);
        Sdl.SDL_Rect dest = new Sdl.SDL_Rect(
            (short)(x + startX), (short)(y + startY),
            width, height);

        Sdl.SDL_BlitSurface(textoComoImagen, ref origen,
            hiddenScreen, ref dest);
    }

    // Scroll Methods

    public static void ResetScroll()
    {
        startX = startY = 0;
    }

    public static void ScrollTo(short newStartX, short newStartY)
    {
        startX = newStartX;
        startY = newStartY;
    }

    public static void ScrollHorizontally(short xDespl)
    {
        startX += xDespl;
    }

    public static void ScrollVertically(short yDespl)
    {
        startY += yDespl;
    }

    // Joystick methods

    /** JoystickPressed: returns TRUE if
        *  a certain button in the joystick/gamepad
        *  has been pressed
        */
    public static bool JoystickPressed(int boton)
    {
        if (!isThereJoystick)
            return false;

        if (Sdl.SDL_JoystickGetButton(joystick, boton) > 0)
            return true;
        else
            return false;
    }

    /** JoystickMoved: returns TRUE if
        *  the joystick/gamepad has been moved
        *  up to the limit in any direction
        *  Then, int returns the corresponding
        *  X (1=right, -1=left)
        *  and Y (1=down, -1=up)
        */
    public static bool JoystickMoved(out int posX, out int posY)
    {
        posX = 0; posY = 0;
        if (!isThereJoystick)
            return false;

        posX = Sdl.SDL_JoystickGetAxis(joystick, 0);
        posY = Sdl.SDL_JoystickGetAxis(joystick, 1);

        if (posX == -32768) posX = -1;
        else if (posX == 32767) posX = 1;
        else posX = 0;
        if (posY == -32768) posY = -1;
        else if (posY == 32767) posY = 1;
        else posY = 0;

        if ((posX != 0) || (posY != 0))
            return true;
        else
            return false;
    }


    /** JoystickMovedRight: returns TRUE if
        *  the joystick/gamepad has been moved
        *  completely to the right
        */
    public static bool JoystickMovedRight()
    {
        if (!isThereJoystick)
            return false;

        int posX = 0, posY = 0;
        if (JoystickMoved(out posX, out posY) && (posX == 1))
            return true;
        else
            return false;
    }

    /** JoystickMovedLeft: returns TRUE if
        *  the joystick/gamepad has been moved
        *  completely to the left
        */
    public static bool JoystickMovedLeft()
    {
        if (!isThereJoystick)
            return false;

        int posX = 0, posY = 0;
        if (JoystickMoved(out posX, out posY) && (posX == -1))
            return true;
        else
            return false;
    }


    /** JoystickMovedUp: returns TRUE if
        *  the joystick/gamepad has been moved
        *  completely upwards
        */
    public static bool JoystickMovedUp()
    {
        if (!isThereJoystick)
            return false;

        int posX = 0, posY = 0;
        if (JoystickMoved(out posX, out posY) && (posY == -1))
            return true;
        else
            return false;
    }


    /** JoystickMovedDown: returns TRUE if
        *  the joystick/gamepad has been moved
        *  completely downwards
        */
    public static bool JoystickMovedDown()
    {
        if (!isThereJoystick)
            return false;

        int posX = 0, posY = 0;
        if (JoystickMoved(out posX, out posY) && (posY == 1))
            return true;
        else
            return false;
    }


    /** GetMouseX: returns the current X
        *  coordinate of the mouse position
        */
    public static int GetMouseX()
    {
        int posX = 0, posY = 0;
        Sdl.SDL_PumpEvents();
        Sdl.SDL_GetMouseState(out posX, out posY);
        return posX;
    }


    /** GetMouseY: returns the current Y
        *  coordinate of the mouse position
        */
    public static int GetMouseY()
    {
        int posX = 0, posY = 0;
        Sdl.SDL_PumpEvents();
        Sdl.SDL_GetMouseState(out posX, out posY);
        return posY;
    }


    /** MouseClicked: return TRUE if
        *  the (left) mouse button has been clicked
        */
    public static bool MouseClicked()
    {
        int posX = 0, posY = 0;

        Sdl.SDL_PumpEvents();

        // To avoid two consecutive clicks
        int now = Sdl.SDL_GetTicks();
        if (now - lastMouseClick < mouseClickLapse)
            return false;

        if ((Sdl.SDL_GetMouseState(out posX, out posY) & Sdl.SDL_BUTTON(1)) == 1)
        {
            lastMouseClick = now;
            return true;
        }
        else
            return false;
    }


    // Private (auxiliar) methods

    private static void drawHiddenImage(IntPtr image, int x, int y)
    {
        Sdl.SDL_Rect origin = new Sdl.SDL_Rect(0, 0, width, height);
        Sdl.SDL_Rect dest = new Sdl.SDL_Rect((short)x, (short)y,
            width, height);
        Sdl.SDL_BlitSurface(image, ref origin, hiddenScreen, ref dest);
    }


    // Alternate key definitions

    public static int KEY_ESC = Sdl.SDLK_ESCAPE;
    public static int KEY_SPC = Sdl.SDLK_SPACE;
    public static int KEY_A = Sdl.SDLK_a;
    public static int KEY_B = Sdl.SDLK_b;
    public static int KEY_C = Sdl.SDLK_c;
    public static int KEY_D = Sdl.SDLK_d;
    public static int KEY_E = Sdl.SDLK_e;
    public static int KEY_F = Sdl.SDLK_f;
    public static int KEY_G = Sdl.SDLK_g;
    public static int KEY_H = Sdl.SDLK_h;
    public static int KEY_I = Sdl.SDLK_i;
    public static int KEY_J = Sdl.SDLK_j;
    public static int KEY_K = Sdl.SDLK_k;
    public static int KEY_L = Sdl.SDLK_l;
    public static int KEY_M = Sdl.SDLK_m;
    public static int KEY_N = Sdl.SDLK_n;
    public static int KEY_O = Sdl.SDLK_o;
    public static int KEY_P = Sdl.SDLK_p;
    public static int KEY_Q = Sdl.SDLK_q;
    public static int KEY_R = Sdl.SDLK_r;
    public static int KEY_S = Sdl.SDLK_s;
    public static int KEY_T = Sdl.SDLK_t;
    public static int KEY_U = Sdl.SDLK_u;
    public static int KEY_V = Sdl.SDLK_v;
    public static int KEY_W = Sdl.SDLK_w;
    public static int KEY_X = Sdl.SDLK_x;
    public static int KEY_Y = Sdl.SDLK_y;
    public static int KEY_Z = Sdl.SDLK_z;
    public static int KEY_1 = Sdl.SDLK_1;
    public static int KEY_2 = Sdl.SDLK_2;
    public static int KEY_3 = Sdl.SDLK_3;
    public static int KEY_4 = Sdl.SDLK_4;
    public static int KEY_5 = Sdl.SDLK_5;
    public static int KEY_6 = Sdl.SDLK_6;
    public static int KEY_7 = Sdl.SDLK_7;
    public static int KEY_8 = Sdl.SDLK_8;
    public static int KEY_9 = Sdl.SDLK_9;
    public static int KEY_0 = Sdl.SDLK_0;
    public static int KEY_UP = Sdl.SDLK_UP;
    public static int KEY_DOWN = Sdl.SDLK_DOWN;
    public static int KEY_RIGHT = Sdl.SDLK_RIGHT;
    public static int KEY_LEFT = Sdl.SDLK_LEFT;
    public static int KEY_RETURN = Sdl.SDLK_RETURN;
    public static int KEY_DEL = Sdl.SDLK_DELETE;

    public static int DetectKey()
    {
        if (KeyPressed(KEY_ESC))
        {
            return KEY_ESC;
        }
        else if (KeyPressed(KEY_SPC))
        {
            return KEY_SPC;
        }
        else if (KeyPressed(KEY_DEL))
        {
            return KEY_DEL;
        }
        else if (KeyPressed(KEY_A))
        {
            return KEY_A;
        }
        else if (KeyPressed(KEY_B))
        {
            return KEY_B;
        }
        else if (KeyPressed(KEY_C))
        {
            return KEY_C;
        }
        else if (KeyPressed(KEY_D))
        {
            return KEY_D;
        }
        else if (KeyPressed(KEY_E))
        {
            return KEY_E;
        }
        else if (KeyPressed(KEY_F))
        {
            return KEY_F;
        }
        else if (KeyPressed(KEY_G))
        {
            return KEY_G;
        }
        else if (KeyPressed(KEY_H))
        {
            return KEY_H;
        }
        else if (KeyPressed(KEY_I))
        {
            return KEY_I;
        }
        else if (KeyPressed(KEY_J))
        {
            return KEY_J;
        }
        else if (KeyPressed(KEY_K))
        {
            return KEY_K;
        }
        else if (KeyPressed(KEY_L))
        {
            return KEY_L;
        }
        else if (KeyPressed(KEY_M))
        {
            return KEY_M;
        }
        else if (KeyPressed(KEY_N))
        {
            return KEY_N;
        }
        else if (KeyPressed(KEY_O))
        {
            return KEY_O;
        }
        else if (KeyPressed(KEY_P))
        {
            return KEY_P;
        }
        else if (KeyPressed(KEY_Q))
        {
            return KEY_Q;
        }
        else if (KeyPressed(KEY_R))
        {
            return KEY_R;
        }
        else if (KeyPressed(KEY_S))
        {
            return KEY_S;
        }
        else if (KeyPressed(KEY_T))
        {
            return KEY_T;
        }
        else if (KeyPressed(KEY_U))
        {
            return KEY_U;
        }
        else if (KeyPressed(KEY_V))
        {
            return KEY_V;
        }
        else if (KeyPressed(KEY_W))
        {
            return KEY_W;
        }
        else if (KeyPressed(KEY_X))
        {
            return KEY_X;
        }
        else if (KeyPressed(KEY_Y))
        {
            return KEY_Y;
        }
        else if (KeyPressed(KEY_Z))
        {
            return KEY_Z;
        }
        else if (KeyPressed(KEY_1))
        {
            return KEY_1;
        }
        else if (KeyPressed(KEY_2))
        {
            return KEY_2;
        }
        else if (KeyPressed(KEY_3))
        {
            return KEY_3;
        }
        else if (KeyPressed(KEY_4))
        {
            return KEY_4;
        }
        else if (KeyPressed(KEY_5))
        {
            return KEY_5;
        }
        else if (KeyPressed(KEY_6))
        {
            return KEY_6;
        }
        else if (KeyPressed(KEY_7))
        {
            return KEY_7;
        }
        else if (KeyPressed(KEY_8))
        {
            return KEY_8;
        }
        else if (KeyPressed(KEY_9))
        {
            return KEY_9;
        }
        else if (KeyPressed(KEY_0))
        {
            return KEY_0;
        }
        else if (KeyPressed(KEY_UP))
        {
            return KEY_UP;
        }
        else if (KeyPressed(KEY_DOWN))
        {
            return KEY_DOWN;
        }
        else if (KeyPressed(KEY_RIGHT))
        {
            return KEY_RIGHT;
        }
        else if (KeyPressed(KEY_LEFT))
        {
            return KEY_LEFT;
        }
        else if (KeyPressed(KEY_RETURN))
        {
            return KEY_RETURN;
        }
        else
        {
            return -1;
        }
    }

    public static string KeyToString(int keyCode)
    {
        if (keyCode == KEY_ESC)
        {
            return "ESC";
        }
        else if (keyCode == KEY_SPC)
        {
            return "SPC";
        }
        else if (keyCode == KEY_DEL)
        {
            return "DEL";
        }
        else if (keyCode == KEY_A)
        {
            return "A";
        }
        else if (keyCode == KEY_B)
        {
            return "B";
        }
        else if (keyCode == KEY_C)
        {
            return "C";
        }
        else if (keyCode == KEY_D)
        {
            return "D";
        }
        else if (keyCode == KEY_E)
        {
            return "E";
        }
        else if (keyCode == KEY_F)
        {
            return "F";
        }
        else if (keyCode == KEY_G)
        {
            return "G";
        }
        else if (keyCode == KEY_H)
        {
            return "H";
        }
        else if (keyCode == KEY_I)
        {
            return "I";
        }
        else if (keyCode == KEY_J)
        {
            return "J";
        }
        else if (keyCode == KEY_K)
        {
            return "K";
        }
        else if (keyCode == KEY_L)
        {
            return "L";
        }
        else if (keyCode == KEY_M)
        {
            return "M";
        }
        else if (keyCode == KEY_N)
        {
            return "N";
        }
        else if (keyCode == KEY_O)
        {
            return "O";
        }
        else if (keyCode == KEY_P)
        {
            return "P";
        }
        else if (keyCode == KEY_Q)
        {
            return "Q";
        }
        else if (keyCode == KEY_R)
        {
            return "R";
        }
        else if (keyCode == KEY_S)
        {
            return "S";
        }
        else if (keyCode == KEY_T)
        {
            return "T";
        }
        else if (keyCode == KEY_U)
        {
            return "U";
        }
        else if (keyCode == KEY_V)
        {
            return "V";
        }
        else if (keyCode == KEY_W)
        {
            return "W";
        }
        else if (keyCode == KEY_X)
        {
            return "X";
        }
        else if (keyCode == KEY_Y)
        {
            return "Y";
        }
        else if (keyCode == KEY_Z)
        {
            return "Z";
        }
        else if (keyCode == KEY_1)
        {
            return "1";
        }
        else if (keyCode == KEY_2)
        {
            return "2";
        }
        else if (keyCode == KEY_3)
        {
            return "3";
        }
        else if (keyCode == KEY_4)
        {
            return "4";
        }
        else if (keyCode == KEY_5)
        {
            return "5";
        }
        else if (keyCode == KEY_6)
        {
            return "6";
        }
        else if (keyCode == KEY_7)
        {
            return "7";
        }
        else if (keyCode == KEY_8)
        {
            return "8";
        }
        else if (keyCode == KEY_9)
        {
            return "9";
        }
        else if (keyCode == KEY_0)
        {
            return "0";
        }
        else if (keyCode == KEY_UP)
        {
            return "UP";
        }
        else if (keyCode == KEY_DOWN)
        {
            return "DOWN";
        }
        else if (keyCode == KEY_RIGHT)
        {
            return "RIGHT";
        }
        else if (keyCode == KEY_LEFT)
        {
            return "LEFT";
        }
        else if (keyCode == KEY_RETURN)
        {
            return "RETURN";
        }
        else
        {
            return "";
        }
    }
}

