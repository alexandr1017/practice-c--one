
using NoteApp;
using System.Reflection.Metadata;

namespace WinFormsApp1

{
    internal static class MainOfMainForm
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize(); // ����� ����� ��������� �����

            MainForm mainForm = new MainForm();

            Project project = ManagerProject.loadProjectFromJsonFile();
            mainForm.SetProject(project);

            Application.Run(mainForm); // ������ ������� �����

            //Note noteOne = new Note("�������1", TypeNoteEnum.Home, "������ ������", DateTime.Now, DateTime.Now);
            //Note noteTwo = new Note("�������2", TypeNoteEnum.Work, "��������� ������ 1",DateTime.Now, DateTime.Now);


        }
    }
}