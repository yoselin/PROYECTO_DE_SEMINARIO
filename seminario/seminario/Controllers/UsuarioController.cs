using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using seminario.Models;
using System.Web.Security;

namespace seminario.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/

        public ActionResult perfil()
        {

            ViewBag.Msessage = "definodo";
            MembershipUser usuario_actual = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
            DataClasses1DataContext db = new DataClasses1DataContext();

            System.Guid idus = db.aspnet_Users.Where(a=>a.UserName == usuario_actual.UserName).Select(a=>a.UserId).ToArray()[0];
            
            var infoUsuario = from mem in db.aspnet_Memberships
                              from pers in db.PERSONAs
                              from asp_us in db.aspnet_Users
                              where (mem.UserId == idus && pers.UserId == idus && asp_us.UserId == idus)
                              select new datosview ()
                              {
                                  NickName = usuario_actual.UserName,
                                  Email = mem.Email,
                                  Nombre = pers.NOMBRE,
                                  app = pers.APATERNO,
                                  apm = pers.AMATERNO,
                                  descripcion = pers.DESCRIPCION,
                                  ubicacion = pers.UBICACION
                              };
            ViewBag.datos = infoUsuario;

            return View();
        }
        public ActionResult Libros()
        {

            return View();
        }

    }
}
