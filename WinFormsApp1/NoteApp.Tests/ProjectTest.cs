namespace NoteApp.Tests
{
    [TestFixture]
    public class ProjectTest
    {
        [Test(Description = "���� ���������, ��� ����������� Project �������������� ������ ������ �������, ���� ������� null.")]
        public void Constructor_ShouldInitializeEmptyList_WhenPassedNull()
        {
            // Act
            var project = new Project(null);

            // Assert
            Assert.IsNotNull(project.getNotesList());
            Assert.AreEqual(0, project.getNotesList().Count);
        }

        [Test(Description = "���� ���������, ��� ����������� Project ��������� �������������� ������ �������, ���� ������� �� null.")]
        public void Constructor_ShouldInitializeListWithGivenNotes_WhenPassedValidList()
        {
            // Arrange
            var notes = new List<Note> { new Note("Test Note", TypeNoteEnum.Work, "Test Text", DateTime.Now, DateTime.Now) };

            // Act
            var project = new Project(notes);

            // Assert
            Assert.AreEqual(1, project.getNotesList().Count);
            Assert.AreEqual("Test Note", project.getNotesList()[0].getName());
        }

        [Test(Description = "���� ���������, ��� ����� addNote ��������� ��������� ������� � ������ �������.")]
        public void AddNote_ShouldAddNoteToProject()
        {
            // Arrange
            var project = new Project(new List<Note>());
            var note = new Note("New Note", TypeNoteEnum.Work, "Note Text", DateTime.Now, DateTime.Now);

            // Act
            project.addNote(note);

            // Assert
            Assert.AreEqual(1, project.getNotesList().Count);
            Assert.AreEqual("New Note", project.getNotesList()[0].getName());
        }

        [Test(Description = "���� ���������, ��� ����� removeNoteOfNotesList ������� ������������ ������� �� ������ � ���������� true.")]
        public void RemoveNoteOfNotesList_ShouldRemoveNoteAndReturnTrue_WhenNoteExists()
        {
            // Arrange
            var note = new Note("Test Note", TypeNoteEnum.Work, "Test Text", DateTime.Now, DateTime.Now);
            var project = new Project(new List<Note> { note });

            // Act
            var result = project.removeNoteOfNotesList(note);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, project.getNotesList().Count);
        }

        [Test(Description = "���� ���������, ��� ����� removeNoteOfNotesList ���������� false, ���� ������� �� ������� � ������.")]
        public void RemoveNoteOfNotesList_ShouldReturnFalse_WhenNoteNotFound()
        {
            // Arrange
            var note = new Note("Test Note", TypeNoteEnum.Work, "Test Text", DateTime.Now, DateTime.Now);
            var project = new Project(new List<Note>());

            // Act
            var result = project.removeNoteOfNotesList(note);

            // Assert
            Assert.IsFalse(result);
        }

        [Test(Description = "���� ���������, ��� ����� updateNote ��������� ������������ ������� � ���������� true.")]
        public void UpdateNote_ShouldUpdateExistingNoteAndReturnTrue()
        {
            // Arrange
            var note = new Note("Test Note", TypeNoteEnum.Work, "Test Text", DateTime.Now, DateTime.Now);
            var project = new Project(new List<Note> { note });

            // Act
            var updatedNote = new Note("Test Note", TypeNoteEnum.Home, "Updated Text", DateTime.Now, DateTime.Now);
            var result = project.updateNote(updatedNote);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("Updated Text", project.getNotesList()[0].getTextOfNote());
            Assert.AreEqual(TypeNoteEnum.Home, project.getNotesList()[0].getTypeOfNote());
        }

        [Test(Description = "���� ���������, ��� ����� updateNote ���������� false, ���� ������� �� ������� � ������.")]
        public void UpdateNote_ShouldReturnFalse_WhenNoteNotFound()
        {
            // Arrange
            var note = new Note("Test Note", TypeNoteEnum.Work, "Test Text", DateTime.Now, DateTime.Now);
            var project = new Project(new List<Note>());

            // Act
            var result = project.updateNote(note);

            // Assert
            Assert.IsFalse(result);
        }

        [Test(Description = "���� ���������, ��� ����� getNotesList ���������� ���������� ������ �������.")]
        public void GetNotesList_ShouldReturnCorrectNotesList()
        {
            // Arrange
            var note = new Note("Test Note", TypeNoteEnum.Work, "Test Text", DateTime.Now, DateTime.Now);
            var project = new Project(new List<Note> { note });

            // Act
            var notesList = project.getNotesList();

            // Assert
            Assert.AreEqual(1, notesList.Count);
            Assert.AreEqual("Test Note", notesList[0].getName());
        }

        [Test(Description = "���� ���������, ��� ����� setNotesList ��������� ������������� ����� ������ �������.")]
        public void SetNotesList_ShouldSetNewNotesList()
        {
            // Arrange
            var project = new Project(new List<Note>());
            var newNotesList = new List<Note> { new Note("New Note", TypeNoteEnum.Finance, "New Text", DateTime.Now, DateTime.Now) };

            // Act
            project.setNotesList(newNotesList);

            // Assert
            Assert.AreEqual(1, project.getNotesList().Count);
            Assert.AreEqual("New Note", project.getNotesList()[0].getName());
        }
    }
}