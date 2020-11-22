/*
 *	Developed By: Aebian
 *	Name: PassengerKI
 *	Dependent: Rage Plugin Hook
 *	Released On: GitHub
 */

namespace PassengerKI.Utils
{
    using System;
    using System.Net;
    using System.Net.Http;

    internal static class Updater
    {
        private static readonly HttpClient hc = new HttpClient();

        public static int CheckUpdate()
        {
            string response = "";

            try
            {

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Logger.Log("Fetching latest plugin version from Repo");
                response = hc.GetStringAsync(new Uri("https://adm.knight-industries.org/updates/PassengerKI/latestVersion.txt")).Result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());

                return -2;
                
            }

            Global.Application.LatestVersion = response;


            Version CurrentVN = new Version(Global.Application.CurrentVersion);
            Version LatestVN = new Version(Global.Application.LatestVersion);

            if (CurrentVN.CompareTo(LatestVN) > 0)
            {
                return 1;
            }
            else if (CurrentVN.CompareTo(LatestVN) < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
