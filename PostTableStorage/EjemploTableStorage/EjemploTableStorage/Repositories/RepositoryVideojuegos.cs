using EjemploTableStorage.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EjemploTableStorage.Repositories
{
    public class RepositoryVideojuegos
    {
        CloudTable tabla;

        public RepositoryVideojuegos()
        {
            String keys = CloudConfigurationManager.GetSetting("StorageConnectionString");//Nombre de tu conexión.
            CloudStorageAccount account = CloudStorageAccount.Parse(keys);
            CloudTableClient client = account.CreateCloudTableClient();
            this.tabla = client.GetTableReference("videojuegos");
            this.tabla.CreateIfNotExists();
        }

        public void CrearVideojuego(String estudio, String idvideojuego
         , String nombre, String creador,String descripcion,DateTime fecha,int precio,String categorias)
        {
            
            Videojuego videojuego = new Videojuego();

            videojuego.Estudio = estudio;
            videojuego.IdVideojuego = idvideojuego;
            videojuego.Nombre = nombre;
            videojuego.Creador = creador;
            videojuego.Descripcion = descripcion;
            videojuego.FechaLanzamiento = fecha;
            videojuego.Precio = precio;
            videojuego.Categorias = categorias;

            TableOperation operation = TableOperation.Insert(videojuego);
            this.tabla.Execute(operation);
        }

        public Videojuego BuscarVideojuego(String partitionkey
           , String rowkey)
        {
            TableOperation operation = TableOperation.Retrieve<Videojuego>(partitionkey, rowkey);
            TableResult result = this.tabla.Execute(operation);
            if (result.Result == null)
            {
                return null;
            }
            else
            {
                Videojuego videojuego = result.Result as Videojuego;
                return videojuego;
            }
        }

        public void ModificarVideojuego(String estudio, String idvideojuego
         , String nombre, String creador, String descripcion, DateTime fecha, int precio, String categorias)
        {
            Videojuego videojuego = this.BuscarVideojuego(estudio, idvideojuego);
            if (videojuego != null)
            {
                videojuego.Nombre = nombre;
                videojuego.Creador = creador;
                videojuego.Descripcion = descripcion;
                videojuego.FechaLanzamiento = fecha;
                videojuego.Precio = precio;
                videojuego.Categorias = categorias;
                TableOperation operation = TableOperation.Replace(videojuego);
                this.tabla.Execute(operation);
            }
        }

        public void EliminarVideojuego(String partitionkey, String rowkey)
        {
            Videojuego videojuego = this.BuscarVideojuego(partitionkey, rowkey);
            if (videojuego != null)
            {
                TableOperation operation = TableOperation.Delete(videojuego);
                this.tabla.Execute(operation);
            }
        }


        public List<Videojuego> MostrarVideojuegos()
        {
            TableQuery<Videojuego> query = new TableQuery<Videojuego>();
            List<Videojuego> videojuegos = this.tabla.ExecuteQuery(query).ToList();
            return videojuegos;
        }

    }
}