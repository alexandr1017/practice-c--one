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
        private Label noteDetailsLabel;
        private Button addNoteButton;
        private Button editNoteButton;
        private Button removeNoteButton;
        private MenuStrip menuStrip;
        private SplitContainer splitContainer;
        private TableLayoutPanel tableLayoutPanel;

        public MainForm(Project project)
        {
            this.project = project;
            InitializeComponent();
            InitializeMenu();
            this.FormClosing += MainForm_FormClosing;
        }

        private void InitializeComponent()
        {
            this.Text = "NoteApp";
            this.MinimumSize = new Size(600, 400); // Устанавливаем минимальный размер окна
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Создаем SplitContainer для разделения окна на две части (список заметок и детальная информация)
            splitContainer = new SplitContainer();
            splitContainer.Dock = DockStyle.Fill; // Растягиваем SplitContainer на все окно
            splitContainer.Orientation = Orientation.Vertical; // Вертикальное разделение
            splitContainer.SplitterDistance = 50; // Размер панели для списка заметок

            // Создаем TableLayoutPanel для размещения кнопок в нижней части окна
            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Bottom; // Закрепляем панель кнопок внизу
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F)); // Равномерное распределение кнопок
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.Height = 50; // Высота панели кнопок

            // ListBox для списка заметок
            notesListBox = new ListBox();
            notesListBox.Dock = DockStyle.Fill; // Растягиваем ListBox по всей панели
            notesListBox.SelectedIndexChanged += NotesListBox_SelectedIndexChanged;
            splitContainer.Panel1.Controls.Add(notesListBox); // Добавляем ListBox в левую панель SplitContainer

            // Метка для отображения выбранной заметки
            noteDetailsLabel = new Label();
            noteDetailsLabel.Dock = DockStyle.Fill; // Растягиваем метку по всей правой панели
            noteDetailsLabel.BorderStyle = BorderStyle.FixedSingle;
            noteDetailsLabel.Padding = new Padding(10); // Добавляем отступы
            splitContainer.Panel2.Controls.Add(noteDetailsLabel); // Добавляем метку в правую панель SplitContainer

            // Кнопка "Добавить заметку"
            addNoteButton = new Button();
            addNoteButton.Text = "Add Note";
            addNoteButton.Dock = DockStyle.Fill; // Растягиваем кнопку по ячейке
            addNoteButton.Click += AddNoteButton_Click;
            tableLayoutPanel.Controls.Add(addNoteButton, 0, 0); // Добавляем кнопку в первую колонку

            // Кнопка "Редактировать заметку"
            editNoteButton = new Button();
            editNoteButton.Text = "Edit Note";
            editNoteButton.Dock = DockStyle.Fill;
            editNoteButton.Click += EditNoteButton_Click;
            tableLayoutPanel.Controls.Add(editNoteButton, 1, 0); // Добавляем кнопку во вторую колонку

            // Кнопка "Удалить заметку"
            removeNoteButton = new Button();
            removeNoteButton.Text = "Remove Note";
            removeNoteButton.Dock = DockStyle.Fill;
            removeNoteButton.Click += RemoveNoteButton_Click;
            tableLayoutPanel.Controls.Add(removeNoteButton, 2, 0); // Добавляем кнопку в третью колонку

            // Добавляем SplitContainer и TableLayoutPanel в форму
            this.Controls.Add(splitContainer);
            this.Controls.Add(tableLayoutPanel);

            LoadNotes();
        }

        // Загрузка заметок из проекта в ListBox
        private void LoadNotes()
        {
            notesListBox.Items.Clear();
            foreach (var note in this.project.getNotesList())
            {
                notesListBox.Items.Add(note.getName());
            }

            // Устанавливаем первую заметку как выбранную, если есть хотя бы одна заметка
            if (notesListBox.Items.Count > 0)
            {
                notesListBox.SelectedIndex = 0;
            }
        }

        // Обработка выбора заметки
        private void NotesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = notesListBox.SelectedIndex;
            if (index >= 0)
            {
                Note selectedNote = project.getNotesList()[index];
                noteDetailsLabel.Text = $"Название: {selectedNote.getName()}\n" +
                                        $"Тип: {selectedNote.getTypeOfNote()}\n" +
                                        $"Текст: {selectedNote.getTextOfNote()}\n" +
                                        $"Дата создания: {selectedNote.getDateTimeCreate()}\n" +
                                        $"Дата изменения: {selectedNote.getDateTimeUpdate()}";
            }
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
            int index = notesListBox.SelectedIndex;
            if (index >= 0)
            {
                Note selectedNote = project.getNotesList()[index];
                EditNoteForm editForm = new EditNoteForm(selectedNote);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    project.updateNote(editForm.Note);
                    LoadNotes();
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

        public void SetProject(Project project)
        {
            this.project = project;
            LoadNotes();
        }
    }
}
