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

    public static int DetectKey()
    {
        if (SdlHardware.KeyPressed(SdlHardware.KEY_ESC))
        {
            return SdlHardware.KEY_ESC;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_SPC))
        {
            return SdlHardware.KEY_SPC;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_A))
        {
            return SdlHardware.KEY_A;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_B))
        {
            return SdlHardware.KEY_B;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_C))
        {
            return SdlHardware.KEY_C;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_D))
        {
            return SdlHardware.KEY_D;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_E))
        {
            return SdlHardware.KEY_E;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_F))
        {
            return SdlHardware.KEY_F;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_G))
        {
            return SdlHardware.KEY_G;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_H))
        {
            return SdlHardware.KEY_H;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_I))
        {
            return SdlHardware.KEY_I;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_J))
        {
            return SdlHardware.KEY_J;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_K))
        {
            return SdlHardware.KEY_K;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_L))
        {
            return SdlHardware.KEY_L;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_M))
        {
            return SdlHardware.KEY_M;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_N))
        {
            return SdlHardware.KEY_N;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_O))
        {
            return SdlHardware.KEY_O;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_P))
        {
            return SdlHardware.KEY_P;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_Q))
        {
            return SdlHardware.KEY_Q;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_R))
        {
            return SdlHardware.KEY_R;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_S))
        {
            return SdlHardware.KEY_S;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_T))
        {
            return SdlHardware.KEY_T;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_U))
        {
            return SdlHardware.KEY_U;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_V))
        {
            return SdlHardware.KEY_V;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_W))
        {
            return SdlHardware.KEY_W;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_X))
        {
            return SdlHardware.KEY_X;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_Y))
        {
            return SdlHardware.KEY_Y;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_Z))
        {
            return SdlHardware.KEY_Z;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_1))
        {
            return SdlHardware.KEY_1;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_2))
        {
            return SdlHardware.KEY_2;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_3))
        {
            return SdlHardware.KEY_3;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_4))
        {
            return SdlHardware.KEY_4;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_5))
        {
            return SdlHardware.KEY_5;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_6))
        {
            return SdlHardware.KEY_6;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_7))
        {
            return SdlHardware.KEY_7;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_8))
        {
            return SdlHardware.KEY_8;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_9))
        {
            return SdlHardware.KEY_9;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_0))
        {
            return SdlHardware.KEY_0;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_UP))
        {
            return SdlHardware.KEY_UP;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_DOWN))
        {
            return SdlHardware.KEY_DOWN;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_RIGHT))
        {
            return SdlHardware.KEY_RIGHT;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_LEFT))
        {
            return SdlHardware.KEY_LEFT;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_RETURN))
        {
            return SdlHardware.KEY_RETURN;
        }
        else
        {
            return -1;
        }
    }
}

