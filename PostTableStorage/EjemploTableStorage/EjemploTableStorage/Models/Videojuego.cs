using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace EjemploTableStorage.Models
{
    public class Videojuego : TableEntity
    {
        public Videojuego() { }

        private String _Estudio { get; set; }
        public String Estudio
        { 
            get
            {
                return this._Estudio;
            }
            set
            {
                this.PartitionKey = value;
                this._Estudio = value;
            }
        }
        private String _IdVideojuego { get; set; }
        public String IdVideojuego
        {
            get
            {
                return this._IdVideojuego;
            }
            set
            {
                this.RowKey = value;
                this._IdVideojuego = value;
            }
        }
        public String Nombre { get; set; }
        public String Creador { get; set; }
        public String Descripcion { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public int Precio { get; set; }
        public String Categorias { get; set; }
    }
}