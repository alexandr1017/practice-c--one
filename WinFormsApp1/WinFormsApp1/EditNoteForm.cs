using NoteApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteAppUI
{
    public partial class EditNoteForm : Form
    {
        public Note Note { get; private set; }

        private TextBox nameTextBox;
        private ComboBox typeComboBox;
        private TextBox textTextBox;
        private Button okButton;
        private Button cancelButton;

        public EditNoteForm(Note note = null)
        {
            InitializeComponent();

            if (note != null)
            {
                nameTextBox.Text = note.getName();
                typeComboBox.SelectedItem = note.getTypeOfNote();
                textTextBox.Text = note.getTextOfNote();
                Note = note;
            }
            else
            {
                Note = new Note("Без названия", TypeNoteEnum.Other, "", DateTime.Now, DateTime.Now);
            }
        }

        private void InitializeComponent()
        {
            this.Text = "Edit Note";
            this.Size = new Size(400, 300);

            // Поле ввода названия
            Label nameLabel = new Label() { Text = "Название", Location = new Point(10, 10) };
            nameTextBox = new TextBox() { Location = new Point(120, 10), Width = 250 };

            // Выпадающий список для типа заметки
            Label typeLabel = new Label() { Text = "Тип", Location = new Point(10, 50) };
            typeComboBox = new ComboBox() { Location = new Point(120, 50), Width = 250 };
            typeComboBox.Items.AddRange(Enum.GetNames(typeof(TypeNoteEnum)));
            typeComboBox.SelectedIndex = 0;

            // Поле для текста заметки
            Label textLabel = new Label() { Text = "Текст", Location = new Point(10, 90) };
            textTextBox = new TextBox() { Location = new Point(120, 90), Width = 250, Height = 100, Multiline = true };

            // Кнопка OK
            okButton = new Button() { Text = "OK", Location = new Point(100, 200) };
            okButton.Click += OkButton_Click;

            // Кнопка Cancel
            cancelButton = new Button() { Text = "Cancel", Location = new Point(200, 200) };
            cancelButton.Click += (sender, e) => this.DialogResult = DialogResult.Cancel;

            // Добавляем элементы на форму
            this.Controls.Add(nameLabel);
            this.Controls.Add(nameTextBox);
            this.Controls.Add(typeLabel);
            this.Controls.Add(typeComboBox);
            this.Controls.Add(textLabel);
            this.Controls.Add(textTextBox);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            // Проверка на корректность данных
            if (string.IsNullOrEmpty(nameTextBox.Text) || nameTextBox.Text.Length > 50)
            {
                MessageBox.Show("Название должно быть непустым и не более 50 символов");
                return;
            }

            // Обновляем или создаем заметку
            Note.setName(nameTextBox.Text);
            Note.setTypeOfNote((TypeNoteEnum)Enum.Parse(typeof(TypeNoteEnum), typeComboBox.SelectedItem.ToString()));
            Note.setTextOfNote(textTextBox.Text);
            Note.setDateTimeUpdate(DateTime.Now);

            this.DialogResult = DialogResult.OK;
        }
    }
}
