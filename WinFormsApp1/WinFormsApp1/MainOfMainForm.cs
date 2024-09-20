
using System;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace NoteApp
{
    /// <summary>
    /// ������� �����, ���������� ����� ����� � ���������� NoteApp.
    /// </summary>
    static class MainOfMainForm
    {
        /// <summary>
        /// �������� �����, � �������� ���������� ���������� ����������.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // ��������� ���������� ������ ��� ���������� (������������ ����������� ����� Windows).
            Application.EnableVisualStyles();

            // ��������� ������������� ��� ���������� ������.
            Application.SetCompatibleTextRenderingDefault(false);

            // �������� ������� �� ����� JSON � ������� ManagerProject � ������������� ������ ������ loadProjectFromJsonFile().
            Project project = ManagerProject.loadProjectFromJsonFile();

            // �������� ������� ����� ���������� � �������� � ��� ������������ Project.
            MainForm mainForm = new MainForm(project);

            // ������ ������� ����� � ������ ������ ����������.
            Application.Run(mainForm);
        }
    }
}