/*
17 - 04 - 2019, Create basic game structure and Main menu

06 - 05 - 2019, Create classes Controls and ChangeControlsMenu,
    detect if a suitable key has been pressed

13 - 05 - 2019, class diagram, created de Menu class and made MainMenu 
	and ChangeConstrolsMenu inherit from it

15 - 05 - 2019, implemented the DetectKey method wich returns te key code of the pressed key.
    Implemented the KeyInUse method from the Controls class

16 - 05 - 2019, Implemented SwapKey and KeyToString functions. 
	Started the new game option and SaveMenu class.

17 - 05 - 2019, mplemented a way for the player to introduce a name for the save file.

20 - 05 - 2019, Implemented partialy a way to save the state of the game with files

22 - 05 - 2019, Finished the ChangeControlsMenu (now it works) and started to implement the
	ChooseSlot method

23 - 05 - 2019, Upgraded the ChangeControlsMenu and SaveMenu classes
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
        Controls c = new Controls();
        Controls.LoadControls();

        do
        {
            main.Run();
            

            if(main.GetChosenOption() == 1)
            {
                SdlHardware.Pause(20);
                SaveMenu newGame = new SaveMenu(new Player("data/adol.png"), true);
                newGame.Run();
                Game ys = new Game();
                ys.Run();
                SdlHardware.Pause(100);
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
                ChangeControlsMenu change = new ChangeControlsMenu();
                change.Run();
                SdlHardware.Pause(100);
            }
        } while (main.GetChosenOption() != 4);
        Controls.SaveControls();
    }
}

