using System;
using System.Threading.Tasks;

namespace DesktopToastNotification
{
    class Program
    {
        static async Task Main(string[] args)
        {
            NewToastNotification Window = new NewToastNotification("VTC RD Notification", "This is notify from VTC ITS Center. Thank you for watching. http://vtc.org.vn/", 3);

            await Task.Delay(5000);
        }
    }
}
