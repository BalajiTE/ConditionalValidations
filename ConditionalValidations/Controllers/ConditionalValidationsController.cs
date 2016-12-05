using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ConditionalValidations.Models;

namespace ConditionalValidations.Controllers
{
    public class ConditionalValidationsController : ApiController
    {
        private ConditionalValidationsContext db = new ConditionalValidationsContext();

        // GET: api/ConditionalValidations
        public IQueryable<ConditionalValidation> GetConditionalValidations()
        {
            return db.ConditionalValidation;
        }

        // GET: api/ConditionalValidations/5
        [ResponseType(typeof(ConditionalValidation))]
        public IHttpActionResult GetConditionalValidation(int id)
        {
            ConditionalValidation conditionalValidation = db.ConditionalValidation.Find(id);
            if (conditionalValidation == null)
            {
                return NotFound();
            }

            return Ok(conditionalValidation);
        }

        // PUT: api/ConditionalValidations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConditionalValidation(int id, ConditionalValidation conditionalValidation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != conditionalValidation.ConditionalValidationId)
            {
                return BadRequest();
            }

            db.Entry(conditionalValidation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConditionalValidationExists(id))
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

        // POST: api/ConditionalValidations
        [ResponseType(typeof(ConditionalValidation))]
        public IHttpActionResult PostConditionalValidation(ConditionalValidation conditionalValidation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ConditionalValidation.Add(conditionalValidation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = conditionalValidation.ConditionalValidationId }, conditionalValidation);
        }

        // DELETE: api/ConditionalValidations/5
        [ResponseType(typeof(ConditionalValidation))]
        public IHttpActionResult DeleteConditionalValidation(int id)
        {
            ConditionalValidation conditionalValidation = db.ConditionalValidation.Find(id);
            if (conditionalValidation == null)
            {
                return NotFound();
            }

            db.ConditionalValidation.Remove(conditionalValidation);
            db.SaveChanges();

            return Ok(conditionalValidation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConditionalValidationExists(int id)
        {
            return db.ConditionalValidation.Count(e => e.ConditionalValidationId == id) > 0;
        }
    }
}