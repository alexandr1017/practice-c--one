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
        private Label noteTitleLabel; // Новый Label для названия заметки
        private Label noteTypeLabel; // Новый Label для типа заметки
        private ComboBox noteTypeComboBox;
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
            splitContainer = new SplitContainer();
            notesListBox = new ListBox();
            noteTypeComboBox = new ComboBox();
            noteDetailsLabel = new Label();
            noteTypeLabel = new Label();
            noteTitleLabel = new Label();
            tableLayoutPanel = new TableLayoutPanel();
            addNoteButton = new Button();
            editNoteButton = new Button();
            removeNoteButton = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 0);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(notesListBox);
            splitContainer.Panel1.Controls.Add(noteTypeComboBox);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(noteDetailsLabel);
            splitContainer.Panel2.Controls.Add(noteTypeLabel);
            splitContainer.Panel2.Controls.Add(noteTitleLabel);
            splitContainer.Size = new Size(784, 511);
            splitContainer.SplitterDistance = 261;
            splitContainer.TabIndex = 0;
            // 
            // notesListBox
            // 
            notesListBox.Dock = DockStyle.Fill;
            notesListBox.ItemHeight = 15;
            notesListBox.Location = new Point(0, 23);
            notesListBox.Name = "notesListBox";
            notesListBox.Size = new Size(261, 488);
            notesListBox.TabIndex = 0;
            notesListBox.SelectedIndexChanged += NotesListBox_SelectedIndexChanged;
            // 
            // noteTypeComboBox
            // 
            noteTypeComboBox.Dock = DockStyle.Top;
            noteTypeComboBox.Items.AddRange(new object[] { "All", "Work", "Home", "HealthAndSport", "Peaople", "Documents", "Finance", "Other" });
            noteTypeComboBox.Location = new Point(0, 0);
            noteTypeComboBox.Name = "noteTypeComboBox";
            noteTypeComboBox.Size = new Size(261, 23);
            noteTypeComboBox.TabIndex = 1;
            noteTypeComboBox.SelectedIndexChanged += NoteTypeComboBox_SelectedIndexChanged;
            // 
            // noteDetailsLabel
            // 
            noteDetailsLabel.BorderStyle = BorderStyle.FixedSingle;
            noteDetailsLabel.Dock = DockStyle.Fill;
            noteDetailsLabel.Location = new Point(0, 91);
            noteDetailsLabel.Name = "noteDetailsLabel";
            noteDetailsLabel.Padding = new Padding(10);
            noteDetailsLabel.Size = new Size(519, 420);
            noteDetailsLabel.TabIndex = 0;
            // 
            // noteTypeLabel
            // 
            noteTypeLabel.AutoSize = true;
            noteTypeLabel.Dock = DockStyle.Top;
            noteTypeLabel.Font = new Font("Segoe UI", 12F, FontStyle.Italic);
            noteTypeLabel.Location = new Point(0, 50);
            noteTypeLabel.Name = "noteTypeLabel";
            noteTypeLabel.Padding = new Padding(10);
            noteTypeLabel.Size = new Size(20, 41);
            noteTypeLabel.TabIndex = 1;
            // 
            // noteTitleLabel
            // 
            noteTitleLabel.AutoSize = true;
            noteTitleLabel.Dock = DockStyle.Top;
            noteTitleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            noteTitleLabel.Location = new Point(0, 0);
            noteTitleLabel.Name = "noteTitleLabel";
            noteTitleLabel.Padding = new Padding(10);
            noteTitleLabel.Size = new Size(20, 50);
            noteTitleLabel.TabIndex = 2;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.Controls.Add(addNoteButton, 0, 0);
            tableLayoutPanel.Controls.Add(editNoteButton, 1, 0);
            tableLayoutPanel.Controls.Add(removeNoteButton, 2, 0);
            tableLayoutPanel.Dock = DockStyle.Bottom;
            tableLayoutPanel.Location = new Point(0, 511);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.Size = new Size(784, 50);
            tableLayoutPanel.TabIndex = 1;
            // 
            // addNoteButton
            // 
            addNoteButton.Dock = DockStyle.Fill;
            addNoteButton.Location = new Point(3, 3);
            addNoteButton.Name = "addNoteButton";
            addNoteButton.Size = new Size(255, 44);
            addNoteButton.TabIndex = 0;
            addNoteButton.Text = "Add Note";
            addNoteButton.Click += AddNoteButton_Click;
            // 
            // editNoteButton
            // 
            editNoteButton.Dock = DockStyle.Fill;
            editNoteButton.Location = new Point(264, 3);
            editNoteButton.Name = "editNoteButton";
            editNoteButton.Size = new Size(255, 44);
            editNoteButton.TabIndex = 1;
            editNoteButton.Text = "Edit Note";
            editNoteButton.Click += EditNoteButton_Click;
            // 
            // removeNoteButton
            // 
            removeNoteButton.Dock = DockStyle.Fill;
            removeNoteButton.Location = new Point(525, 3);
            removeNoteButton.Name = "removeNoteButton";
            removeNoteButton.Size = new Size(256, 44);
            removeNoteButton.TabIndex = 2;
            removeNoteButton.Text = "Remove Note";
            removeNoteButton.Click += RemoveNoteButton_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(784, 561);
            Controls.Add(splitContainer);
            Controls.Add(tableLayoutPanel);
            MinimumSize = new Size(600, 400);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NoteApp";
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            tableLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        // Загрузка заметок из проекта в ListBox с фильтрацией по типу
        private void LoadNotes()
        {
            notesListBox.Items.Clear();
            filteredNotes = new List<Note>(); // Создаем новый список для фильтрованных заметок
            string selectedType = noteTypeComboBox.SelectedItem.ToString();

            foreach (var note in this.project.getNotesList())
            {
                // Если выбран "All" или тип заметки совпадает с выбранным типом в ComboBox
                if (selectedType == "All" || Enum.GetName(note.getTypeOfNote()) == selectedType)
                {
                    notesListBox.Items.Add(note.getName());
                    filteredNotes.Add(note); // Добавляем заметку в фильтрованный список
                }
            }

            // Если есть заметки в списке, выбираем первую
            if (notesListBox.Items.Count > 0)
            {
                notesListBox.SelectedIndex = 0;
            }
            else
            {
                ClearNoteDetails(); // Очищаем правую панель, если заметок нет
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
                Note selectedNote = filteredNotes[index]; // Используем индекс для фильтрованного списка
                noteTitleLabel.Text = selectedNote.getName(); // Устанавливаем название заметки
                noteTypeLabel.Text = Enum.GetName(typeof(TypeNoteEnum), selectedNote.getTypeOfNote()); // Устанавливаем тип заметки
                noteDetailsLabel.Text = $"Текст: {selectedNote.getTextOfNote()}\n" +
                                        $"Дата создания: {selectedNote.getDateTimeCreate()}\n" +
                                        $"Дата изменения: {selectedNote.getDateTimeUpdate()}";
            }
            else
            {
                ClearNoteDetails(); // Если индекс не найден, очищаем правую панель
            }
        }

        private void ClearNoteDetails()
        {
            noteTitleLabel.Text = string.Empty;
            noteTypeLabel.Text = string.Empty;
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
