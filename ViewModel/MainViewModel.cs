using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Model;
using Model.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ViewModel
{
    public class MainViewModel : NotificationObject
    {
        private DataAccess _dataAcces;
        private string _nombre;
        private string _apellidos;
        private decimal _promedio;
        private int _id;
        private ICommand _agregarCommand;
        private ICommand _modificarCommand;
        private ICommand _eliminarCommand;
        private ICommand _limpiarCommand;
        private List<Alumno> _alumnos;
        private Alumno _alumnoSeleccionado;

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if(value == _nombre) return;
                _nombre = value;
                RaisePropertyChanged(() => Nombre);
            }
        }
        public string Apellidos
        {
            get { return _apellidos; }
            set
            {
                if (value == _apellidos) return;
                _apellidos = value;
                RaisePropertyChanged(() => Apellidos);
            }
        }
        public decimal Promedio
        {
            get { return _promedio; }
            set
            {
                if (value == _promedio) return;
                _promedio = value;
                RaisePropertyChanged(() => Promedio);
            }
        }

        public int Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }
        public List<Alumno> Alumnos
        {
            get { return _alumnos; }
            set
            {
                if (value == _alumnos) return;
                _alumnos = value;
                RaisePropertyChanged(() => Alumnos);
            }
        }
        public Alumno AlumnoSeleccionado
        {
            get { return _alumnoSeleccionado; }
            set
            {
                if (value == _alumnoSeleccionado) return;
                _alumnoSeleccionado = value;

                if (_alumnoSeleccionado == null) return;
                Id = _alumnoSeleccionado.Id;
                Nombre = _alumnoSeleccionado.Nombre;
                Apellidos = _alumnoSeleccionado.Apellidos;
                Promedio = _alumnoSeleccionado.Promedio;

                RaisePropertyChanged(() => AlumnoSeleccionado);
            }
        }
        public ICommand AgregarCommand
        {
            get
            {
                if(_agregarCommand == null)
                {
                    _agregarCommand = new DelegateCommand(AgregarAlumno, () => true);
                }
                return _agregarCommand;
            }
        }

        public ICommand ModificarCommand
        {
            get
            {
                if (_modificarCommand == null)
                {
                    _modificarCommand = new DelegateCommand(ModificarAlumno, () => true);
                }
                return _modificarCommand;
            }
        }

        public ICommand EliminarCommand
        {
            get
            {
                if (_eliminarCommand == null)
                {
                    _eliminarCommand = new DelegateCommand(EliminarAlumno, () => true);
                }
                return _eliminarCommand;
            }
        }

        public ICommand LimpiarCommand
        {
            get
            {
                if (_limpiarCommand == null)
                {
                    _limpiarCommand = new DelegateCommand(LimpiarAlumno, () => true);
                }
                return _limpiarCommand;
            }
        }

        public MainViewModel()
        {
            Alumnos = new List<Alumno>();
            _dataAcces = new DataAccess();
            Alumnos = _dataAcces.GetAlumnosList();
        }



        public void AgregarAlumno()
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellidos))
            {
                MessageBox.Show("Favor de llenar los campos", "Error");
            } else
            {
                Alumno alumno = new Alumno();
                alumno.Nombre = Nombre;
                alumno.Apellidos = Apellidos;
                alumno.Promedio = Promedio;
                if (Alumnos.Any(a => (a.Nombre == alumno.Nombre && a.Apellidos == alumno.Apellidos)))
                {
                    MessageBox.Show("Ya existe un alumno con este nombre, ingrese otro");
                }
                else
                {
                    _dataAcces.SaveAlumno(alumno);
                    this.LimpiarAlumno();
                }
            }
        }

        public void ModificarAlumno()
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellidos))
            {
                MessageBox.Show("Favor de seleccionar un alumno", "Error");
            } else if (Id == 0)
            {
                MessageBox.Show("Alumno no esta registrado", "Error");
            } else
            {
                Alumno alumno = new Alumno();
                alumno.Id = Id;
                alumno.Nombre = Nombre;
                alumno.Apellidos = Apellidos;
                alumno.Promedio = Promedio;
                _dataAcces.UpdAlumno(alumno);
                this.LimpiarAlumno();
            }
        }

        public void EliminarAlumno()
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellidos))
            {
                MessageBox.Show("Favor de seleccionar un alumno", "Error");

            } else if (Id == 0)
            {
                MessageBox.Show("Alumno no esta registrado", "Error");
            } else
            {   
                DialogResult result = MessageBox.Show("Desea eliminar el alumno?", "Advertencia", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    Alumno alumno = new Alumno();
                    alumno.Id = Id;
                    alumno.Nombre = Nombre;
                    alumno.Apellidos = Apellidos;
                    alumno.Promedio = Promedio;
                    _dataAcces.DelAlumno(alumno);
                    this.LimpiarAlumno();
                }
            }
        }

        public void LimpiarAlumno()
        {
            Alumnos = _dataAcces.GetAlumnosList();
            Id = 0;
            Nombre = String.Empty;
            Apellidos = String.Empty;
            Promedio = 0;
        }
    }
}
