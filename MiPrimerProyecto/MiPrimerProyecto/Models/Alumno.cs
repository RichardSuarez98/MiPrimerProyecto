using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MiPrimerProyecto.Models
{
    public class Alumno
    {
        [PrimaryKey,AutoIncrement]
        public int IdAlumno { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; }

        [MaxLength(50)]
        public string  ApellidoPaterno { get; set; }

        [MaxLength(50)]
        public string ApellidoMaterno { get; set; }

        public int edad { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }
    }


}
