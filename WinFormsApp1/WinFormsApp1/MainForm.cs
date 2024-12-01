using NoteAppUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    /// <summary>
    /// Класс MainForm — это главное окно приложения
    /// для работы с заметками.Оно отображает список
    /// заметок, детальную информацию о выбранной заметке,
    /// а также предоставляет функционал для добавления,
    /// редактирования и удаления заметок.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Экземпляр класса Project, который хранит список заметок.
        /// </summary>
        private Project project;

        /// <summary>
        /// Элемент интерфейса ListBox, отображающий список заметок.
        /// </summary>
        private ListBox notesListBox;

        /// <summary>
        /// Метка, отображающая название выбранной заметки.
        /// </summary>
        private Label noteTitleLabel;

        /// <summary>
        /// Метка, отображающая тип выбранной заметки.
        /// </summary>
        private Label noteTypeLabel;

        /// <summary>
        /// Метка, указывающая категорию типа заметки (например, личная, рабочая).
        /// </summary>
        private Label noteTypeCategoryLabel;

        /// <summary>
        /// Панель, которая содержит метки типа заметки и категории.
        /// </summary>
        private FlowLayoutPanel noteTypePanel;

        /// <summary>
        /// Выпадающий список для фильтрации заметок по типу.
        /// </summary>
        private ComboBox noteTypeComboBox;

        /// <summary>
        /// Панель с прокруткой, отображающая текст выбранной заметки.
        /// </summary>
        private Panel noteDetailsPanel;

        /// <summary>
        /// Метка, отображающая дату последнего изменения заметки.
        /// </summary>
        private Label noteUpdateDateLabel;

        /// <summary>
        /// Метка, отображающая дату создания заметки.
        /// </summary>
        private Label noteCreationDateLabel;

        /// <summary>
        /// Метка, отображающая текст заметки.
        /// </summary>
        private Label noteDetailsLabel;

        /// <summary>
        /// Кнопка для добавления новой заметки.
        /// </summary>
        private Button addNoteButton;

        /// <summary>
        /// Кнопка для редактирования выбранной заметки.
        /// </summary>
        private Button editNoteButton;

        /// <summary>
        /// Кнопка для удаления выбранной заметки.
        /// </summary>
        private Button removeNoteButton;

        /// <summary>
        /// Главное меню приложения.
        /// </summary>
        private MenuStrip menuStrip;

        /// <summary>
        /// Контейнер для разделения окна на две части: список заметок и детальную информацию о заметке.
        /// </summary>
        private SplitContainer splitContainer;

        /// <summary>
        /// Панель для размещения кнопок в нижней части окна.
        /// </summary>
        private TableLayoutPanel tableLayoutPanel;

        /// <summary>
        /// Список заметок, отфильтрованных по выбранному типу.
        /// </summary>
        private List<Note> filteredNotes;

        /// <summary>
        /// инициализирует форму с проектом, переданным в качестве аргумента.
        /// Вызывает методы InitializeComponent() и InitializeMenu() для создания интерфейса и меню.
        /// Добавляет обработчик события закрытия формы FormClosing.
        /// </summary>
        /// <param name="project">Проект, который содержит заметки для отображения.</param>
        public MainForm(Project project)
        {
            this.project = project;
            InitializeComponent();
            InitializeMenu();
            this.FormClosing += MainForm_FormClosing;
        }

        /// <summary>
        /// Этот метод инициализирует компоненты формы пользовательского интерфейса:
        /// Создает SplitContainer для разделения окна на две части(список заметок и детальная информация).
        /// Создает TableLayoutPanel для размещения кнопок(добавить, редактировать, удалить заметки).
        /// Инициализирует и настраивает элементы интерфейса: notesListBox, noteTitleLabel, noteTypeComboBox, noteDetailsPanel, noteTypePanel,
        /// метки для дат создания и изменения заметок.
        /// Устанавливает обработчики событий для работы с элементами интерфейса, таких как нажатие на кнопки, выбор заметок и изменение фильтра.
        /// Загружает заметки через метод LoadNotes().
        /// </summary>
        /// <summary>
        /// Инициализация компонентов пользовательского интерфейса.
        /// </summary>
        private void InitializeComponent()
        {
            // Настройка главного окна приложения
            Text = "NoteApp"; // Заголовок окна
            MinimumSize = new Size(640, 480); // Минимальный размер окна
            Size = new Size(720, 720); // Стартовый размер окна
            StartPosition = FormStartPosition.CenterScreen; // Центрирование окна на экране

            // Создание SplitContainer для разделения окна на две области
            splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill, // Заполнение всей области окна
                Orientation = Orientation.Vertical, // Вертикальное разделение
                SplitterWidth = 2, // Ширина разделителя
                SplitterDistance = 75 // Начальная позиция разделителя
            };

            // Создание TableLayoutPanel для кнопок внизу окна
            tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Bottom, // Привязка к нижней части окна
                ColumnCount = 5, // Количество колонок, увеличиваем до 5 для кнопок сортировки
                RowCount = 1, // Количество строк
                Height = 50 // Высота панели
            };
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // 20% на каждую кнопку
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

            // Создание ComboBox для выбора типа заметок
            noteTypeComboBox = new ComboBox
            {
                Dock = DockStyle.Top, // Привязка к верхней части
                Items = { "All" } // Начальное значение
            };
            noteTypeComboBox.Items.AddRange(Enum.GetNames(typeof(TypeNoteEnum))); // Добавление типов заметок из перечисления
            noteTypeComboBox.SelectedIndex = 0; // Установка начального индекса
            noteTypeComboBox.SelectedIndexChanged += NoteTypeComboBox_SelectedIndexChanged; // Событие при изменении выбора

            // Создание ListBox для отображения списка заметок
            notesListBox = new ListBox
            {
                Dock = DockStyle.Fill // Заполнение области панели
            };
            notesListBox.SelectedIndexChanged += NotesListBox_SelectedIndexChanged; // Событие при выборе заметки
            notesListBox.MouseDoubleClick += NotesListBox_MouseDoubleClick; // Событие при двойном щелчке
            notesListBox.KeyDown += NotesListBox_KeyDown; // Событие при нажатии клавиши

            // Добавление ComboBox и ListBox в левую панель SplitContainer
            splitContainer.Panel1.Controls.Add(notesListBox);
            splitContainer.Panel1.Controls.Add(noteTypeComboBox);

            // Создание Label для заголовка заметки
            noteTitleLabel = new Label
            {
                Dock = DockStyle.Top, // Привязка к верхней части
                AutoSize = true, // Автоматическое определение размера
                Font = new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold), // Стиль текста
                Padding = new Padding(10), // Отступы
                Height = 50, // Высота метки
                MaximumSize = new Size(splitContainer.Panel2.ClientSize.Width - 20, 0) // Максимальная ширина, с учетом отступов
            };

            // Обновление максимальной ширины метки при изменении размеров панели
            splitContainer.Panel2.Resize += (s, e) =>
            {
                noteTitleLabel.MaximumSize = new Size(splitContainer.Panel2.ClientSize.Width - 20, 0); // Обновляем ширину
            };

            // Создание FlowLayoutPanel для отображения категорий и дат заметки
            noteTypePanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top, // Привязка к верхней части
                Height = 30, // Высота панели
                Padding = new Padding(10), // Отступы
                AutoSize = true, // Автоматическое изменение размеров
                AutoSizeMode = AutoSizeMode.GrowAndShrink, // Режим изменения размера
                FlowDirection = FlowDirection.LeftToRight // Направление элементов
            };

            // Элементы для отображения категорий и дат заметок
            noteTypeCategoryLabel = new Label
            {
                Text = "  Категория заметки:",
                AutoSize = true,
                Font = new Font(FontFamily.GenericSansSerif, 12),
                TextAlign = ContentAlignment.MiddleLeft
            };

            noteTypeLabel = new Label
            {
                AutoSize = true,
                Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Underline),
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(10, 0, 0, 0)
            };

            noteCreationDateLabel = new Label
            {
                AutoSize = true,
                Font = new Font(FontFamily.GenericSansSerif, 12),
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(10, 0, 0, 0)
            };

            noteUpdateDateLabel = new Label
            {
                AutoSize = true,
                Font = new Font(FontFamily.GenericSansSerif, 12),
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(10, 0, 0, 0)
            };

            // Добавление элементов в FlowLayoutPanel
            noteTypePanel.Controls.Add(noteTypeCategoryLabel);
            noteTypePanel.Controls.Add(noteTypeLabel);
            noteTypePanel.Controls.Add(noteCreationDateLabel);
            noteTypePanel.Controls.Add(noteUpdateDateLabel);

            // Создание панели для отображения детальной информации о заметке
            noteDetailsPanel = new Panel
            {
                Dock = DockStyle.Fill, // Заполнение области
                AutoScroll = true, // Прокрутка при необходимости
                BorderStyle = BorderStyle.None // Убираем рамку панели
            };

            // Label для текста заметки
            noteDetailsLabel = new Label
            {
                AutoSize = true, // Автоматический размер
                Padding = new Padding(10), // Отступы
                TextAlign = ContentAlignment.TopLeft, // Выравнивание текста
                MaximumSize = new Size(noteDetailsPanel.ClientSize.Width - 20, 0) // Максимальный размер
            };

            // Обновление максимального размера текста при изменении размеров панели
            noteDetailsPanel.Resize += (s, e) =>
            {
                noteDetailsLabel.MaximumSize = new Size(noteDetailsPanel.ClientSize.Width - 20, 0);
            };

            // Добавление текста заметки в панель
            noteDetailsPanel.Controls.Add(noteDetailsLabel);

            // Добавление панели с деталями, категорий и заголовка в правую панель SplitContainer
            splitContainer.Panel2.Controls.Add(noteDetailsPanel);
            splitContainer.Panel2.Controls.Add(noteTypePanel);
            splitContainer.Panel2.Controls.Add(noteTitleLabel);

            // Кнопки для управления заметками
            addNoteButton = new Button
            {
                Text = "Add Note", // Текст кнопки
                Dock = DockStyle.Fill // Заполнение области ячейки
            };
            addNoteButton.Click += AddNoteButton_Click; // Событие нажатия кнопки
            tableLayoutPanel.Controls.Add(addNoteButton, 0, 0); // Первая ячейка

            editNoteButton = new Button
            {
                Text = "Edit Note",
                Dock = DockStyle.Fill
            };
            editNoteButton.Click += EditNoteButton_Click;
            tableLayoutPanel.Controls.Add(editNoteButton, 1, 0); // Вторая ячейка

            removeNoteButton = new Button
            {
                Text = "Remove Note",
                Dock = DockStyle.Fill
            };
            removeNoteButton.Click += RemoveNoteButton_Click;
            tableLayoutPanel.Controls.Add(removeNoteButton, 2, 0); // Третья ячейка

            // Кнопки сортировки
            var sortByNameButton = new Button
            {
                Text = "Сортировать по имени",
                Dock = DockStyle.Fill // Заполнение области ячейки
            };
            sortByNameButton.Click += SortByNameButton_Click;
            tableLayoutPanel.Controls.Add(sortByNameButton, 3, 0); // Четвертая ячейка

            var sortByDateButton = new Button
            {
                Text = "Сортировать по дате изменения",
                Dock = DockStyle.Fill // Заполнение области ячейки
            };
            sortByDateButton.Click += SortByDateButton_Click;
            tableLayoutPanel.Controls.Add(sortByDateButton, 4, 0); // Пятая ячейка

            // Добавление SplitContainer и TableLayoutPanel в основное окно
            Controls.Add(splitContainer);
            Controls.Add(tableLayoutPanel);

            // Загрузка заметок
            LoadNotes();
        }




        /// <summary>
        /// Загружает заметки из Project и фильтрует заметки в зависимости от выбранного типа.
        /// Если выбрано "All", загружаются все заметки. Если выбран конкретный тип, загружаются заметки только этого типа.
        /// После загрузки выбирает первую заметку, если список не пуст.
        /// </summary>
        private void LoadNotes()
        {
            notesListBox.Items.Clear();
            filteredNotes = new List<Note>();
            string selectedType = noteTypeComboBox.SelectedItem.ToString();

            // Загружаем и фильтруем заметки по типу
            foreach (var note in this.project.getNotesList())
            {
                if (selectedType == "All" || Enum.GetName(note.getTypeOfNote()) == selectedType)
                {
                    filteredNotes.Add(note);
                }
            }

            // Сортировка заметок по дате создания (в обратном порядке)
            filteredNotes = filteredNotes.OrderByDescending(note => note.getDateTimeCreate()).ToList();

            // Добавление отсортированных заметок в список
            foreach (var note in filteredNotes)
            {
                notesListBox.Items.Add(note.getName());
            }

            // Установка выбранной заметки, если они есть
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
        /// <summary>
        /// Обработчик события изменения выбранного типа в noteTypeComboBox.
        /// Перезагружает список заметок при изменении фильтра.
        /// </summary>
        private void NoteTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadNotes(); // Перезагружаем список заметок при изменении фильтра
        }

        /// <summary>
        /// Обработка выбора заметки в ListBox.
        /// Загружает информацию о выбранной заметке (название, тип, текст, дата создания и изменения) и отображает её в соответствующих метках.
        /// Если нет выбранной заметки, очищает детали заметки.
        /// </summary>
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
                noteDetailsLabel.Text = $"{selectedNote.getTextOfNote()}";
            }
            else
            {
                ClearNoteDetails();
            }
        }


        /// <summary>
        /// Очищает детали заметки при отсутствии выбранной заметки в правой панели
        /// </summary>
        private void ClearNoteDetails()
        {
            noteTitleLabel.Text = string.Empty;
            noteTypeLabel.Text = string.Empty;
            noteTypeCategoryLabel.Text = string.Empty;

            noteCreationDateLabel.Text = string.Empty;
            noteUpdateDateLabel.Text = string.Empty;
            noteDetailsLabel.Text = string.Empty;
        }

        // Добавление новой заметки
        /// <summary>
        /// Обработчик нажатия кнопки "Add Note".
        /// Открывает форму для добавления новой заметки.
        /// После добавления заметки перезагружает список заметок.
        /// </summary
        private void AddNoteButton_Click(object sender, EventArgs e)
        {
            EditNoteForm editForm = new EditNoteForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                project.addNote(editForm.Note);
                LoadNotes();
            }
        }


        /// <summary>
        /// Обработчик нажатия кнопки "Edit Note".
        /// Редактирование выбранной заметки
        /// Открывает форму для редактирования выбранной заметки.
        /// После изменения заметки обновляет её в проекте и перезагружает список.
        /// </summary
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

        /// <summary>
        /// Обработчик нажатия кнопки "Remove Note".
        /// Удаление выбранной заметки
        /// Показывает подтверждающее сообщение для удаления заметки.
        /// Удаляет заметку из проекта и перезагружает список, если удаление подтверждено.
        /// </summary
        private void RemoveNoteButton_Click(object sender, EventArgs e)
        {
            int index = notesListBox.SelectedIndex;

            // Проверка, что индекс выбранной заметки валиден
            if (index >= 0 && filteredNotes != null && index < filteredNotes.Count)
            {
                // Получение имени заметки для отображения в диалоге
                string noteName = filteredNotes[index].getName();

                // Отображение окна с подтверждением удаления
                var result = MessageBox.Show($"Вы действительно хотите удалить эту заметку: {noteName}?",
                                              "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Удаление заметки из коллекции и списка проекта
                    project.removeNoteOfNotesList(filteredNotes[index]);

                    // Обновление списка заметок после удаления
                    LoadNotes();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заметку для удаления.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Инициализирует главное меню программы.
        /// Создает пункты меню "File", "Edit", "Help" с соответствующими пунктами: "Exit", "Add Note", "Edit Note", "Remove Note", "About".
        /// Устанавливает горячие клавиши для некоторых пунктов меню.
        /// </summary>
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


        /// <summary>
        /// Обработчик выхода из приложения
        /// Обработчик выхода из приложения
        /// </summary>
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрыть приложение
        }

        /// <summary>
        /// Обработчик добавления новой заметки через меню.
        /// Вызывает метод AddNoteButton_Click() для добавления заметки.
        /// </summary
        private void AddNoteMenuItem_Click(object sender, EventArgs e)
        {
            AddNoteButton_Click(sender, e); // Вызов метода для создания новой заметки
        }

        /// <summary>
        /// Обработчик редактирования заметки через меню.
        /// Вызывает метод EditNoteButton_Click() для редактирования заметки.
        /// </summary
        private void EditNoteMenuItem_Click(object sender, EventArgs e)
        {
            EditNoteButton_Click(sender, e); // Вызов метода для редактирования заметки
        }

        /// <summary>
        /// Обработчик удаления заметки через меню.
        /// Вызывает метод RemoveNoteButton_Click() для удаления заметки.
        /// </summary
        private void RemoveNoteMenuItem_Click(object sender, EventArgs e)
        {
            RemoveNoteButton_Click(sender, e); // Вызов метода для удаления заметки
        }

        /// <summary>
        /// Обработчик вызова окна "О программе" через меню.
        /// Открывает форму AboutForm, которая отображает информацию о приложении.
        /// </summary
        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog(); // Открыть окно "О программе"
        }

        /// <summary>
        /// Обработчик события закрытия формы.
        /// Сохраняет данные проекта перед закрытием приложения и выводит сообщение об успешном сохранении.
        /// </summary
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Сохранение данных проекта перед выходом
            ManagerProject.saveProjectToJsonFile(project, ManagerProject.getFilePath());
            MessageBox.Show("Проект сохранен успешно.");
        }

        /// <summary>
        /// Обработчик двойного клика по заметке в списке.
        /// Вызывает метод EditSelectedNote() для открытия окна редактирования выбранной заметки.
        /// </summary>
        private void NotesListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditSelectedNote();
        }

        /// <summary>
        /// Обработчик нажатия клавиш в списке заметок
        /// Если нажата клавиша Enter, открывает окно редактирования для выбранной заметки через метод EditSelectedNote().
        /// </summary
        private void NotesListBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверяем, нажата ли клавиша Enter
            if (e.KeyCode == Keys.Enter)
            {
                EditSelectedNote();
            }
        }

        /// <summary>
        /// Редактирует выбранную заметку.
        /// Открывает форму EditNoteForm с данными выбранной заметки.
        /// После редактирования обновляет заметку в проекте и перезагружает список заметок.
        /// </summary>
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


        // Обработчик кнопки сортировки по имени
        private void SortByNameButton_Click(object sender, EventArgs e)
        {
            filteredNotes = filteredNotes.OrderBy(note => note.getName()).ToList();
            UpdateNotesList();
        }

        // Обработчик кнопки сортировки по дате
        private void SortByDateButton_Click(object sender, EventArgs e)
        {
            filteredNotes = filteredNotes.OrderByDescending(note => note.getDateTimeUpdate()).ToList();
            UpdateNotesList();
        }

        // Метод обновления списка заметок в интерфейсе
        private void UpdateNotesList()
        {
            notesListBox.Items.Clear();
            foreach (var note in filteredNotes)
            {
                notesListBox.Items.Add(note.getName());
            }

            // Если есть заметки, выделяем первую
            if (notesListBox.Items.Count > 0)
            {
                notesListBox.SelectedIndex = 0;
            }
            else
            {
                ClearNoteDetails();
            }
        }

        /// <summary>
        /// Метод для установки Project в MainForm.
        /// Загружает заметки установки проекта.
        /// </summary>
        /// <param name="project"></param>
        public void SetProject(Project project)
        {
            this.project = project;
            LoadNotes();
        }
    }
}
