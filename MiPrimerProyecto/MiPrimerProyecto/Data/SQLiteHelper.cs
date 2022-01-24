using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using MiPrimerProyecto.Models;
using System.Threading.Tasks;

namespace MiPrimerProyecto.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Alumno>().Wait();

        }


        public Task <int> SaveAlumno(Alumno alum)
        {
            if (alum.IdAlumno != 0)
            {
                return db.UpdateAsync(alum);
            }
            else
            {
                return db.InsertAsync(alum);
            }
        }


        public Task<int> DeleteAlumno(Alumno alumno)
        {
            return db.DeleteAsync(alumno);
        }
        /// <summary>
        /// Recuperar todos los alumnos
        /// </summary>
        /// <returns></returns>
        public Task<List<Alumno>> GetAlumnosAsync()
        {
            return db.Table<Alumno>().ToListAsync();
        }

        /// <summary>
        /// Recuperar alumnos por id
        /// </summary>
        /// <param name="alumnoId">Id del alumno que se requiere</param> 
        /// <returns></returns>
        public Task<Alumno> GetAlumnoByIdAsync(int alumnoId)
        {
            return db.Table<Alumno>().Where(x => x.IdAlumno == alumnoId).FirstOrDefaultAsync();
        }





    }

}
