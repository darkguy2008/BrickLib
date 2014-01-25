using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace BrickLib.WinAPI
{
    public enum EWallpaperChangeEffect
    {
        None = 1,
        FadeAero = 2
    }

    public static class Wrapper
    {
        public static void EnableActiveDesktop()
        {
            IntPtr result = IntPtr.Zero;
            Windows.SendMessageTimeout(Windows.FindWindow("Progman", null), 0x52c, IntPtr.Zero, IntPtr.Zero, 0, 500, out result);
        }

        public static Windows.IActiveDesktop GetActiveDesktop()
        {
            Type typeActiveDesktop = Type.GetTypeFromCLSID(Windows.CLSID_ActiveDesktop);
            return (Windows.IActiveDesktop)Activator.CreateInstance(typeActiveDesktop);
        }

        public static void SetWallpaper(String localPath, EWallpaperChangeEffect eEffect)
        {
            if (eEffect == EWallpaperChangeEffect.FadeAero)
            {
                EnableActiveDesktop();
                ThreadStart threadStarter = () =>
                {
                    Windows.IActiveDesktop _activeDesktop = GetActiveDesktop();
                    _activeDesktop.SetWallpaper(localPath, 0);
                    _activeDesktop.ApplyChanges(Windows.AD_Apply.ALL | Windows.AD_Apply.FORCE);
                    Marshal.ReleaseComObject(_activeDesktop);
                };
                Thread thread = new Thread(threadStarter);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join(2000);
            }
            else
            {
                Windows.SystemParametersInfo(Windows.SPI_SETDESKWALLPAPER, 0, localPath, Windows.SPIF_UPDATEINIFILE | Windows.SPIF_SENDWININICHANGE);
            }
        }
    }
}
