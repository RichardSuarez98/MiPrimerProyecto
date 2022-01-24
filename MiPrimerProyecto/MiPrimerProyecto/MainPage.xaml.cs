using MiPrimerProyecto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MiPrimerProyecto
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            cargarDatos();
        }

        
        
        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Alumno alum = new Alumno();
                alum.Nombre = txtNombre.Text;
                alum.ApellidoPaterno = txtApellidoPaterno.Text;
                alum.ApellidoMaterno = txtApellidoMaterno.Text;
                alum.edad = Convert.ToInt32(txtEdad.Text);
                alum.Email = txtEmail.Text;
                await App.SQLiteDB.SaveAlumno(alum);
                LimpiarPantalla();
                await DisplayAlert("Registro","Registrado Exitosamente","Ok");

                /*Variable alumno lis*/
                cargarDatos();
                
            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresar todos los datos", "Ok");
            }


        }


        public bool validarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }else if(string.IsNullOrEmpty(txtApellidoPaterno.Text)){
                respuesta = false;
            }else if (string.IsNullOrEmpty(txtApellidoMaterno.Text))
            {
                respuesta = false;
            }else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                respuesta = false;
            }

            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        public  async void cargarDatos()
        {
            var alumniList = await App.SQLiteDB.GetAlumnosAsync();
            if (alumniList != null)
            {
                lstAlumnos.ItemsSource = alumniList;
            }
        }


        public void LimpiarPantalla()
        {
            txtNombre.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            txtEdad.Text = "";
            txtEmail.Text = "";

            txtId.Text = "";
            txtId.IsVisible = false;
            btnActualizar.IsVisible = false;
            btnRegistrar.IsVisible = true;
            btnEliminar.IsVisible = false;
        }

        private async void lstAlumnos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Alumno)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtId.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.IdAlumno.ToString()))
            {
                var alumno = await App.SQLiteDB.GetAlumnoByIdAsync(obj.IdAlumno);
                if (alumno != null)
                {
                    txtId.Text = Convert.ToString(alumno.IdAlumno);
                    txtNombre.Text = alumno.Nombre;
                    txtApellidoPaterno.Text = alumno.ApellidoPaterno;
                    txtApellidoMaterno.Text = alumno.ApellidoMaterno;
                    txtEdad.Text = Convert.ToString(alumno.edad);
                    txtEmail.Text = alumno.Email;
                }
            }

        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                Alumno alum = new Alumno();
                alum.IdAlumno = Convert.ToInt32(txtId.Text);
                alum.Nombre = txtNombre.Text;
                alum.ApellidoPaterno = txtApellidoPaterno.Text;
                alum.ApellidoMaterno = txtApellidoMaterno.Text;
                alum.edad =Convert.ToInt32( txtEdad.Text);
                alum.Email = txtEmail.Text;

                await App.SQLiteDB.SaveAlumno(alum);
                await DisplayAlert("Registro", "Actualizacion Exitosamente", "Ok");

                LimpiarPantalla();
                cargarDatos();

            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var alumno = await App.SQLiteDB.GetAlumnoByIdAsync(Convert.ToInt32(txtId.Text));
            if (alumno !=null)
            {
                await App.SQLiteDB.DeleteAlumno(alumno);
                await DisplayAlert("Alumno","Se elimino de manera correcta","Ok");
                LimpiarPantalla();
                cargarDatos();
            }
        }
    }



}
