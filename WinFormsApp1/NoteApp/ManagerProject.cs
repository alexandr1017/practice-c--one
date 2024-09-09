using Newtonsoft.Json;
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
        

        public Project loadProjectFromJsonFile(String filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File at path {filePath} was not found.");
            }

            using (StreamReader sr = new StreamReader(filePath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
              
                List<Note> notesList = serializer.Deserialize<List<Note>>(reader);

                return new Project(notesList);
            }
        }
        public void saveProjectToJsonFile(String filePath, Project project)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(filePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            { serializer.Serialize(writer, project.getNotesList); }
        }


    }
}
