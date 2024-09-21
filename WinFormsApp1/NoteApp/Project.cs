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
        /// <param name="note">Заметка для обновления.</param>
        /// <returns>
        /// Возвращает true, если заметка успешно обновлена. 
        /// Возвращает false, если заметка не найдена в списке или является null.
        /// </returns>
        public bool updateNote(Note note)
        {
            if (note != null && !notesList.Contains(note))
            {
                return false;
            }
            else
            {
                int findIndex = notesList.IndexOf(note);
                if (findIndex != -1)
                {
                    notesList[findIndex] = note;
                    return true;
                }
                return false;
            }
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

