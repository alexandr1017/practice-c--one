using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    /// <summary>
    /// Класс для управления проектами, включающий методы для загрузки и сохранения проекта в JSON-файл.
    /// </summary>
    public class ManagerProject
    {
        /// <summary>
        /// Путь к файлу, в который будут сохраняться и загружаться данные заметок.
        /// По умолчанию файл сохраняется в папке "Мои документы" с именем "json.txt".
        /// </summary>
        static string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "json.txt");

        /// <summary>
        /// Загружает проект из JSON-файла. Если файл не найден, возвращает новый проект с пустым списком заметок.
        /// </summary>
        /// <returns>Проект, содержащий список заметок <see cref="Project"/>.</returns>
        public static Project loadProjectFromJsonFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    // Десериализация списка заметок из JSON-файла
                    List<Note> notesList = serializer.Deserialize<List<Note>>(reader);

                    return new Project(notesList);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Файл не найден! Будет создан новый файл для заметок! {ex.Message}");
                return new Project(new List<Note>());
            }
        }

        /// <summary>
        /// Сохраняет текущий проект в JSON-файл.
        /// Если файл существует, он будет перезаписан.
        /// </summary>
        /// <param name="project">Проект, который нужно сохранить <see cref="Project"/>.</param>
        public static void saveProjectToJsonFile(Project project)
        {
            // Если файл существует, он удаляется перед записью нового содержимого
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Настройки сериализации для правильного форматирования JSON
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,  // Игнорирование циклических ссылок
                Formatting = Formatting.Indented,  // Форматирование JSON с отступами для удобства чтения
                Converters = new List<JsonConverter> { new StringEnumConverter() }  // Преобразование перечислений в строковый формат
            };

            // Сериализация списка заметок в JSON-строку
            string json = JsonConvert.SerializeObject(project.getNotesList(), settings);

            // Запись сериализованных данных в файл
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(json);
            }
        }
    }
}
