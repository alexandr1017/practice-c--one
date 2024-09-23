using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Tests
{
    public class NoteTest
    {
        [Test (Description = "Тестирует корректную инициализацию объекта Note через конструктор")]
        public void Constructor_ShouldInitializeFieldsCorrectly()
        {
            // Arrange
            var creationDate = DateTime.Now;
            var updateDate = DateTime.Now;
            var note = new Note("Test Note", TypeNoteEnum.Work, "Test Text", creationDate, updateDate);

            // Assert
            Assert.AreEqual("Test Note", note.getName());
            Assert.AreEqual(TypeNoteEnum.Work, note.getTypeOfNote());
            Assert.AreEqual("Test Text", note.getTextOfNote());
            Assert.AreEqual(creationDate, note.getDateTimeCreate());
            Assert.AreEqual(updateDate, note.getDateTimeUpdate());
        }

        [Test (Description = "Проверяет, что имя будет установлено по умолчанию, если передано пустое или null значение")]
        public void Constructor_ShouldAssignDefaultName_WhenNameIsEmptyOrNull()
        {
            // Arrange
            var noteWithEmptyName = new Note("", TypeNoteEnum.Work, "Test Text", DateTime.Now, DateTime.Now);
            var noteWithNullName = new Note(null, TypeNoteEnum.Work, "Test Text", DateTime.Now, DateTime.Now);

            // Assert
            Assert.AreEqual("Без названия", noteWithEmptyName.getName());
            Assert.AreEqual("Без названия", noteWithNullName.getName());
        }

        [Test (Description = "Убеждается, что имя будет обрезано до 50 символов, если оно длиннее.")]
        public void Constructor_ShouldTruncateName_WhenNameExceeds50Characters()
        {
            // Arrange
            var longName = new string('a', 51);
            var note = new Note(longName, TypeNoteEnum.Work, "Test Text", DateTime.Now, DateTime.Now);

            // Assert
            Assert.AreEqual(50, note.getName().Length);
            Assert.AreEqual(new string('a', 50), note.getName());
        }

        [Test (Description = "Проверяет, что установка нового имени обновляет также дату последнего изменения.")]
        public void SetName_ShouldUpdateNameAndSetUpdateDateTime()
        {
            // Arrange
            var note = new Note("Initial Name", TypeNoteEnum.Work, "Test Text", DateTime.Now, DateTime.Now);
            var oldUpdateTime = note.getDateTimeUpdate();

            // Act
            note.setName("Updated Name");

            // Assert
            Assert.AreEqual("Updated Name", note.getName());
            Assert.Greater(note.getDateTimeUpdate(), oldUpdateTime);
        }

        [Test (Description = "Проверка, что если имя установлено как null или пустая строка, оно будет заменено на \"Без названия\"")]
        public void SetName_ShouldAssignDefaultName_WhenNameIsNullOrEmpty()
        {
            // Arrange
            var note = new Note("Initial Name", TypeNoteEnum.Work, "Test Text", DateTime.Now, DateTime.Now);

            // Act
            note.setName("");
            var emptyNameResult = note.getName();

            note.setName(null);
            var nullNameResult = note.getName();

            // Assert
            Assert.AreEqual("Без названия", emptyNameResult);
            Assert.AreEqual("Без названия", nullNameResult);
        }

        [Test (Description = "Проверяет изменение типа заметки и обновление времени изменения.")]
        public void SetTypeOfNote_ShouldUpdateNoteTypeAndSetUpdateDateTime()
        {
            // Arrange
            var note = new Note("Test Note", TypeNoteEnum.Work, "Test Text", DateTime.Now, DateTime.Now);
            var oldUpdateTime = note.getDateTimeUpdate();

            // Act
            note.setTypeOfNote(TypeNoteEnum.Home);

            // Assert
            Assert.AreEqual(TypeNoteEnum.Home, note.getTypeOfNote());
            Assert.Greater(note.getDateTimeUpdate(), oldUpdateTime);
        }

        [Test (Description = "Проверяет изменение текста заметки и обновление времени изменения.")]
        public void SetTextOfNote_ShouldUpdateTextAndSetUpdateDateTime()
        {
            // Arrange
            var note = new Note("Test Note", TypeNoteEnum.Work, "Initial Text", DateTime.Now, DateTime.Now);
            var oldUpdateTime = note.getDateTimeUpdate();

            // Act
            note.setTextOfNote("Updated Text");

            // Assert
            Assert.AreEqual("Updated Text", note.getTextOfNote());
            Assert.Greater(note.getDateTimeUpdate(), oldUpdateTime);
        }

        [Test (Description = "Проверка, что метод Clone возвращает точную копию объекта Note.")]
        public void Clone_ShouldReturnExactCopyOfNote()
        {
            // Arrange
            var creationDate = DateTime.Now;
            var updateDate = DateTime.Now;
            var note = new Note("Test Note", TypeNoteEnum.Work, "Test Text", creationDate, updateDate);

            // Act
            var clonedNote = (Note)note.Clone();

            // Assert
            Assert.AreEqual(note.getName(), clonedNote.getName());
            Assert.AreEqual(note.getTypeOfNote(), clonedNote.getTypeOfNote());
            Assert.AreEqual(note.getTextOfNote(), clonedNote.getTextOfNote());
            Assert.AreEqual(note.getDateTimeCreate(), clonedNote.getDateTimeCreate());
            Assert.AreEqual(note.getDateTimeUpdate(), clonedNote.getDateTimeUpdate());
        }


        [Test (Description = "Проверяет, что метод setDateTimeUpdate корректно обновляет дату последнего изменения.")]
        public void SetDateTimeUpdate_ShouldUpdateCorrectly()
        {
            // Arrange
            var note = new Note("Test Note", TypeNoteEnum.Work, "Test Text", DateTime.Now, DateTime.Now);
            var newUpdateDate = DateTime.Now.AddMinutes(10);

            // Act
            note.setDateTimeUpdate(newUpdateDate);

            // Assert
            Assert.AreEqual(newUpdateDate, note.getDateTimeUpdate());
        }

        [Test(Description = "Проверяет, что метод getDateTimeUpdate возвращает правильную дату последнего обновления.")]
        public void GetDateTimeUpdate_ShouldReturnCorrectUpdateDate()
        {
            // Arrange
            var updateDate = DateTime.Now;
            var note = new Note("Test Note", TypeNoteEnum.Work, "Test Text", DateTime.Now, updateDate);

            // Act
            var resultUpdateDate = note.getDateTimeUpdate();

            // Assert
            Assert.AreEqual(updateDate, resultUpdateDate);
        }


        [Test (Description = "Проверяет, что метод getDateTimeCreate возвращает правильную дату создания.")]
        public void GetDateTimeCreate_ShouldReturnCorrectCreationDate()
        {
            // Arrange
            var creationDate = DateTime.Now;
            var note = new Note("Test Note", TypeNoteEnum.Work, "Test Text", creationDate, DateTime.Now);

            // Act
            var resultCreationDate = note.getDateTimeCreate();

            // Assert
            Assert.AreEqual(creationDate, resultCreationDate);
        }

        [Test (Description = "Проверяет, что метод getTextOfNote возвращает правильный текст заметки.")]
        public void GetTextOfNote_ShouldReturnCorrectText()
        {
            // Arrange
            var note = new Note("Test Note", TypeNoteEnum.Work, "Initial Text", DateTime.Now, DateTime.Now);

            // Act
            var textOfNote = note.getTextOfNote();

            // Assert
            Assert.AreEqual("Initial Text", textOfNote);
        }

        [Test (Description = "Проверяет, что метод getTypeOfNote возвращает правильный тип заметки.")]
        public void GetTypeOfNote_ShouldReturnCorrectNoteType()
        {
            // Arrange
            var note = new Note("Test Note", TypeNoteEnum.Finance, "Test Text", DateTime.Now, DateTime.Now);

            // Act
            var noteType = note.getTypeOfNote();

            // Assert
            Assert.AreEqual(TypeNoteEnum.Finance, noteType);
        }


    }
}
