namespace WinFormsApp1
{
    partial class Form1
    {
       
        private System.ComponentModel.IContainer components = null;

        
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
            showInfoButton.Text = "Show Person Info";
            showInfoButton.Location = new Point(150,150);
            showInfoButton.Click += (sender, e) => ShowPersonInfo(label);

            // Добавляем метку на форму
            this.Controls.Add(label);
            this.Controls.Add(showInfoButton);

        }
        private void ShowPersonInfo(Label label)
        {
            // Создаем объект Person
            Person person = new Person(30, "Alex");

            // Выводим информацию в метку
            label.Text = person.printInfo();
        }

        #endregion
    }
}
