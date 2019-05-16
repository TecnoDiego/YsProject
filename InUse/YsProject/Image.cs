using System;
using Tao.Sdl;


class Image
{
    private IntPtr internalPointer;

    public Image(string fileName)  // Constructor
    {
        Load(fileName);
    }

    public void Load(string fileName)
    {
        internalPointer = SdlImage.IMG_Load(fileName);
        if (internalPointer == IntPtr.Zero)
            SdlHardware.FatalError("Image not found: " + fileName);
    }


    public IntPtr GetPointer()
    {
        return internalPointer;
    }
}
