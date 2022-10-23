using System.Collections.Generic;
using System.Linq;

namespace SuperMercado.Models
{
    public class ProductoModel
    {
        //Clase para visualizar en el formulario principal
        private List<Producto> productos;
        public ProductoModel()
        {
            productos = new List<Producto>()
            {
                new Producto
                {
                    id = 1,
                    nombre = "Coca Cola",
                    precio = 5.99,
                    imagen = "cocacola.jpg"
                },
                new Producto
                {
                    id = 2,
                    nombre = "Jabon Liquido",
                    precio = 30.70,
                    imagen = "jabon.jpg"
                },
                new Producto
                {
                    id = 3,
                    nombre = "Papel Higienico",
                    precio = 25.50,
                    imagen = "papel.jpg"
                },
                new Producto
                {
                    id = 4,
                    nombre = "Leche Pil en Tarro",
                    precio = 220.99,
                    imagen = "leche.jpg"
                }
            };
        }
        public List<Producto> getTodo()
        {
           //retorna todos los productos
            return productos;
        }
        public Producto getProducto(int id)
        {
            // retornamos un producto por su id
            return productos.Single(p => p.id == id);
        }
    }
}


