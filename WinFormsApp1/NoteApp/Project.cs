namespace NoteApp
{
    public class Project
    {
        private List<Note> notesList;

        public Project(List<Note> notesList)
        {
            if (notesList == null)
            {
                notesList = new List<Note>();
            }
            this.notesList = notesList;
        }


        public void addNote(Note note)
        {
            notesList.Add(note);
        }

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

        public List<Note> getNotesList()
        {
            return notesList;
        }

        public void setNotesList(List<Note> notesList)
        {
            this.notesList = notesList;
        }
    }
}

