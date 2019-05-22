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
    protected bool nameChosen;

    public SaveMenu(Player player)
    {
        playerToSave = player;
        name = "";
        SLOTS = new Slot[MAX_SLOTS];
        nameChosen = false;

        for (int i = 0; i < MAX_SLOTS; i++)
        {
            SLOTS[i].name = "";
            SLOTS[i].fileName = "";
        }
    }

    public void ChooseName()
    {
        //DateTime pressTic, drawTic;
        //TimeSpan diff;
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
        else if (key == SdlHardware.KEY_SPC && name.Length > 0)
        {
            name = name.Remove(name.Length - 1);
        }
    }

    public void ChooseSlot()
    {
        int selectedSlotKey = SdlHardware.DetectKey();
        int selectedSlot = 0;
        if (selectedSlotKey == SdlHardware.KEY_1)
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

    public override void DrawMenu()
    {
        Console.WriteLine("slot name: " + SLOTS[0].name);
        SdlHardware.ClearScreen();
        SdlHardware.WriteHiddenText("1. " + SLOTS[0].name == "" ? 
            "Empty" : SLOTS[0].name,
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
        SdlHardware.WriteHiddenText("Name: " + name,
            100, 100,
            0xC0, 0xC0, 0xC0,
            font);
        if(nameChosen)
            SdlHardware.WriteHiddenText("Choose slot: " + name,
                100, 120,
                0xC0, 0xC0, 0xC0,
                font);
        SdlHardware.ShowHiddenScreen();

        if(nameChosen)
            ChooseName();
        ChooseSlot();
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
            SdlHardware.Pause(100); // Works but I'm not sure why
        }
        while (!SdlHardware.KeyPressed(Controls.Cancel));
        Save(SLOTS[0]);
    }
}

