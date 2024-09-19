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
        private Label creationDateLabel;
        private Label updateDateLabel;
        private Button okButton;
        private Button cancelButton;
        private TableLayoutPanel mainTableLayout;

        public EditNoteForm(Note note = null)
        {
            InitializeComponent();

            if (note != null)
            {
                nameTextBox.Text = note.getName();
                typeComboBox.SelectedItem = note.getTypeOfNote();
                textTextBox.Text = note.getTextOfNote();
                creationDateLabel.Text = $"Дата создания: {note.getDateTimeCreate():dd.MM.yyyy HH:mm}";
                updateDateLabel.Text = $"Дата изменения: {note.getDateTimeUpdate():dd.MM.yyyy HH:mm}";
                Note = note;
            }
            else
            {
                Note = new Note("Без названия", TypeNoteEnum.Other, "", DateTime.Now, DateTime.Now);
                creationDateLabel.Text = $"Дата создания: {Note.getDateTimeCreate():dd.MM.yyyy HH:mm}";
                updateDateLabel.Text = $"Дата изменения: {Note.getDateTimeUpdate():dd.MM.yyyy HH:mm}";
            }
        }

        private void InitializeComponent()
        {
            this.Text = "Edit Note";
            this.MinimumSize = new Size(500, 400); // Устанавливаем минимальный размер окна
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Создаем TableLayoutPanel для размещения полей и меток
            mainTableLayout = new TableLayoutPanel();
            mainTableLayout.Dock = DockStyle.Fill; // Растягиваем TableLayoutPanel на все окно
            mainTableLayout.RowCount = 6;
            mainTableLayout.ColumnCount = 2;
            mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F)); // 25% для меток
            mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F)); // 75% для полей ввода
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // Название
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // Тип
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // Дата создания
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // Дата изменения
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Метка "Текст"
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Поле для текста (растягивается)
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Кнопки OK и Cancel

            // Поле ввода названия
            Label nameLabel = new Label() { Text = "Название:", Anchor = AnchorStyles.Left, AutoSize = true, Padding = new Padding(0, 0, 10, 0) };
            nameTextBox = new TextBox() { Anchor = AnchorStyles.Left | AnchorStyles.Right };
            mainTableLayout.Controls.Add(nameLabel, 0, 0);
            mainTableLayout.Controls.Add(nameTextBox, 1, 0);

            // Выпадающий список для типа заметки
            Label typeLabel = new Label() { Text = "Тип:", Anchor = AnchorStyles.Left, AutoSize = true, Padding = new Padding(0, 0, 10, 0) };
            typeComboBox = new ComboBox() { Anchor = AnchorStyles.Left | AnchorStyles.Right };
            typeComboBox.Items.AddRange(Enum.GetNames(typeof(TypeNoteEnum)));
            typeComboBox.SelectedIndex = 0;
            mainTableLayout.Controls.Add(typeLabel, 0, 1);
            mainTableLayout.Controls.Add(typeComboBox, 1, 1);

            // Создаем панель для меток даты создания и изменения
            TableLayoutPanel dateLayout = new TableLayoutPanel();
            dateLayout.ColumnCount = 2;
            dateLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // Создано
            dateLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // Изменено
            dateLayout.Dock = DockStyle.Fill;

            // Метка для даты создания
            creationDateLabel = new Label() { Text = "Дата создания: ", Anchor = AnchorStyles.Left, AutoSize = true };
            dateLayout.Controls.Add(creationDateLabel, 0, 0);

            // Метка для даты изменения
            updateDateLabel = new Label() { Text = "Дата изменения: ", Anchor = AnchorStyles.Left, AutoSize = true };
            dateLayout.Controls.Add(updateDateLabel, 1, 0);

            // Добавляем панель для даты в основную таблицу
            mainTableLayout.Controls.Add(dateLayout, 0, 2);
            mainTableLayout.SetColumnSpan(dateLayout, 2); // Растягиваем на обе колонки

            // Метка для поля текста заметки
            Label textLabel = new Label() { Text = "Текст:", Anchor = AnchorStyles.Left, AutoSize = true };
            mainTableLayout.Controls.Add(textLabel, 0, 4);
            mainTableLayout.SetColumnSpan(textLabel, 2); // Метка должна занимать обе колонки

            // Поле для текста заметки (адаптируемое под размер окна)
            textTextBox = new TextBox() { Multiline = true, Dock = DockStyle.Fill, ScrollBars = ScrollBars.Vertical };
            mainTableLayout.Controls.Add(textTextBox, 0, 5);
            mainTableLayout.SetColumnSpan(textTextBox, 2); // Поле для текста должно занимать обе колонки

            // Панель для кнопок OK и Cancel
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonPanel.Dock = DockStyle.Fill;

            // Кнопка Cancel (теперь будет первой)
            cancelButton = new Button() { Text = "Cancel" };
            cancelButton.Click += (sender, e) => this.DialogResult = DialogResult.Cancel;
            buttonPanel.Controls.Add(cancelButton);

            // Кнопка OK (теперь будет справа)
            okButton = new Button() { Text = "OK" };
            okButton.Click += OkButton_Click;
            buttonPanel.Controls.Add(okButton);

            // Добавляем панель с кнопками в TableLayoutPanel
            mainTableLayout.Controls.Add(buttonPanel, 0, 6);
            mainTableLayout.SetColumnSpan(buttonPanel, 2); // Кнопки должны растягиваться на обе колонки

            // Добавляем TableLayoutPanel в форму
            this.Controls.Add(mainTableLayout);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            // Проверка на корректность данных
            if (nameTextBox.Text.Length > 50)
            {
                MessageBox.Show("Название должно быть не более 50 символов");
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
    

