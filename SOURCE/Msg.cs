using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Project1
{
    public class Msg
    {
        public static void Mensagem()
        {
            var msg1 = MessageBox.Show("ATTENTION!!\r\nDo you really wish to run this software?\r\nIt is potentially dangerous and destructive.\r\n\r\nIT CAN CAUSE IRREVERSIBLE DAMAGE TO YOUR COMPUTER AND I (CYBERWARE) AM NOT RESPONSIBLE FOR ANYTHING!!",
                "ARE YOU SURE??",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (msg1 == DialogResult.No)
            {
                CloseApplication();
            }
            else
            {
                var msg2 = MessageBox.Show("THIS IS THE LAST WARNING, DO YOU REALLY WANT TO RUN THIS MALWARE?",
                    "LAST WARNING!!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (msg2 == DialogResult.No)
                {
                    CloseApplication();
                }
            }
        }

        public static void CloseApplication()
        {
            string exeName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            var processes = Process.GetProcessesByName(exeName);
            foreach (var process in processes)
            {
                process.Kill();
            }
        }
    }
}
