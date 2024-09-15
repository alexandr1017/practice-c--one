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

        public MainForm(Project project)
        {   
            this.project = project;
            InitializeComponent();
            InitializeMenu(); // Добавляем меню
            this.FormClosing += MainForm_FormClosing;
        }

        private void InitializeComponent()
        {
            this.Text = "NoteApp";
            this.Size = new Size(800, 600);

            // ListBox для списка заметок
            notesListBox = new ListBox();
            notesListBox.Location = new Point(10, 35);
            notesListBox.Size = new Size(300, 484);
            notesListBox.SelectedIndexChanged += NotesListBox_SelectedIndexChanged;

            // Метка для отображения выбранной заметки
            noteDetailsLabel = new Label();
            noteDetailsLabel.Location = new Point(320, 35);
            noteDetailsLabel.Size = new Size(450, 480);
            noteDetailsLabel.AutoSize = false;
            noteDetailsLabel.BorderStyle = BorderStyle.FixedSingle;

            // Кнопка "Добавить заметку"
            addNoteButton = new Button();
            addNoteButton.Text = "Add Note";
            addNoteButton.Location = new Point(10, 520);
            addNoteButton.Click += AddNoteButton_Click;

            // Кнопка "Редактировать заметку"
            editNoteButton = new Button();
            editNoteButton.Text = "Edit Note";
            editNoteButton.Location = new Point(100, 520);
            editNoteButton.Click += EditNoteButton_Click;

            // Кнопка "Удалить заметку"
            removeNoteButton = new Button();
            removeNoteButton.Text = "Remove Note";
            removeNoteButton.Location = new Point(200, 520);
            removeNoteButton.Click += RemoveNoteButton_Click;

            // Добавляем компоненты в форму
            this.Controls.Add(notesListBox);
            this.Controls.Add(noteDetailsLabel);
            this.Controls.Add(addNoteButton);
            this.Controls.Add(editNoteButton);
            this.Controls.Add(removeNoteButton);

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
            // Создаем MenuStrip
            menuStrip = new MenuStrip();

            // Создаем меню File
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Exit", null, ExitMenuItem_Click);
            exitMenuItem.ShortcutKeys = Keys.Alt | Keys.F4; // Устанавливаем горячие клавиши Alt+F4
            fileMenu.DropDownItems.Add(exitMenuItem);

            // Создаем меню Edit
            ToolStripMenuItem editMenu = new ToolStripMenuItem("Edit");
            ToolStripMenuItem addNoteMenuItem = new ToolStripMenuItem("Add Note", null, AddNoteMenuItem_Click);
            ToolStripMenuItem editNoteMenuItem = new ToolStripMenuItem("Edit Note", null, EditNoteMenuItem_Click);
            ToolStripMenuItem removeNoteMenuItem = new ToolStripMenuItem("Remove Note", null, RemoveNoteMenuItem_Click);
            editMenu.DropDownItems.AddRange(new ToolStripItem[] { addNoteMenuItem, editNoteMenuItem, removeNoteMenuItem });

            // Создаем меню Help
            ToolStripMenuItem helpMenu = new ToolStripMenuItem("Help");
            ToolStripMenuItem aboutMenuItem = new ToolStripMenuItem("About", null, AboutMenuItem_Click);
            aboutMenuItem.ShortcutKeys = Keys.F1; // Горячая клавиша F1 для вызова окна "О программе"
            helpMenu.DropDownItems.Add(aboutMenuItem);

            // Добавляем все меню в MenuStrip
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(editMenu);
            menuStrip.Items.Add(helpMenu);

            // Устанавливаем MenuStrip в форму
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
