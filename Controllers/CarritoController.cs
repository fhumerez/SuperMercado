using Microsoft.AspNetCore.Mvc;
using SuperMercado.Models;
using System.Collections.Generic;
using SuperMercado.Herramientas;
using System;
using System.Linq;

namespace SuperMercado.Controllers
{
    public class CarritoController : Controller
    {
        public IActionResult Index()
        {
            List<Elemento> carrito = Conversor.RecuperarObjeto<List<Elemento>>(HttpContext.Session, "carrito");
           //validacion del carrito
            if (carrito != null && carrito.Count > 0)
            {
                //a viewbag
                ViewBag.carrito = carrito;
                //calculamos total
                ViewBag.total = carrito.Sum(e => e.producto.precio * e.cantidad);
            }
            else
            {
                ViewBag.carrito = new List<Elemento>();
                ViewBag.total = 0;
            }
            return View();
        }

        [Route("Comprar/{id}")]
        public IActionResult Comprar(int id)
        {
            ProductoModel aux = new ProductoModel();//Auxiliar
            //para primer producto
            if (Conversor.RecuperarObjeto<List<Elemento>>(HttpContext.Session, "carrito") == null)
            {
                List<Elemento> carrito = new List<Elemento>();
                carrito.Add(new Elemento { producto = aux.getProducto(id), cantidad = 1 });
                Conversor.GuardarObjeto(HttpContext.Session, "carrito", carrito);
            }
            else
            // en caso de no existir o ser otro producto nuevo
            {
                List<Elemento> carrito = Conversor.RecuperarObjeto<List<Elemento>>(HttpContext.Session, "carrito");
                int indice = existe(id);
                //para aumentar un producto que ya existe
                if (indice != -1)
                    carrito[indice].cantidad++;
               //producto nuevo que no esta en el carrito
                else
                    carrito.Add(new Elemento { producto = aux.getProducto(id), cantidad = 1 });

                Conversor.GuardarObjeto(HttpContext.Session, "carrito", carrito);
            }
            return RedirectToAction("Index");
        }
        [Route("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            List<Elemento> carrito = Conversor.RecuperarObjeto<List<Elemento>>(HttpContext.Session, "carrito");
            int indice = existe(id);
            if (indice != -1)//existe
            {
                carrito[indice].cantidad--;
                if (carrito[indice].cantidad <= 0)
                    carrito.RemoveAt(indice);
                Conversor.GuardarObjeto(HttpContext.Session, "carrito", carrito);
            }
            return RedirectToAction("Index");
        }
        private int existe(int id)
        {
           //para validar si el producto existe
            List<Elemento> carrito = Conversor.RecuperarObjeto<List<Elemento>>(HttpContext.Session, "carrito");
            for (int i = 0; i < carrito.Count; i++)
            {
                if (carrito[i].producto.id == id)
                    return i;
            }
            return -1;
        }
        public IActionResult Vaciar()
        {
            try
            {
                List<Elemento> carrito = Conversor.RecuperarObjeto<List<Elemento>>(HttpContext.Session, "carrito");
                ViewBag.total = carrito.Sum(e => e.producto.precio * e.cantidad);
                carrito = new List<Elemento>();
                Conversor.GuardarObjeto(HttpContext.Session, "carrito", carrito);
                
            }
            catch (Exception)
            {

               // throw;
            }
            return View("Fin");

        }
    }
}
