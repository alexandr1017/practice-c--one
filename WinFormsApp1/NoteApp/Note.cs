using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NoteApp
{
    public class Note : ICloneable
    {
        private const String DEFAULT_NAME_NOTE = "Без названия";
        public String name;
        public TypeNoteEnum noteType;
        public String textOfNote;
        public readonly DateTime dateTimeCreate = DateTime.Now;
        public DateTime dateTimeUpdate = DateTime.Now;



        [JsonConstructor]
        public Note(String name, TypeNoteEnum noteType,
            String textOfNote, DateTime dateTimeCreate, DateTime dateTimeUpdate)
        {
            this.name =nameCheck(name);
            this.noteType = noteType;
            this.textOfNote = textOfNote;
            this.dateTimeCreate = dateTimeCreate;
            this.dateTimeUpdate = dateTimeUpdate;
        }


        private String nameCheck(String name) {
            if (String.IsNullOrEmpty(name))
            { 
                return DEFAULT_NAME_NOTE;
            }
                
            if (name.Length > 50)
                {
                    return name.Substring(0, 50);
                }
            return name;
            }
            
        



        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = nameCheck(name);
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
                this.dateTimeCreate,
                this.dateTimeUpdate
            );
        }
    }
}
