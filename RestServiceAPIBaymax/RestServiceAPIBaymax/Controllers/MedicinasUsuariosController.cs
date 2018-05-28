using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RestServiceAPIBaymax.Models;
using RestServiceBaymax.Models;

namespace RestServiceAPIBaymax.Controllers
{
    public class MedicinasUsuariosController : ApiController
    {
        private RestServiceAPIBaymaxContext db = new RestServiceAPIBaymaxContext();

        // GET: api/MedicinasUsuarios
        public IQueryable<MedicinasUsuario> GetMedicinasUsuarios()
        {
            return db.MedicinasUsuarios;
        }

        // GET: api/MedicinasUsuarios/5
        [ResponseType(typeof(MedicinasUsuario))]
        public async Task<IHttpActionResult> GetMedicinasUsuario(int id)
        {
            MedicinasUsuario medicinasUsuario = await db.MedicinasUsuarios.FindAsync(id);
            if (medicinasUsuario == null)
            {
                return NotFound();
            }

            return Ok(medicinasUsuario);
        }

        // PUT: api/MedicinasUsuarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMedicinasUsuario(int id, MedicinasUsuario medicinasUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicinasUsuario.UsuarioId)
            {
                return BadRequest();
            }

            db.Entry(medicinasUsuario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicinasUsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MedicinasUsuarios
        [ResponseType(typeof(MedicinasUsuario))]
        public async Task<IHttpActionResult> PostMedicinasUsuario(MedicinasUsuario medicinasUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MedicinasUsuarios.Add(medicinasUsuario);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = medicinasUsuario.UsuarioId }, medicinasUsuario);
        }

        // DELETE: api/MedicinasUsuarios/5
        [ResponseType(typeof(MedicinasUsuario))]
        public async Task<IHttpActionResult> DeleteMedicinasUsuario(int id)
        {
            MedicinasUsuario medicinasUsuario = await db.MedicinasUsuarios.FindAsync(id);
            if (medicinasUsuario == null)
            {
                return NotFound();
            }

            db.MedicinasUsuarios.Remove(medicinasUsuario);
            await db.SaveChangesAsync();

            return Ok(medicinasUsuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MedicinasUsuarioExists(int id)
        {
            return db.MedicinasUsuarios.Count(e => e.UsuarioId == id) > 0;
        }
    }
}