using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data.Entity;

namespace DemoAPI.Controllers
{

    public class OwnerController : ApiController
    {
        demoapiEntities entities = new demoapiEntities();
        public IEnumerable<owner> Get()
        {
            //disables proxy classes creation for entity instances
            entities.Configuration.ProxyCreationEnabled = false;

            return entities.owners.ToList();

        }

        public void Put(int id, [FromBody]owner ownerEdit)
        {
            var ownerToEdit = entities.owners.Find(id);
            ownerToEdit.name = ownerEdit.name;
            ownerToEdit.country = ownerEdit.country;
            ownerToEdit.email = ownerEdit.email;
            entities.Entry(ownerToEdit).State = System.Data.Entity.EntityState.Modified;
            entities.SaveChanges();
            
        }

   

        public void Delete(int id)
        {
            using (demoapiEntities entities = new demoapiEntities())
            {
                var ownerToDelete = entities.owners.FirstOrDefault(o => o.owner_id == id);
                List<ship> shipOwners = entities.ships.Include(s => s.owners).ToList();
                System.Diagnostics.Debug.WriteLine(ownerToDelete);

                entities.owners.Remove(ownerToDelete);
                entities.SaveChanges();

            }
        }
    }
}
