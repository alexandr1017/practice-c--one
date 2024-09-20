namespace NoteApp
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            aboutLabel = new Label();
            SuspendLayout();
            // 
            // aboutLabel
            // 
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
            ClientSize = new Size(384, 161);
            Controls.Add(aboutLabel);
            Name = "AboutForm";
            Text = "О программе";
            Load += AboutForm_Load;
            ResumeLayout(false);
            PerformLayout();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void aboutLabel_Click(object sender, EventArgs e)
        {

        }

        private Label aboutLabel;

        private void AboutForm_Load(object sender, EventArgs e)
        {

        }
    }
}
