using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenHospital
{
    internal class Program
    {
        
        // VARIABLES 

        // Requisito del examen: N es mi último dígito de carné (0) + 10 = 10
        static readonly int N = 10;

        // Vectores paralelos para mantener vinculados los datos por el mismo índice
        static string[] cedula = new string[N];
        static string[] nombre = new string[N];

        // Variable global para guardar la posición que encuentra el método de búsqueda
        static int index = -1;

        static void Main(string[] args)
        {
            // Llamamos al menú principal del sistema
            menu();
        }

        // Método principal para controlar el flujo de la aplicación con do-while
        static void menu()
        {
            int opcion = 0; // Variable local para capturar la opción digitada

            do
            {
                // Limpiamos pantalla y aplicamos colores a la consola
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Clear();
                Console.WriteLine("==========================================");
                Console.WriteLine("   SISTEMA DE CENSO Y EXPEDIENTES ");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. Registrar Paciente");
                Console.WriteLine("2. Buscar Paciente por Cédula");
                Console.WriteLine("3. Corregir Nombre de Paciente");
                Console.WriteLine("4. Dar de Baja Paciente");
                Console.WriteLine("5. Reportes");
                Console.WriteLine("6. Salir");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Ingrese una opción (1-6): ");

                // Manejo de excepciones por si digitan letras en vez de números
                try
                {
                    opcion = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    opcion = 0; 
                }

                // Evaluar la opción ingresada
                switch (opcion)
                {
                    case 1:
                        AgregarPaciente();
                        break;
                    case 2:
                        BuscarPaciente();
                        break;
                    case 3:
                        ModificarPaciente();
                        break;
                    case 4:
                        BorrarPaciente();
                        break;
                    case 5:
                        Reporte();
                        break;
                    case 6:
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                // Pausa para que el usuario pueda leer la respuesta en pantalla antes de limpiar
                Console.ReadKey();

            } while (opcion != 6);
        }

        // Función booleana auxiliar para buscar la posición de un paciente por su cédula
        static bool BuscarPorCedula(string ced)
        {
            index = -1; // Reiniciamos el índice a -1 (no encontrado)

            for (int i = 0; i < N; i++)
            {
                // Usamos .Equals para comparar cadenas y verificar si la cédula coincide
                if (ced.Equals(cedula[i]))
                {
                    index = i; // Guardamos el índice donde coincide
                    return true;
                }
            }
            return false;
        }

        // Método para agregar un nuevo paciente en la primera posición disponible
        static void AgregarPaciente()
        {
            Console.ForegroundColor = ConsoleColor.White;
            int posicionLibre = -1;

            // Recorremos el arreglo para encontrar el primer espacio null
            for (int i = 0; i < N; i++)
            {
                if (cedula[i] == null)
                {
                    posicionLibre = i;
                    break; // Salimos del ciclo al encontrar la primera posición disponible
                }
            }

            // Validar si hay espacio libre
            if (posicionLibre != -1)
            {
                Console.Write("Ingrese la cédula del paciente: ");
                cedula[posicionLibre] = Console.ReadLine();

                Console.Write("Ingrese el nombre del paciente: ");
                nombre[posicionLibre] = Console.ReadLine();

                Console.WriteLine("Paciente registrado con éxito.");
            }
            else
            {
                Console.WriteLine("El sistema está lleno, no se pueden registrar más pacientes.");
            }
        }

        // Método para consultar los datos de un paciente específico por cédula
        static void BuscarPaciente()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Ingrese la cédula que desea consultar: ");
            string ced = Console.ReadLine();

            if (BuscarPorCedula(ced))
            {
                Console.WriteLine("Paciente encontrado:");
                Console.WriteLine("Cédula: {0} | Nombre: {1}", cedula[index], nombre[index]);
            }
            else
            {
                Console.WriteLine("El paciente no se encuentra registrado.");
            }
        }

        // Método para modificar o corregir el nombre de un paciente existente
        static void ModificarPaciente()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Ingrese la cédula del paciente que desea modificar: ");
            string ced = Console.ReadLine();

            if (BuscarPorCedula(ced))
            {
                Console.WriteLine("Nombre actual: {0}", nombre[index]);
                Console.Write("Ingrese el nuevo nombre corregido: ");
                nombre[index] = Console.ReadLine(); // Reemplazamos en el mismo índice
                Console.WriteLine("Nombre actualizado con éxito.");
            }
            else
            {
                Console.WriteLine("El paciente no se encuentra registrado.");
            }
        }

        // Método para eliminar un paciente y correr las posiciones a la izquierda sin dejar espacios vacíos
        static void BorrarPaciente()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Ingrese la cédula del paciente que desea dar de baja: ");
            string ced = Console.ReadLine();

            if (BuscarPorCedula(ced))
            {
                // Reorganizamos los arreglos desplazando los elementos siguientes hacia la izquierda
                for (int j = index; j < N - 1; j++)
                {
                    cedula[j] = cedula[j + 1];
                    nombre[j] = nombre[j + 1];
                }

                // Limpiamos el último espacio del vector que quedó duplicado tras el corrimiento
                cedula[N - 1] = null;
                nombre[N - 1] = null;

                Console.WriteLine("Paciente dado de baja correctamente.");
            }
            else
            {
                Console.WriteLine("El paciente no se encuentra registrado.");
            }
        }

        // Método para listar todos los pacientes registrados actualmente en el sistema
        static void Reporte()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("****** REPORTE DE PACIENTES CENSADOS ******");
            Console.WriteLine("-------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" Cédula          Nombre");

            for (int i = 0; i < N; i++)
            {
                // Solo imprimimos las posiciones que tengan datos (diferentes de null)
                if (cedula[i] != null)
                {
                    Console.WriteLine(" {0}         {1}", cedula[i], nombre[i]);
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("------------ FIN DEL REPORTE --------------");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}