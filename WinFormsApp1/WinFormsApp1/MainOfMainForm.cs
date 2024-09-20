
using System;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace NoteApp
{
    /// <summary>
    /// Главный класс, содержащий точку входа в приложение NoteApp.
    /// </summary>
    static class MainOfMainForm
    {
        /// <summary>
        /// Основной метод, с которого начинается выполнение приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Включение визуальных стилей для приложения (соответствие современным темам Windows).
            Application.EnableVisualStyles();

            // Установка совместимости для рендеринга текста.
            Application.SetCompatibleTextRenderingDefault(false);

            // Загрузка проекта из файла JSON с помощью ManagerProject и статаического метода класса loadProjectFromJsonFile().
            Project project = ManagerProject.loadProjectFromJsonFile();

            // Создание главной формы приложения и передача в нее загруженного Project.
            MainForm mainForm = new MainForm(project);

            // Запуск главной формы и начало работы приложения.
            Application.Run(mainForm);
        }
    }
}