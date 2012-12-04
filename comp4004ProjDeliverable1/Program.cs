using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace comp4004ProjDeliverable1
{
    using Model;
    using Views;

    class Program
    {
        private static Controller controller = new Controller();
     
        [STAThread]
        static void Main()
        {
            /*ConsoleView view = new ConsoleView();

            view.Render();
            DbMethods.getInstance().clearTables();*/

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI());
            
        }
    }
}
