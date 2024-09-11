using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    internal class ManagerProject
    {

        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "json.txt");
        

        public Project loadProjectFromJsonFile()
        {
            
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    List<Note> notesList = serializer.Deserialize<List<Note>>(reader);

                    return new Project(notesList);
                }
            } catch (FileNotFoundException ex) {
                Console.WriteLine($"Файл не найден! Будет создан новый файл для заметок! {ex.Message}");
                return new Project(new List<Note>());
            }

           
        }
        public void saveProjectToJsonFile(Project project)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                //DateFormatString = "dd:MM:yy HH:mm",
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };

            string json = JsonConvert.SerializeObject(project.getNotesList(), settings);

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(json);
            }
        }

        public String getFilePath() {
            return filePath;
        }
        public void setFilePath(String filePath)
        {
            this.filePath = filePath;
        }


    }
}
