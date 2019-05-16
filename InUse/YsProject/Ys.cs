/*
17 - 04 - 2019, Diego Lezcano: Create basic game structure and Main menu

06 - 05 - 2019, Diego Lezcano: Create classes Controls and ChangeControlsMenu,
    detect if a suitable key has been pressed

13 - 05 - 2019, Diego Lezcano: class diagram, created de Menu class and made MainMenu 
	and ChangeConstrolsMenu inherit from it
*/
class Ys
{
    static void Main(string[] args)
    {
        bool fullScreen = false;
        SdlHardware.Init(640, 400, 24, fullScreen);
        Font font;
        font = new Font("data/Joystix.ttf", 12);

        MainMenu main = new MainMenu ();
        Controls control = new Controls();

        do
        {
            main.Run();
            

            if(main.GetChosenOption() == 1)
            {
                Game ys = new Game();
            }
            else if (main.GetChosenOption() == 2)
            {
                SdlHardware.ClearScreen();
                SdlHardware.WriteHiddenText("Work in progress...",
                   100, 200,
                   0xC0, 0xC0, 0xC0,
                   font);
                SdlHardware.ShowHiddenScreen();
                SdlHardware.Pause(2000);
            }
            else if(main.GetChosenOption() == 3)
            {
                ChangeControlsMenu change = new ChangeControlsMenu(control);
                change.Run();
                control = change.GetNewControls();
            }
        } while (main.GetChosenOption() != 4);

    }
}

