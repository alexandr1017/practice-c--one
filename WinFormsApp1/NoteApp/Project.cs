namespace NoteApp
{
    /// <summary>
    /// Класс представляет проект, содержащий список заметок.
    /// Предоставляет методы для добавления, удаления, обновления и управления списком заметок.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Список заметок, связанных с проектом.
        /// </summary>
        private List<Note> notesList;

        /// <summary>
        /// Конструктор, создающий объект проекта с переданным списком заметок.
        /// Если переданный список равен null, создается пустой список.
        /// </summary>
        /// <param name="notesList">Список заметок для инициализации проекта.</param>
        public Project(List<Note> notesList)
        {
            if (notesList == null)
            {
                notesList = new List<Note>();
            }
            this.notesList = notesList;
        }

        /// <summary>
        /// Добавляет заметку в список проекта.
        /// </summary>
        /// <param name="note">Заметка для добавления.</param>
        public void addNote(Note note)
        {
            notesList.Add(note);
        }

        /// <summary>
        /// Удаляет заметку из списка проекта.
        /// </summary>
        /// <param name="note">Заметка, которую необходимо удалить.</param>
        /// <returns>
        /// Возвращает true, если заметка успешно удалена. 
        /// Возвращает false, если заметка не найдена в списке или является null.
        /// </returns>
        public bool removeNoteOfNotesList(Note note)
        {
            if (note != null && !notesList.Contains(note))
            {
                return false;
            }
            else
            {
                notesList.Remove(note);
                return true;
            }
        }

        /// <summary>
        /// Обновляет заметку в списке проекта, если она существует.
        /// </summary>
        /// <param name="updatedNote">Заметка для обновления.</param>
        /// <returns>
        /// Возвращает true, если заметка успешно обновлена. 
        /// Возвращает false, если заметка не найдена в списке или является null.
        /// </returns>
        public bool updateNote(Note updatedNote)
        {
            if (updatedNote == null)
            {
                return false;
            }

        // Найти заметку по имени
        /*FirstOrDefault:
        Это метод расширения, доступный для коллекций, поддерживающих интерфейс IEnumerable<T>, таких как List<T>.
        Он выполняет поиск первого элемента в коллекции, который удовлетворяет заданному условию, или возвращает значение по умолчанию,
        если такой элемент не найден.
        Если элемент не найден, метод возвращает null для ссылочных типов, таких как объекты классов (в данном случае Note).
        note => note.getName() == updatedNote.getName():
        Это лямбда-выражение, которое представляет собой условие, по которому осуществляется поиск.
        Лямбда-выражение означает: "для каждого объекта note в notesList, верни первую заметку, у которой
        название (note.getName()) совпадает с названием заметки, которую мы пытаемся обновить (updatedNote.getName())".*/
            var existingNote = notesList.FirstOrDefault(note => note.getName() == updatedNote.getName());

            if (existingNote != null)
            {
                // Обновляем только необходимые поля
                existingNote.setTypeOfNote(updatedNote.getTypeOfNote());
                existingNote.setTextOfNote(updatedNote.getTextOfNote());
                existingNote.setDateTimeUpdate(updatedNote.getDateTimeUpdate()); // Обновляем дату

                return true;
            }

            return false; // Если заметка не найдена
        }

        /// <summary>
        /// Возвращает список заметок, связанных с проектом.
        /// </summary>
        /// <returns>Список заметок <see cref="List{Note}"/>.</returns>
        public List<Note> getNotesList()
        {
            return notesList;
        }

        /// <summary>
        /// Устанавливает новый список заметок для проекта.
        /// </summary>
        /// <param name="notesList">Новый список заметок.</param>
        public void setNotesList(List<Note> notesList)
        {
            this.notesList = notesList;
        }
    }
}

