using System;
using System.IO;
public struct Slot
{
    public string name;
    public string fileName;
}

class SaveMenu : Menu
{
   
    public static Slot[] SLOTS;
    public static int MAX_SLOTS = 3;
    protected int selectedSlot;
    protected string name;
    protected Player playerToSave;
    protected bool nameChosen;
    protected string showSlot;
    protected bool newGame;

    public SaveMenu(Player player, bool newGame)
    {
        playerToSave = player;

        if (newGame)
            name = "";
        else
            name = player.GetName();

        showSlot = "";
        this.newGame = newGame;
        selectedSlot = 0;
        nameChosen = false;

        SLOTS = new Slot[MAX_SLOTS];

        if (File.Exists("slots.txt"))
            LoadSlotsInfo();
        else
        {
            for (int i = 0; i < MAX_SLOTS; i++)
            {
                SLOTS[i].name = "Empty";
                SLOTS[i].fileName = "Empty" + i + ".txt";
            }
        }

    }

    public void ChooseName()
    {
        //DateTime pressTic, drawTic;
        //TimeSpan diff;
        //pressTic = DateTime.Now;
        //drawTic = DateTime.Now;
        //diff = drawTic - pressTic;
        int key = SdlHardware.DetectKey();
        if (key != SdlHardware.KEY_SPC && key != SdlHardware.KEY_ESC &&
            key != SdlHardware.KEY_UP && key != SdlHardware.KEY_DOWN &&
            key != SdlHardware.KEY_LEFT && key != SdlHardware.KEY_RIGHT &&
            key != SdlHardware.KEY_RETURN /*&& ((int) diff.TotalMilliseconds > 500)*/)
        {
            name += SdlHardware.KeyToString(key);
        }
        else if (key == SdlHardware.KEY_SPC && name.Length > 0)
        {
            name = name.Remove(name.Length - 1);
        }
        else if (key == SdlHardware.KEY_RETURN)
            nameChosen = true;
        SdlHardware.Pause(105);
    }

    public int ChooseSlot()
    {
        int selectedSlotKey = SdlHardware.DetectKey();
        if (selectedSlotKey == SdlHardware.KEY_1)
        {
            selectedSlot = 1;
            showSlot = "1";
        }
        if (selectedSlotKey == SdlHardware.KEY_2)
        {
            selectedSlot = 2;
            showSlot = "2";
        }
        if (selectedSlotKey == SdlHardware.KEY_3)
        {
            selectedSlot = 3;
            showSlot = "3";
        }
        if(selectedSlot != 0)
        {
            playerToSave.SetName(name);
            SLOTS[selectedSlot - 1].name = name;
            SLOTS[selectedSlot - 1].fileName = "slot1" + name + ".txt";
            SaveSlotsInfo();
        }
        
        return selectedSlot;
    }

    public override void DrawMenu()
    {
        SdlHardware.ClearScreen();
        SdlHardware.WriteHiddenText("1. " + SLOTS[0].name,
            100, 20,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("2. " + SLOTS[1].name,
            100, 40,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("3. " + SLOTS[2].name,
            100, 60,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Name: " + name,
            100, 100,
            0xC0, 0xC0, 0xC0,
            font);
        if(nameChosen || !newGame)
            SdlHardware.WriteHiddenText("Choose slot: " + showSlot,
                100, 120,
                0xC0, 0xC0, 0xC0,
                font);
        SdlHardware.WriteHiddenText("Note: SPACEBAR to delete",
            100, 200,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.ShowHiddenScreen();
    }

    private void SaveSlotsInfo()
    {
        try
        {
            StreamWriter slotsFile = new StreamWriter("slots.txt");

            foreach (Slot s in SLOTS)
            {
                slotsFile.WriteLine(s.name + ";" + s.fileName);
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

    private void LoadSlotsInfo()
    {
        try
        {
            StreamReader slotsFile = new StreamReader("slots.txt");
            for(int i = 0;i < MAX_SLOTS; i++)
            {
                string[] slotInfo = slotsFile.ReadLine().Split(';');
                SLOTS[i].name = slotInfo[0];
                SLOTS[i].fileName = slotInfo[1];
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

    public void Save(Slot slot)
    {
        try
        {
            StreamWriter saveFile = new StreamWriter(slot.fileName);
            playerToSave.SetName(name);
            saveFile.WriteLine("YsSaveFile");
            saveFile.WriteLine(playerToSave.GetName());
            saveFile.WriteLine(playerToSave.GetGold());
            saveFile.WriteLine(playerToSave.GetExp());
            saveFile.WriteLine(playerToSave.GetLvl());
            saveFile.WriteLine(playerToSave.GetX());
            saveFile.WriteLine(playerToSave.GetY());
            saveFile.Close();
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
        do
        {
            DrawMenu();
            if (newGame)
            {
                do
                {
                    DrawMenu();
                    if (!nameChosen) // when newGame == false there is no need to ask for a name
                        ChooseName();
                } while (!SdlHardware.KeyPressed(Controls.Accept));
            }
            
            if (nameChosen || !newGame)
            {
                do
                {


                    do
                    {
                        selectedSlot = ChooseSlot();
                        DrawMenu();
                        if (selectedSlot != 0)
                            SdlHardware.Pause(200);
                    } while (selectedSlot == 0);
                    
                } while (!SdlHardware.KeyPressed(Controls.Accept));
            }
        } while (!SdlHardware.KeyPressed(Controls.Cancel) &&  selectedSlot == 0);
        Save(SLOTS[selectedSlot - 1]);
    }
}

