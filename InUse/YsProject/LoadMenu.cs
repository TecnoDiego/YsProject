using System;
using System.IO;

namespace YsProject
{
    class LoadMenu : Menu
    {
        protected int loadSlot;
        public LoadMenu()
        {
            loadSlot = 0;
        }

        public override void DrawMenu()
        {
            SdlHardware.ClearScreen();
            SdlHardware.WriteHiddenText("1. " + SaveMenu.SLOTS[0].name,
                100, 20,
                0xC0, 0xC0, 0xC0,
                font);
            SdlHardware.WriteHiddenText("2. " + SaveMenu.SLOTS[1].name,
                100, 40,
                0xC0, 0xC0, 0xC0,
                font);
            SdlHardware.WriteHiddenText("3. " + SaveMenu.SLOTS[2].name,
                100, 60,
                0xC0, 0xC0, 0xC0,
                font);
            SdlHardware.WriteHiddenText("Choose slot: ",
                100, 120,
                0xC0, 0xC0, 0xC0,
                font);
            SdlHardware.ShowHiddenScreen();
        }

        private void LoadSlotsInfo()
        {
            try
            {
                StreamReader slotsFile = new StreamReader("slots.txt");
                for (int i = 0; i < SaveMenu.MAX_SLOTS; i++)
                {
                    string[] slotInfo = slotsFile.ReadLine().Split(';');
                    SaveMenu.SLOTS[i].name = slotInfo[0];
                    SaveMenu.SLOTS[i].fileName = slotInfo[1];
                }
                slotsFile.Close();
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("Path too long");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
            catch (IOException io)
            {
                Console.WriteLine("Read/Write error: " + io.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void Run()
        {

        }
    }
}
