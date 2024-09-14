using NoteApp;

namespace WinFormsApp1
{
    partial class MainForm
    {
       
        private System.ComponentModel.IContainer components = null;
        private Project project;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

      
        private void InitializeComponent()
        {

            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";

            
            // Создаем метку (Label)
            Label label = new Label();
            label.Location = new Point(100, 100); // Позиция метки на форме
            label.AutoSize = true; // Автоматическое изменение размера метки в зависимости от длины текста

            Button showInfoButton = new Button();
            showInfoButton.Text = "Show all Info";
            showInfoButton.Location = new Point(150,150);
            //showInfoButton.Click += (sender, e) => ShowInfo(label);

            showInfoButton.Click += (sender, e) => ShowNotes(label);

            // Добавляем метку на форму
            this.Controls.Add(label);
            this.Controls.Add(showInfoButton);

        }
        private void ShowInfo(Label label)
        {
           
            // Выводим информацию в метку
            label.Text = "Hellow";
        }

        public void SetProject(Project project) { 
        this.project = project;
        }

        private void ShowNotes(Label label) {
            // Очистим текст метки
            label.Text = "";

            // Проходим по каждому заметке в проекте
            foreach (Note note in this.project.getNotesList())
            {
                // Добавляем информацию о заметке в текст метки
                label.Text += $"Название: {note.getName()}\n" +
                              $"Тип: {note.getTypeOfNote()}\n" +
                              $"Текст: {note.getTextOfNote()}\n" +
                              $"Дата создания: {note.getDateTimeCreate()}\n" +
                              $"Дата изменения: {note.getDateTimeUpdate()}\n\n";
            }
        }

     

        #endregion
    }
}
