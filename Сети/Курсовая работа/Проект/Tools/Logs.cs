using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Course.Utils;

namespace Course.Tools
{
    public partial class Logs : Form
    {
        public Logs()
        {
            InitializeComponent();
        }

        private void Init()
        {
            Logger l = Logger.Instance;
            logsTextBox.Text = l.Get();
        }

        #region events

        private void Logs_Load(object sender, EventArgs e)
        {
            Init();
        }
        #endregion
    }
}
