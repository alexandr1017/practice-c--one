using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Tests
{
    internal class ManagerProjectTest
    {
        [Test(Description = "Тест проверяет, что проект с пустым списком заметок корректно сохраняется в JSON-файл.")]
        public void SaveProjectToJsonFile_ShouldSaveEmptyProject()
        {
            // Arrange
            var emptyProject = new Project(new List<Note>());
            var mockFilePath = Path.Combine(Path.GetTempPath(), "test_empty_project.json");

            // Act
            ManagerProject.saveProjectToJsonFile(emptyProject, mockFilePath);

            // Assert
            Assert.IsTrue(File.Exists(mockFilePath), "Файл должен быть создан.");
            var jsonContent = File.ReadAllText(mockFilePath);
            Assert.AreEqual("[]", jsonContent.Trim(), "JSON файл должен содержать пустой массив.");
        }

        [Test(Description = "Тест проверяет, что проект с одной заметкой корректно сохраняется в JSON-файл.")]
        public void SaveProjectToJsonFile_ShouldSaveProjectWithOneNote()
        {
            // Arrange
            var note = new Note("Test Note", TypeNoteEnum.Work, "Test Content", DateTime.Now, DateTime.Now);
            var project = new Project(new List<Note> { note });
            var mockFilePath = Path.Combine(Path.GetTempPath(), "test_project_one_note.json");

            // Act
            ManagerProject.saveProjectToJsonFile(project, mockFilePath);

            // Assert
            Assert.IsTrue(File.Exists(mockFilePath), "Файл должен быть создан.");
            var jsonContent = File.ReadAllText(mockFilePath);
            Assert.IsTrue(jsonContent.Contains("Test Note"), "JSON файл должен содержать данные заметки.");
        }

        [Test(Description = "Тест проверяет, что при загрузке проекта из несуществующего файла создается пустой проект.")]
        public void LoadProjectFromJsonFile_ShouldReturnEmptyProjectWhenFileNotFound()
        {
            // Arrange
            var mockFilePath = Path.Combine(Path.GetTempPath(), "non_existent_file.json");
            if (File.Exists(mockFilePath)) File.Delete(mockFilePath);

            // Act
            var project = ManagerProject.loadProjectFromJsonFile(mockFilePath);

            // Assert
            Assert.NotNull(project);
            Assert.IsEmpty(project.getNotesList(), "Список заметок должен быть пустым.");
        }

        [Test(Description = "Тест проверяет, что проект с одной заметкой корректно загружается из JSON-файла.")]
        public void LoadProjectFromJsonFile_ShouldLoadProjectWithOneNote()
        {
            // Arrange
            var note = new Note("Loaded Note", TypeNoteEnum.Home, "Content", DateTime.Now, DateTime.Now);
            var project = new Project(new List<Note> { note });
            var mockFilePath = Path.Combine(Path.GetTempPath(), "test_load_project.json");

            // Сохранение проекта перед загрузкой для имитации файла
            ManagerProject.saveProjectToJsonFile(project, mockFilePath);

            // Act
            var loadedProject = ManagerProject.loadProjectFromJsonFile(mockFilePath);

            // Assert
            Assert.AreEqual(1, loadedProject.getNotesList().Count, "В проекте должна быть одна заметка.");
            Assert.AreEqual("Loaded Note", loadedProject.getNotesList()[0].getName(), "Название заметки должно совпадать.");
        }

    }
}
