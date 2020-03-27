using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	class Program
	{


		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		[DllImport("user32.dll")]
		static extern IntPtr GetShellWindow();

		[DllImport("user32.dll")]
		static extern IntPtr GetDesktopWindow();


		static NotifyIcon notifyIcon;
		static IntPtr processHandle;
		static IntPtr WinShell;
		static IntPtr WinDesktop;
		static MenuItem HideMenu;
		static MenuItem RestoreMenu;

		static void Main(string[] args)
		{
			notifyIcon = new NotifyIcon();
			notifyIcon.Icon = new Icon("TestICON.ico");
			notifyIcon.Text = "Monitor";
			notifyIcon.Visible = true;

			ContextMenu menu = new ContextMenu();
			HideMenu = new MenuItem("Hide", new EventHandler(Minimize_Click));
			RestoreMenu = new MenuItem("Restore", new EventHandler(Maximize_Click));

			menu.MenuItems.Add(RestoreMenu);
			menu.MenuItems.Add(HideMenu);
			menu.MenuItems.Add(new MenuItem("Exit", new EventHandler(CleanExit)));

			notifyIcon.ContextMenu = menu;

			//You need to spin off your actual work in a different thread so that the Notify Icon works correctly
			Task.Factory.StartNew(Run);

			processHandle = Process.GetCurrentProcess().MainWindowHandle;

			WinShell = GetShellWindow();

			WinDesktop = GetDesktopWindow();

			//Hide the Window
			ResizeWindow(false);

			///This is required for triggering WinForms activity in Console app
			Application.Run();


		}

		static void Run()
		{
			Console.WriteLine("Listening to messages");

			while (true)
			{
				System.Threading.Thread.Sleep(1000);
			}
		}


		private static void CleanExit(object sender, EventArgs e)
		{
			notifyIcon.Visible = false;
			Application.Exit();
			Environment.Exit(1);
		}


		static void Minimize_Click(object sender, EventArgs e)
		{
			ResizeWindow(false);
		}


		static void Maximize_Click(object sender, EventArgs e)
		{
			ResizeWindow();
		}

		static void ResizeWindow(bool Restore = true)
		{
			if (Restore)
			{
				RestoreMenu.Enabled = false;
				HideMenu.Enabled = true;
				SetParent(processHandle, WinDesktop);
			}
			else
			{
				RestoreMenu.Enabled = true;
				HideMenu.Enabled = false;
				SetParent(processHandle, WinShell);
			}
		}
	}
}
