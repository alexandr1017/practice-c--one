using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NoteApp
{
    /// <summary>
    /// Класс представляет собой заметку, содержащую название, тип, текст,
    /// а также информацию о времени создания и последнего обновления.
    /// Реализует интерфейс ICloneable для создания копий заметок.
    /// </summary>
    public class Note : ICloneable
    {
        /// <summary>
        /// Константа по умолчанию для имени заметки, если оно не задано.
        /// </summary>
        private const String DEFAULT_NAME_NOTE = "Без названия";
        
        /// <summary>
        /// Название заметки.
        /// </summary>
        public String name;
        
        /// <summary>
        /// Тип заметки. Используется перечисление <see cref="TypeNoteEnum"/>.
        /// </summary>
        public TypeNoteEnum noteType;
        
        /// <summary>
        /// Текст заметки.
        /// </summary>
        
        public String textOfNote;
        /// <summary>
        /// Дата и время создания заметки. Неизменяемое поле.
        /// </summary>
        public readonly DateTime dateTimeCreate = DateTime.Now;

        /// <summary>
        /// Дата и время последнего обновления заметки.
        /// </summary>
        public DateTime dateTimeUpdate = DateTime.Now;


        /// <summary>
        /// Конструктор с параметрами, используемый для создания объекта заметки.
        /// </summary>
        /// <param name="name">Название заметки.</param>
        /// <param name="noteType">Тип заметки.</param>
        /// <param name="textOfNote">Текст заметки.</param>
        /// <param name="dateTimeCreate">Дата и время создания заметки.</param>
        /// <param name="dateTimeUpdate">Дата и время последнего обновления заметки.</param>
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

        /// <summary>
        /// Проверяет корректность имени заметки.
        /// Если имя пустое, возвращает значение по умолчанию. 
        /// Если имя превышает 50 символов, оно обрезается до этого лимита.
        /// </summary>
        /// <param name="name">Имя для проверки.</param>
        /// <returns>Корректное имя заметки.</returns>
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




        /// <summary>
        /// Возвращает название заметки.
        /// </summary>
        /// <returns>Название заметки.</returns>
        public String getName()
        {
            return name;
        }

        /// <summary>
        /// Устанавливает новое имя заметки и обновляет дату изменения.
        /// </summary>
        /// <param name="name">Новое имя заметки.</param>
        public void setName(String name)
        {
            this.name = nameCheck(name);
            this.dateTimeUpdate = DateTime.Now;
        }

        /// <summary>
        /// Возвращает тип заметки.
        /// </summary>
        /// <returns>Тип заметки.</returns>
        public TypeNoteEnum getTypeOfNote()
        {
            return noteType;
        }

        /// <summary>
        /// Устанавливает новый тип заметки и обновляет дату изменения.
        /// </summary>
        /// <param name="noteType">Новый тип заметки.</param>
        public void setTypeOfNote(TypeNoteEnum noteType)
        {
            this.noteType = noteType;
            this.dateTimeUpdate = DateTime.Now;

        }

        /// <summary>
        /// Возвращает текст заметки.
        /// </summary>
        /// <returns>Текст заметки.</returns>
        public String getTextOfNote()
        {
            return textOfNote;
        }


        /// <summary>
        /// Устанавливает новый текст заметки и обновляет дату изменения.
        /// </summary>
        /// <param name="textOfNote">Новый текст заметки.</param>
        public void setTextOfNote(String textOfNote)
        {
            this.textOfNote = textOfNote;
            this.dateTimeUpdate = DateTime.Now;
        }

        /// <summary>
        /// Возвращает дату и время создания заметки.
        /// </summary>
        /// <returns>Дата и время создания.</returns>
        public DateTime getDateTimeCreate()
        {
            return dateTimeCreate;
        }

        /// <summary>
        /// Возвращает дату и время последнего обновления заметки.
        /// </summary>
        /// <returns>Дата и время последнего обновления.</returns>
        public DateTime getDateTimeUpdate()
        {
            return dateTimeUpdate;
        }

        /// <summary>
        /// Устанавливает дату и время последнего обновления заметки.
        /// </summary>
        /// <param name="dateTimeUpdate">Дата и время последнего обновления.</param>
        public void setDateTimeUpdate(DateTime dateTimeUpdate)
        {
            this.dateTimeUpdate = dateTimeUpdate;
        }

        /// <summary>
        /// Создаёт копию текущей заметки.
        /// </summary>
        /// <returns>Копия объекта <see cref="Note"/>.</returns>
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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Note other = (Note)obj;
            return this.name == other.name &&
                   this.noteType == other.noteType &&
                   this.textOfNote == other.textOfNote &&
                   this.dateTimeCreate == other.dateTimeCreate &&
                   this.dateTimeUpdate == other.dateTimeUpdate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, noteType, textOfNote, dateTimeCreate, dateTimeUpdate);
        }
    }
}
