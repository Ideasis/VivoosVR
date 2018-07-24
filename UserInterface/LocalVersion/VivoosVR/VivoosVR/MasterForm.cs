using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

namespace VivoosVR
{
    public partial class MasterForm : Form
    {
        protected static Assembly assembly = Assembly.Load("VivoosVR");
        protected static ResourceManager resourceManager = new ResourceManager("VivoosVR.lang.langres", assembly);

        public MasterForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            GlobalVariables.FindSecondMonitor();
            PlaceSelfOnSecondMonitor();
        }
        protected void PlaceSelfOnSecondMonitor()
        {
            if (GlobalVariables.secondaryScreen != null)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = GlobalVariables.secondaryScreen.WorkingArea.Location;
            }
        }
    }
}
