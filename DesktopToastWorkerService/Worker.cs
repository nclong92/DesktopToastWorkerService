using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace DesktopToastWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Start {DateTime.Now}");

                try
                {
                    Process.Start(@"C:\Users\IsaoTakashi\Desktop\DesktopToastWorkerService.bat");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception {ex.Message} \n StackTrace {ex.StackTrace}");
                }
                
                await Task.Delay(10000, stoppingToken);
            }
        }

        private void LaunchApp()
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            //psi.FileName = "C:\\Users\\IsaoTakashi\\Desktop\\DesktopToastWorkerService_Publish1\\DesktopToastWorkerService.exe";
            psi.FileName = @"C:\Windows\system32\cmd.exe";
            //psi.Arguments = "sample.txt";
            //psi.WorkingDirectory = "c:\\mywork";
            psi.WindowStyle = ProcessWindowStyle.Maximized;
            Process p = Process.Start(psi);
        }

        private void EditFile()
        {
            string[] lines = File.ReadAllLines("test.txt");
            List<string> newLines = new List<string>();
            foreach(var line in lines)
            {
                _logger.LogInformation($"EditFile {line}");
            }
        }
    }
}
