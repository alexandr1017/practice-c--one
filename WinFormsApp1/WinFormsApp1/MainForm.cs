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

        public MainForm(Project project)
        {   
            this.project = project;
            InitializeComponent();
            this.FormClosing += MainForm_FormClosing;
        }

        private void InitializeComponent()
        {
            this.Text = "Главное окно программы";
            this.Size = new Size(800, 600);

            // ListBox для списка заметок
            notesListBox = new ListBox();
            notesListBox.Location = new Point(10, 10);
            notesListBox.Size = new Size(300, 500);
            notesListBox.SelectedIndexChanged += NotesListBox_SelectedIndexChanged;

            // Метка для отображения выбранной заметки
            noteDetailsLabel = new Label();
            noteDetailsLabel.Location = new Point(320, 10);
            noteDetailsLabel.Size = new Size(450, 500);
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
        //сохранение проекта с заметками в файл  при выходе из программы
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
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
