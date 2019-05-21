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
    protected string name;
    protected Player playerToSave;
    public SaveMenu(Player player)
    {
        playerToSave = player;
        name = "";
        SLOTS = new Slot[MAX_SLOTS];

        for (int i = 0; i < MAX_SLOTS; i++)
        {
            SLOTS[i].name = "";
            SLOTS[i].fileName = "";
        }
    }

    public override void DrawMenu()
    {
        //DateTime pressTic, drawTic;
        //TimeSpan diff;
        SdlHardware.ClearScreen();
        SdlHardware.WriteHiddenText("1. "/* + SLOTS[0].name == "" ? 
            "Empty" : SLOTS[0].name*/,
            100, 20,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("2. "/* + SLOTS[1].name == "" ? 
            "Empty" : SLOTS[1].name*/,
            100, 40,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("3. "/* + SLOTS[2].name == "" ? 
            "Empty" : SLOTS[2].name*/,
            100, 60,
            0xC0, 0xC0, 0xC0,
            font);

        //pressTic = DateTime.Now;
        int key = SdlHardware.DetectKey();
        //drawTic = DateTime.Now;
        //diff = drawTic - pressTic;
        if (key != SdlHardware.KEY_SPC && key != SdlHardware.KEY_ESC &&
            key != SdlHardware.KEY_UP && key != SdlHardware.KEY_DOWN &&
            key != SdlHardware.KEY_LEFT && key != SdlHardware.KEY_RIGHT &&
            key != SdlHardware.KEY_RETURN /*&& ((int) diff.TotalMilliseconds > 500)*/)
        {
            name += SdlHardware.KeyToString(key);
        }
        else if(key == SdlHardware.KEY_SPC && name.Length > 0)
        {
            name = name.Remove(name.Length - 1);
        }

        SdlHardware.WriteHiddenText("Name: " + name,
            100, 100,
            0xC0, 0xC0, 0xC0,
            font);
        /*
        SdlHardware.WriteHiddenText("Choose slot: " + name,
            100, 120,
            0xC0, 0xC0, 0xC0,
            font);*/
        SdlHardware.ShowHiddenScreen();

        int selectedSlotKey = SdlHardware.DetectKey();
        int selectedSlot = 0;
        if(selectedSlotKey == SdlHardware.KEY_1)
        {
            selectedSlot = 1;
        }
        if (selectedSlotKey == SdlHardware.KEY_2)
        {
            selectedSlot = 2;
        }
        if (selectedSlotKey == SdlHardware.KEY_3)
        {
            selectedSlot = 3;
        }
        playerToSave.SetName(name);
        SLOTS[0].name = name;
        SLOTS[0].fileName = "slot1_" + name + ".txt";
        
    }

    public void Run()
    {
        while (!SdlHardware.KeyPressed(Controls.Cancel))
        {
            DrawMenu();
            SdlHardware.Pause(100); // Works but I'm not sure why
        }
        Save(SLOTS[0]);
    }

    public int GetSlots()
    {
        return MAX_SLOTS;
    }

    public void Save(Slot slot)
    {
        try
        {
            StreamWriter saveFile = new StreamWriter(slot.fileName);
            saveFile.WriteLine("YsSaveFile");
            saveFile.WriteLine(playerToSave.GetName());
            saveFile.WriteLine(playerToSave.GetGold());
            saveFile.WriteLine(playerToSave.GetExp());
            saveFile.WriteLine(playerToSave.GetLvl());
            saveFile.Close();
        }
        catch (PathTooLongException)
        {
            Console.WriteLine("Path too long");
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
}

