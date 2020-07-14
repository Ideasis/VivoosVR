using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivoosVR.Properties;
using System.Globalization;
using System.Windows.Forms;

namespace VivoosVR
{
    static class GlobalVariables
    {
        public static Guid Edit_ID;
        public static Guid Session_ID;
        public static Guid Command_ID;
        public static string Session_ID_name;
        public static Guid Session_Data_ID;
        public static string Session_Data_name;
        public static string Session_Data_date;
        public static Guid Asset_Start_ID;
        public static string Asset_Start_name;
        public static Guid Command_Start_ID;
        //public static Guid Default_Start_ID;
        public static Guid LoginID;
        public static bool Is_Edit;
        public static bool Is_Session;
        public static bool Is_Stop=false;
        public static int Patients_Search_Flag;
        public static int Sessions_Search_Flag;
        public static int NewSessions_Search_Flag;
        public static System.Diagnostics.Process neulogProcess;
        public static System.Diagnostics.Process sessionProcess;
        public static CultureInfo uiLanguage = new CultureInfo("tr-TR");

        public static Screen secondaryScreen;

        public static string processURL;


        //Called on MasterForm Initializer
        public static void FindSecondMonitor()
        {
            if (Screen.AllScreens.Length > 1)
            {
                secondaryScreen = Screen.AllScreens.OrderBy(s => s.Bounds.Left).Last();
            }
        }
    }
}
