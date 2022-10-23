using Microsoft.AspNetCore.Mvc;
using SuperMercado.Models;

namespace SuperMercado.Controllers
{
    [Route("Super")]
    public class SuperController : Controller
    {
        [Route("")]
        [Route("~/")]
        [Route("index")]
        public IActionResult Index()
        {
            ProductoModel productoModel = new ProductoModel();
            ViewBag.productos = productoModel.getTodo();//pasando a la vista en Viewbag
            return View();
        }
    }
}
