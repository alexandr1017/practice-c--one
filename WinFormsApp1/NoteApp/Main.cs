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

       
            Project project = new Project(new List<Note>());

         
            Note noteOne = new Note("Заметка1", TypeNoteEnum.Home, "Купить молока");
            Note noteTwo = new Note("Заметка2", TypeNoteEnum.Work, "Выполнить задачу 1");

         
            project.addNote(noteOne);
            project.addNote(noteTwo);

            
            foreach (Note note in project.getNotesList())
            {
                Console.WriteLine($"Название: {note.getName()}, Тип: {note.getTypeOfNote()}, Текст: {note.getTextOfNote()}");
            }
        }
    }
}
