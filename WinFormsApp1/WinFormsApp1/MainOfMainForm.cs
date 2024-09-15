
using System;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace NoteApp
{
    static class MainOfMainForm
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            

            Project project = ManagerProject.loadProjectFromJsonFile();
           
            MainForm mainForm = new MainForm(project);
            
            Application.Run(mainForm);


        }
    }
}