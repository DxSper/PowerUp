using System;
using System.Diagnostics;
using System.Security.Principal;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            while (!IsUserAnAdmin())
            {
                // Demander les droits d'administrateur
                var processInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = Process.GetCurrentProcess().MainModule.FileName,
                    Verb = "runas"
                };
                try
                {
                    Process.Start(processInfo);
                }
                catch (Exception)
                {
                    Console.WriteLine("Impossible de lancer le programme en tant qu'administrateur. Veuillez réessayer.");
                }
            }

            // Le programme est lancé en tant qu'administrateur, on peut continuer ici...
        }

        // Fonction pour vérifier si l'utilisateur est un administrateur
        static bool IsUserAnAdmin()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
