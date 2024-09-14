
using NoteApp;
using System.Reflection.Metadata;

namespace WinFormsApp1

{
    internal static class MainOfMainForm
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize(); // Вызов перед созданием формы

            MainForm mainForm = new MainForm();

            Project project = ManagerProject.loadProjectFromJsonFile();
            mainForm.SetProject(project);

            Application.Run(mainForm); // Запуск главной формы

            //Note noteOne = new Note("Заметка1", TypeNoteEnum.Home, "Купить молока", DateTime.Now, DateTime.Now);
            //Note noteTwo = new Note("Заметка2", TypeNoteEnum.Work, "Выполнить задачу 1",DateTime.Now, DateTime.Now);


        }
    }
}