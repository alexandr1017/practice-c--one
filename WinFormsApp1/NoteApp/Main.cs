using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            ManagerProject managerProject = new ManagerProject();

            Project project = managerProject.loadProjectFromJsonFile();

   


            //Note noteOne = new Note("Заметка1", TypeNoteEnum.Home, "Купить молока");
            //Note noteTwo = new Note("Заметка2", TypeNoteEnum.Work, "Выполнить задачу 1");


            //project.addNote(noteOne);
            //project.addNote(noteTwo);


            Note noteThree = new Note("Заметка3", TypeNoteEnum.HealthAndSport, "Купить витамины",DateTime.Now,DateTime.Now);
            project.addNote(noteThree);

            managerProject.saveProjectToJsonFile(project);

            foreach (Note note in project.getNotesList())
            {
                Console.WriteLine($"Название: {note.getName()}, " +
                    $"Тип: {note.getTypeOfNote()}, " +
                    $"Текст: {note.getTextOfNote()}, " +
                    $"Дата и время создания: {note.getDateTimeCreate()}, " +
                    $"Дата и время изменения: {note.getDateTimeUpdate()}");
            }

        }
    }
}
