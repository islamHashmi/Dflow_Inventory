using Dflow_Inventory.ContentPage;
using System;
using System.Windows.Forms;

namespace Dflow_Inventory
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new VoucherReceipt());
        }
    }
}
