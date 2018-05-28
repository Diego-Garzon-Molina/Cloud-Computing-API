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
    public class MedicinasController : ApiController
    {
        private RestServiceAPIBaymaxContext db = new RestServiceAPIBaymaxContext();

        // GET: api/Medicinas
        public IQueryable<Medicina> GetMedicinas()
        {
            return db.Medicinas;
        }

        // GET: api/Medicinas/5
        [Route("api/Medicinas/{CodigoNacional:int}")]
        [ResponseType(typeof(Medicina))]
        public async Task<IHttpActionResult> GetMedicina(int CodigoNacional)
        {
            Medicina medicina = await db.Medicinas.Where(b => b.CodigoNacional == CodigoNacional).FirstAsync();
            if (medicina == null)
            {
                return NotFound();
            }

            return Ok(medicina);
        }

        // PUT: api/Medicinas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMedicina(int id, Medicina medicina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicina.MedicinaId)
            {
                return BadRequest();
            }

            db.Entry(medicina).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicinaExists(id))
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

        // POST: api/Medicinas
        [ResponseType(typeof(Medicina))]
        public async Task<IHttpActionResult> PostMedicina(Medicina medicina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Medicinas.Add(medicina);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = medicina.MedicinaId }, medicina);
        }

        // DELETE: api/Medicinas/5
        [ResponseType(typeof(Medicina))]
        public async Task<IHttpActionResult> DeleteMedicina(int id)
        {
            Medicina medicina = await db.Medicinas.FindAsync(id);
            if (medicina == null)
            {
                return NotFound();
            }

            db.Medicinas.Remove(medicina);
            await db.SaveChangesAsync();

            return Ok(medicina);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MedicinaExists(int id)
        {
            return db.Medicinas.Count(e => e.MedicinaId == id) > 0;
        }
    }
}