using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ShutDownComputer
{
    public partial class Form1 : Form
    {
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        static extern public int system(string command);
        public Form1()
        {
            InitializeComponent();
        }
        private void btnExec_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            var diff = dateTimePicker2.Value.TimeOfDay - dateTime.TimeOfDay;
            diff += dateTimePicker1.Value.Date - dateTime.Date;
            if (diff.TotalSeconds < 60)
            {
                MessageBox.Show("Вы не можете установить таймер\nменьше чем на 1 минуту!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var a = system($"shutdown -s -t {(int)diff.TotalSeconds}");
                if (a != 0)
                    MessageBox.Show("Таймер уже установлен\nОтмените его, если не устанавливали его раньше!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show($"Таймер установлен, тамер будет выключен в {dateTimePicker1.Value.Date.Add(dateTimePicker2.Value.TimeOfDay)}", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            var a = system("shutdown -a");
            if (a != 0)
                MessageBox.Show("Таймер отсутствует", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("Таймер успешно отменен", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copyright © tr1ckshxt \"Shut down computer timer\" 2022 All rights reserved","", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
