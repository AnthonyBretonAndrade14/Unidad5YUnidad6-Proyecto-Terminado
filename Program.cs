using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;

namespace Proyecto_U5_yU6
{
    class Comentario
    {
        public DateTime fechadepublicacion;
        public string id { get; set; }                                           //Determinacion de Variables en Clase de comentario
        public string creador { get; set; }
        public DateTime Fechadepublicacion { get => fechadepublicacion; set => fechadepublicacion = value; }
        public string comentario { get; set; }

        public IPAddress direccion_ip { get; set; }

        public int likes { get; set; }
        public bool inapropiado { get; set; }
        public Comentario(string id, string creador, DateTime fecha, string Comentario, IPAddress ip, bool inapropiado, int likes) //Constructor
        {

            this.id = id;
            this.creador = creador;
            this.fechadepublicacion = fecha;
            this.comentario = Comentario;
            this.direccion_ip = ip;
            this.inapropiado = inapropiado;
            this.likes = likes;


        }
        public override string ToString()


        {
            return string.Format($"{creador} - Comentario: {comentario} - Inapropiado {inapropiado} - IP: {direccion_ip} - Fecha {fechadepublicacion} - Likes {likes}");          //Codigo para que imprima 
        }

    }                         //Lo que Imprime 

    class Comentary

    {
        public static void SaveToFile(List<Comentario> comentarios, string path)                               //Metodo para que guarde el archivo
        {
            StreamWriter textOut = null;

            try
            {
                textOut = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));
                foreach (var comentario in comentarios)
                {
                    textOut.Write(comentario.id + "|");
                    textOut.Write(comentario.creador + "|");
                    textOut.Write(comentario.fechadepublicacion + "|");
                    textOut.Write(comentario.comentario + "|");                        //Guardar en el archivo comentario 
                    textOut.Write(comentario.direccion_ip + "|");
                    textOut.Write(comentario.inapropiado + "|");
                    textOut.WriteLine(comentario.likes);
                }
            }                                                                       //Ecepciones que podrian ocurrir 
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)                                  //Catch generica
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (textOut != null)
                    textOut.Close();
            }
        }
        //Esta en  .texto no binario
        public static List<Comentario> ReadFromFile(string path)                     //Metodo para leer el archivo
        {


            List<Comentario> comentarios = new List<Comentario>();

            StreamReader textIn = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
            try
            {

                while (textIn.Peek() != -1)                              //leer comentarios dentro del archivo
                {

                    string now = textIn.ReadLine();
                    string[] colums = now.Split("|");                      //Separa en columnas con |
                    Comentario c = new Comentario(
                         colums[0],
                     colums[1],
                         DateTime.Parse(colums[2]),
                     colums[3],
                     IPAddress.Parse(colums[4]),
                        bool.Parse(colums[5]),
                          int.Parse(colums[6]));


                    comentarios.Add(c);


                }

            }

            catch (ArgumentException e)
            {


                Console.WriteLine(e.Message);

            }
            catch (NotSupportedException e)
            {

                Console.WriteLine(e.Message);

            }
            catch (FileNotFoundException e)
            {

                Console.WriteLine(e.Message);

            }
            catch (DirectoryNotFoundException e)
            {

                Console.WriteLine(e.Message);

            }
            catch (UnauthorizedAccessException e)
            {

                Console.WriteLine(e.Message);

            }
            catch (FormatException e)
            {

                Console.WriteLine(e.Message);

            }
            catch (IOException e)
            {

                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            finally
            {

                textIn.Close();

            }
            return comentarios;
        }
        //Orden de Likes 
        public static void GetLikes(string path)
        {

            List<Comentario> comentarios;
            comentarios = ReadFromFile(path);
            var filtro_comentarios = from c in comentarios             //Mas importante del metodo ,es el que ordena el comentario en base que tiene mas likes            
                                     orderby c.likes descending
                                     select c;
            foreach (var c in filtro_comentarios)
                if (c.inapropiado == false)
                    Console.WriteLine(c);



        }
        //Orden de comentarios por fechas 
        public static void GetDate(string path)
        {

            List<Comentario> comentarios;
            comentarios = ReadFromFile(path);
            var filtro_comentarios = from c in comentarios
                                     orderby c.fechadepublicacion descending
                                     select c;
            foreach (var c in filtro_comentarios)
                if (c.inapropiado == false)
                    Console.WriteLine(c);

        }

        class program
        {

            public static void BorrarComentario(List<Comentario> comentarios)
            {


                Console.Clear();
                Console.WriteLine("Ingrese el ID de comentario que desea Eliminar permanentemente");
                string r = Console.ReadLine();

                comentarios.RemoveAll(a => a.id.Contains(r));
                foreach (var c in comentarios.Where(c => c.inapropiado == false))
                {
                    Console.WriteLine(c);


                }
                Console.ReadKey();



            }
            static void Main(string[] args)


            {


                List<Comentario> comentarios = Comentary.ReadFromFile(@"C:\Users\tonyb\Documents\comentarios.txt");            //Ruta de documento asociado 
                foreach (var c in from c in comentarios
                                  where c.inapropiado == false             //comentarios inapropiados seran ocultos 
                                  select c)
                {

                    Console.WriteLine(c);


                }
                comentarios.Add(new Comentario("123", "Mario", DateTime.Today, "Hola", IPAddress.Loopback, false, 9000));      //Loopback sacar ip
                Comentary.SaveToFile(comentarios, @"C:\Users\tonyb\Documents\comentarios.txt");                         //Anadir un comentario



                Console.WriteLine();
                Console.WriteLine(" ordenados por likes ");
                Comentary.GetLikes(@"C:\Users\tonyb\Documents\comentarios.txt");                              //Ruta de documento asociado 

                Console.WriteLine();
                Console.WriteLine(" ordenados por las fechas  ");
                Comentary.GetDate(@"C:\Users\tonyb\Documents\comentarios.txt");                                //Ruta de documento asociado 
                Console.ReadKey();

                Console.Clear();
                Console.WriteLine("Desearia borrar un comentario no deseado? [S/N]");
                string respuesta = Console.ReadLine();
                if (respuesta == "Si" | respuesta == "si")
                {

                    BorrarComentario(comentarios);

                }
                else if (respuesta == "No" | respuesta == "no")
                {

                    Console.Clear();
                    Console.WriteLine("No metio ningun formato valido");


                }
                Console.ReadKey();

            }
        }





























    }





}

