using Microsoft.AspNetCore.Mvc;
using Barberia.Logica;
using Barberia.Entidades;
using System;
using System.Net;
using System.Net.Mail;

namespace Barberia.IUMVC.Controllers
{
    public class LoginController : Controller
    {
        UsuarioBL usuarioBL = new UsuarioBL();

        // LOGIN
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string correo, string password)
        {
            Usuario u = usuarioBL.Login(correo, password);

            if (u != null)
            {
                HttpContext.Session.SetString("usuario", u.Nombre);
                HttpContext.Session.SetString("rol", u.Rol);
                HttpContext.Session.SetInt32("id_usuario", u.IdUsuario);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View();
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        // 🔐 VISTA RECUPERAR
        public IActionResult Recuperar()
        {
            return View();
        }

        // 🔐 PROCESAR RECUPERACIÓN
        [HttpPost]
        public IActionResult Recuperar(string correo)
        {
            Usuario usuario = usuarioBL.ObtenerPorCorreo(correo);

            if (usuario != null)
            {
                string token = Guid.NewGuid().ToString();
                DateTime expira = DateTime.Now.AddMinutes(30);

                usuarioBL.GuardarToken(usuario.IdUsuario, token, expira);

                string link = $"https://localhost:7296/Login/Reset?token={token}";

                EnviarCorreo(correo, link);

                ViewBag.Mensaje = "Se envió un enlace de recuperación a tu correo";
            }
            else
            {
                ViewBag.Mensaje = "Correo no encontrado";
            }

            return View();
        }

        // 🔐 VISTA RESET (MEJORADA)
        public IActionResult Reset(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index");
            }

            ViewBag.Token = token;
            return View();
        }

        // 🔐 GUARDAR NUEVA CONTRASEÑA (MEJORADO)
        [HttpPost]
        public IActionResult Reset(string token, string password)
        {
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.Mensaje = "Token inválido";
                return View();
            }

            var usuario = usuarioBL.ObtenerPorToken(token);

            if (usuario != null)
            {
                usuarioBL.CambiarPassword(usuario.IdUsuario, password);

                // 🔥 REDIRECCIÓN AUTOMÁTICA AL LOGIN
                TempData["Mensaje"] = "Contraseña actualizada correctamente";
                return RedirectToAction("Index");
            }
            else
            {
                // 🔥 MUY IMPORTANTE: devolver token a la vista
                ViewBag.Token = token;
                ViewBag.Mensaje = "Token inválido o expirado";
                return View();
            }
        }

        // 📧 ENVIAR CORREO
        public void EnviarCorreo(string destino, string link)
        {
            var remitente = "orellana13sam09@gmail.com";
            var clave = "hsdswfttebjqdlyc";

            MailMessage correo = new MailMessage();
            correo.From = new MailAddress(remitente);
            correo.To.Add(destino);
            correo.Subject = "Recuperación de contraseña";
            correo.Body = $"Haz clic en el siguiente enlace para restablecer tu contraseña:\n{link}";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(remitente, clave);
            smtp.EnableSsl = true;

            smtp.Send(correo);
        }
    }
}