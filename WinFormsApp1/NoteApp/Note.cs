using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    public class Note : ICloneable
    {
        String name = "Без названия";
        TypeNoteEnum noteType;
        String textOfNote;
        readonly DateTime dateTimeCreate;
        DateTime dateTimeUpdate;

        public Note(String name, TypeNoteEnum noteType,
            String textOfNote, DateTime dateTimeUpdate)
        {
            this.name = name;
            this.noteType = noteType;
            this.textOfNote = textOfNote;
            this.dateTimeCreate = DateTime.Now;
            this.dateTimeUpdate = dateTimeCreate;
        }



        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
            this.dateTimeUpdate = DateTime.Now;
        }

        public TypeNoteEnum getTypeOfNote()
        {
            return noteType;
        }

        public void setTypeOfNote(TypeNoteEnum noteType)
        {
            this.noteType = noteType;
            this.dateTimeUpdate = DateTime.Now;

        }

        public String getTextOfNote()
        {
            return textOfNote;
        }

        public void setTextOfNote(String textOfNote)
        {
            this.textOfNote = textOfNote;
            this.dateTimeUpdate = DateTime.Now;
        }

        public DateTime getDateTimeCreate()
        {
            return dateTimeCreate;
        }

        public DateTime getDateTimeUpdate()
        {
            return dateTimeUpdate;
        }

        public void setDateTimeUpdate(DateTime dateTimeUpdate)
        {
            this.dateTimeUpdate = dateTimeUpdate;
        }

        public object Clone()
        {
            return new Note(
                this.name,
                this.noteType,
                this.textOfNote,
                this.dateTimeUpdate
            );
        }
    }
}
