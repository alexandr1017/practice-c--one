namespace NoteApp
{
    /// <summary>
    /// Форма, отображающая информацию о программе.
    /// </summary>
    public partial class AboutForm : Form
    {
        /// <summary>
        /// Конструктор по умолчанию для AboutForm.
        /// </summary>
        public AboutForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Инициализация компонентов формы.
        /// </summary>
        private void InitializeComponent()
        {
            aboutLabel = new Label();
            SuspendLayout();
            // 
            // aboutLabel
            // 
            /// <summary>
            /// Метка, отображающая информацию о версии и авторе приложения.
            /// </summary>
            aboutLabel.AutoSize = true;
            aboutLabel.Location = new Point(10, 10);
            aboutLabel.Name = "aboutLabel";
            aboutLabel.Size = new Size(169, 30);
            aboutLabel.TabIndex = 0;
            aboutLabel.Text = "NoteApp v1.0\nAuthor: Alexandr Gorshnyakov";
            aboutLabel.Click += aboutLabel_Click;
            // 
            // AboutForm
            // 
            /// <summary>
            /// Устанавливаются параметры формы, такие как размер окна, заголовок и стартовая позиция.
            /// </summary>
            ClientSize = new Size(384, 161);
            Controls.Add(aboutLabel);
            Name = "AboutForm";
            Text = "О программе";
            Load += AboutForm_Load;
            ResumeLayout(false);
            PerformLayout();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Событие, вызываемое при нажатии на метку aboutLabel.
        /// </summary>
        private void aboutLabel_Click(object sender, EventArgs e)
        {
            // Логика при клике на метку (пока не реализовано).
        }

        /// <summary>
        /// Метка, отображающая информацию о программе.
        /// </summary>
        private Label aboutLabel;

        /// <summary>
        /// Событие, вызываемое при загрузке формы AboutForm.
        /// </summary>
        private void AboutForm_Load(object sender, EventArgs e)
        {
            // Логика при загрузке формы (пока не реализовано).
        }
    }
}
