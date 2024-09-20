using NoteAppUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    public partial class MainForm : Form
    {
        private Project project;
        private ListBox notesListBox;
        private Label noteTitleLabel;
        private Label noteTypeLabel;
        private Label noteTypeCategoryLabel;
        private FlowLayoutPanel noteTypePanel;
        private ComboBox noteTypeComboBox;
        private Panel noteDetailsPanel;  // Панель с прокруткой
        private Label noteUpdateDateLabel; // Добавляем две новые метки для даты создания и даты изменения
        private Label noteCreationDateLabel;
        private Label noteDetailsLabel;
        private Button addNoteButton;
        private Button editNoteButton;
        private Button removeNoteButton;
        private MenuStrip menuStrip;
        private SplitContainer splitContainer;
        private TableLayoutPanel tableLayoutPanel;

        private List<Note> filteredNotes;

        public MainForm(Project project)
        {
            this.project = project;
            InitializeComponent();
            InitializeMenu();
            this.FormClosing += MainForm_FormClosing;
        }

        private void InitializeComponent()
        {
            Text = "NoteApp";
            MinimumSize = new Size(600, 400);
            Size = new Size(1280, 720);
            StartPosition = FormStartPosition.CenterScreen;

            // Создаем SplitContainer для разделения окна на две части (список заметок и детальная информация)
            splitContainer = new SplitContainer();
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Orientation = Orientation.Vertical;
            splitContainer.SplitterDistance = 50;

            // Создаем TableLayoutPanel для размещения кнопок в нижней части окна
            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Bottom;
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.Height = 50;

            // ComboBox для выбора типа заметок
            noteTypeComboBox = new ComboBox();
            noteTypeComboBox.Dock = DockStyle.Top;
            noteTypeComboBox.Items.Add("All");
            noteTypeComboBox.Items.AddRange(Enum.GetNames(typeof(TypeNoteEnum)));
            noteTypeComboBox.SelectedIndex = 0;
            noteTypeComboBox.SelectedIndexChanged += NoteTypeComboBox_SelectedIndexChanged;

            // ListBox для списка заметок
            notesListBox = new ListBox();
            notesListBox.Dock = DockStyle.Fill;
            notesListBox.SelectedIndexChanged += NotesListBox_SelectedIndexChanged;
            notesListBox.MouseDoubleClick += NotesListBox_MouseDoubleClick;
            notesListBox.KeyDown += NotesListBox_KeyDown;
            splitContainer.Panel1.Controls.Add(notesListBox);
            splitContainer.Panel1.Controls.Add(noteTypeComboBox);

            // Label для отображения названия заметки
            noteTitleLabel = new Label();
            noteTitleLabel.Dock = DockStyle.Top;
            noteTitleLabel.AutoSize = true;
            noteTitleLabel.Font = new Font(noteTitleLabel.Font.FontFamily, 16, FontStyle.Bold);
            noteTitleLabel.Padding = new Padding(10);
            noteTitleLabel.Height = 50;

            // Панель для размещения "Категория заметки:" и типа заметки в одной строке
            noteTypePanel = new FlowLayoutPanel();
            noteTypePanel.Dock = DockStyle.Top;
            noteTypePanel.Height = 30;
            noteTypePanel.Padding = new Padding(10);
            noteTypePanel.AutoSize = true;
            noteTypePanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            noteTypePanel.FlowDirection = FlowDirection.LeftToRight;

            // Label для категории типа заметки
            noteTypeCategoryLabel = new Label();
            noteTypeCategoryLabel.Text = "Категория заметки:";
            noteTypeCategoryLabel.AutoSize = true;
            noteTypeCategoryLabel.Font = new Font(noteTypeCategoryLabel.Font.FontFamily, 12, FontStyle.Regular);
            noteTypeCategoryLabel.TextAlign = ContentAlignment.MiddleLeft;

            // Label для типа заметки
            noteTypeLabel = new Label();
            noteTypeLabel.AutoSize = true;
            noteTypeLabel.Font = new Font(noteTypeLabel.Font.FontFamily, 12, FontStyle.Italic);
            noteTypeLabel.TextAlign = ContentAlignment.MiddleLeft;
            noteTypeLabel.Margin = new Padding(5, 0, 0, 0);

            // Добавляем подпись и тип заметки на одну панель
            noteTypePanel.Controls.Add(noteTypeCategoryLabel);
            noteTypePanel.Controls.Add(noteTypeLabel);

            // Добавляем метку для даты создания
            noteCreationDateLabel = new Label();
            noteCreationDateLabel.AutoSize = true;
            noteCreationDateLabel.Font = new Font(noteCreationDateLabel.Font.FontFamily, 12, FontStyle.Regular);
            noteCreationDateLabel.TextAlign = ContentAlignment.MiddleLeft;
            noteCreationDateLabel.Margin = new Padding(10, 0, 0, 0);  // Добавляем отступы для форматирования

            // Добавляем метку для даты изменения
            noteUpdateDateLabel = new Label();
            noteUpdateDateLabel.AutoSize = true;
            noteUpdateDateLabel.Font = new Font(noteUpdateDateLabel.Font.FontFamily, 12, FontStyle.Regular);
            noteUpdateDateLabel.TextAlign = ContentAlignment.MiddleLeft;
            noteUpdateDateLabel.Margin = new Padding(10, 0, 0, 0);  // Добавляем отступы для форматирования

            // Добавляем новые метки в noteTypePanel после меток категории и типа заметки
            noteTypePanel.Controls.Add(noteCreationDateLabel);
            noteTypePanel.Controls.Add(noteUpdateDateLabel);

            // Панель с прокруткой для отображения детальной информации
            noteDetailsPanel = new Panel();
            noteDetailsPanel.Dock = DockStyle.Fill;
            noteDetailsPanel.AutoScroll = true;
            noteDetailsPanel.BorderStyle = BorderStyle.FixedSingle;

            // Метка для отображения детальной информации о заметке
            noteDetailsLabel = new Label();
            noteDetailsLabel.AutoSize = true;
            noteDetailsLabel.Padding = new Padding(10);

            // Добавляем метку с информацией в панель с прокруткой
            noteDetailsPanel.Controls.Add(noteDetailsLabel);

            // Добавляем все метки и панель в правую панель SplitContainer
            splitContainer.Panel2.Controls.Add(noteDetailsPanel); // Панель с прокруткой
            splitContainer.Panel2.Controls.Add(noteTypePanel); // Панель с категорией и типом заметки
            splitContainer.Panel2.Controls.Add(noteTitleLabel);

            // Кнопка "Добавить заметку"
            addNoteButton = new Button();
            addNoteButton.Text = "Add Note";
            addNoteButton.Dock = DockStyle.Fill;
            addNoteButton.Click += AddNoteButton_Click;
            tableLayoutPanel.Controls.Add(addNoteButton, 0, 0);

            // Кнопка "Редактировать заметку"
            editNoteButton = new Button();
            editNoteButton.Text = "Edit Note";
            editNoteButton.Dock = DockStyle.Fill;
            editNoteButton.Click += EditNoteButton_Click;
            tableLayoutPanel.Controls.Add(editNoteButton, 1, 0);

            // Кнопка "Удалить заметку"
            removeNoteButton = new Button();
            removeNoteButton.Text = "Remove Note";
            removeNoteButton.Dock = DockStyle.Fill;
            removeNoteButton.Click += RemoveNoteButton_Click;
            tableLayoutPanel.Controls.Add(removeNoteButton, 2, 0);

            // Добавляем SplitContainer и TableLayoutPanel в форму
            Controls.Add(splitContainer);
            Controls.Add(tableLayoutPanel);

            LoadNotes();
        }

        // Загрузка заметок из проекта в ListBox с фильтрацией по типу
        private void LoadNotes()
        {
            notesListBox.Items.Clear();
            filteredNotes = new List<Note>();
            string selectedType = noteTypeComboBox.SelectedItem.ToString();

            foreach (var note in this.project.getNotesList())
            {
                if (selectedType == "All" || Enum.GetName(note.getTypeOfNote()) == selectedType)
                {
                    notesListBox.Items.Add(note.getName());
                    filteredNotes.Add(note);
                }
            }

            if (notesListBox.Items.Count > 0)
            {
                notesListBox.SelectedIndex = 0;
            }
            else
            {
                ClearNoteDetails();
            }
        }

        // Обработка изменения выбранного типа заметки
        private void NoteTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadNotes(); // Перезагружаем список заметок при изменении фильтра
        }

        // Обработка выбора заметки
        private void NotesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = notesListBox.SelectedIndex;

            if (index >= 0 && filteredNotes != null && index < filteredNotes.Count)
            {
                Note selectedNote = filteredNotes[index];

                // Устанавливаем название заметки
                noteTitleLabel.Text = selectedNote.getName();

                // Устанавливаем тип заметки
                noteTypeLabel.Text = Enum.GetName(typeof(TypeNoteEnum), selectedNote.getTypeOfNote());

                // Устанавливаем даты создания и изменения
                noteCreationDateLabel.Text = $"Дата создания: {selectedNote.getDateTimeCreate()}";
                noteUpdateDateLabel.Text = $"Дата изменения: {selectedNote.getDateTimeUpdate()}";

                // Устанавливаем детальную информацию о заметке
                noteDetailsLabel.Text = $"Текст: {selectedNote.getTextOfNote()}";
            }
            else
            {
                ClearNoteDetails();
            }
        }

        private void ClearNoteDetails()
        {
            noteTitleLabel.Text = string.Empty;
            noteTypeLabel.Text = string.Empty;
            noteCreationDateLabel.Text = string.Empty;
            noteUpdateDateLabel.Text = string.Empty;
            noteDetailsLabel.Text = string.Empty;
        }

        // Добавление новой заметки
        private void AddNoteButton_Click(object sender, EventArgs e)
        {
            EditNoteForm editForm = new EditNoteForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                project.addNote(editForm.Note);
                LoadNotes();
            }
        }

        // Редактирование выбранной заметки
        private void EditNoteButton_Click(object sender, EventArgs e)
        {
            int index = notesListBox.SelectedIndex;  // Получаем индекс выбранной заметки в ListBox
            if (index >= 0 && filteredNotes != null && index < filteredNotes.Count)
            {
                Note selectedNote = filteredNotes[index];  // Берем заметку из фильтрованного списка, а не из исходного
                EditNoteForm editForm = new EditNoteForm(selectedNote);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    // Обновляем заметку в оригинальном списке проекта
                    project.updateNote(editForm.Note);
                    LoadNotes();  // Перезагружаем список заметок
                }
            }
        }

        // Удаление выбранной заметки
        private void RemoveNoteButton_Click(object sender, EventArgs e)
        {
            int index = notesListBox.SelectedIndex;
            if (index >= 0)
            {
                var result = MessageBox.Show($"Do you really want to remove this note: {project.getNotesList()[index].getName()}?",
                                             "Confirmation", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    project.removeNoteOfNotesList(project.getNotesList()[index]);
                    LoadNotes();
                }
            }
        }


        private void InitializeMenu()
        {
            menuStrip = new MenuStrip();

            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Exit", null, ExitMenuItem_Click);
            exitMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
            fileMenu.DropDownItems.Add(exitMenuItem);

            ToolStripMenuItem editMenu = new ToolStripMenuItem("Edit");
            ToolStripMenuItem addNoteMenuItem = new ToolStripMenuItem("Add Note", null, AddNoteMenuItem_Click);
            ToolStripMenuItem editNoteMenuItem = new ToolStripMenuItem("Edit Note", null, EditNoteMenuItem_Click);
            ToolStripMenuItem removeNoteMenuItem = new ToolStripMenuItem("Remove Note", null, RemoveNoteMenuItem_Click);
            editMenu.DropDownItems.AddRange(new ToolStripItem[] { addNoteMenuItem, editNoteMenuItem, removeNoteMenuItem });

            ToolStripMenuItem helpMenu = new ToolStripMenuItem("Help");
            ToolStripMenuItem aboutMenuItem = new ToolStripMenuItem("About", null, AboutMenuItem_Click);
            aboutMenuItem.ShortcutKeys = Keys.F1;
            helpMenu.DropDownItems.Add(aboutMenuItem);

            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(editMenu);
            menuStrip.Items.Add(helpMenu);

            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
        }

        // Обработчик выхода из приложения
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрыть приложение
        }

        // Обработчик создания новой заметки
        private void AddNoteMenuItem_Click(object sender, EventArgs e)
        {
            AddNoteButton_Click(sender, e); // Вызов метода для создания новой заметки
        }

        // Обработчик редактирования заметки
        private void EditNoteMenuItem_Click(object sender, EventArgs e)
        {
            EditNoteButton_Click(sender, e); // Вызов метода для редактирования заметки
        }

        // Обработчик удаления заметки
        private void RemoveNoteMenuItem_Click(object sender, EventArgs e)
        {
            RemoveNoteButton_Click(sender, e); // Вызов метода для удаления заметки
        }

        // Обработчик вызова окна "О программе"
        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog(); // Открыть окно "О программе"
        }

        // Обработчик закрытия формы (сохранение данных)
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Сохранение данных проекта перед выходом
            ManagerProject.saveProjectToJsonFile(project);
            MessageBox.Show("Проект сохранен успешно.");
        }

        // Обработчик двойного клика мышью на заметке
        private void NotesListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditSelectedNote();
        }

        // Обработчик нажатия клавиши в ListBox
        private void NotesListBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверяем, нажата ли клавиша Enter
            if (e.KeyCode == Keys.Enter)
            {
                EditSelectedNote();
            }
        }

        // Метод для редактирования выбранной заметки
        private void EditSelectedNote()
        {
            int index = notesListBox.SelectedIndex;  // Получаем индекс выбранной заметки в ListBox
            if (index >= 0 && filteredNotes != null && index < filteredNotes.Count)
            {
                Note selectedNote = filteredNotes[index];  // Берем заметку из фильтрованного списка
                EditNoteForm editForm = new EditNoteForm(selectedNote);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    // Обновляем заметку в оригинальном списке проекта
                    project.updateNote(editForm.Note);
                    LoadNotes();  // Перезагружаем список заметок
                }
            }
        }

        public void SetProject(Project project)
        {
            this.project = project;
            LoadNotes();
        }
    }
}
