namespace NoteApp.Tests
{
    [TestFixture]
    public class ProjectTest
    {
        [Test(Description = "“ест провер€ет, что конструктор Project инициализирует пустой список заметок, если передан null.")]
        public void Constructor_ShouldInitializeEmptyList_WhenPassedNull()
        {
            // Act
            var project = new Project(null);

            // Assert
            Assert.IsNotNull(project.getNotesList());
            Assert.AreEqual(0, project.getNotesList().Count);
        }

        [Test(Description = "“ест провер€ет, что конструктор Project корректно инициализирует список заметок, если передан не null.")]
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

        [Test(Description = "“ест провер€ет, что метод addNote корректно добавл€ет заметку в список проекта.")]
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

        [Test(Description = "“ест провер€ет, что метод removeNoteOfNotesList удал€ет существующую заметку из списка и возвращает true.")]
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

        [Test(Description = "“ест провер€ет, что метод removeNoteOfNotesList возвращает false, если заметка не найдена в списке.")]
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

        [Test(Description = "“ест провер€ет, что метод updateNote обновл€ет существующую заметку и возвращает true.")]
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

        [Test(Description = "“ест провер€ет, что метод updateNote возвращает false, если заметка не найдена в списке.")]
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

        [Test(Description = "“ест провер€ет, что метод getNotesList возвращает правильный список заметок.")]
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

        [Test(Description = "“ест провер€ет, что метод setNotesList корректно устанавливает новый список заметок.")]
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