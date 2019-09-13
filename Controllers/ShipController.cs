using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data.Entity;
using DemoAPI.Models;

namespace DemoAPI.Controllers
{

    public class ShipController : ApiController
    {

        /* Get ships from ship table joined  */
        public IEnumerable<ship> Get()
        {
            using (demoapiEntities entities = new demoapiEntities())
            {
                //disables proxy classes creation for entity instances
                entities.Configuration.ProxyCreationEnabled = false;

                IEnumerable<ship> listOfShips = entities.ships.Include(s =>s.owners).ToList();
                return listOfShips;
            }
                
        }


        /*Insert ship 
         * 
         *
         */
        public void Post(ship shipToAdd, [FromUri] int id)
        {
            using (demoapiEntities entities = new demoapiEntities())
            {
                // insert into ship table
                entities.ships.Add(shipToAdd);

                entities.SaveChanges();
                //fetch recently added ship
               
                ship addedShip = entities.ships.Single(s => s.ship_id == shipToAdd.ship_id);
               
                owner findOwner = entities.owners.Single(o => o.owner_id == id);

                addedShip.owners.Add(findOwner);
                entities.SaveChanges();
            }
        } 


        /* Delete ship
         * we have to delete also the relation of ship*/
       public void Delete(int id)
        {
            using (demoapiEntities entities = new demoapiEntities())
            {
                var shipToDelete = entities.ships.FirstOrDefault(s => s.ship_id == id);
                List<owner> shipOwners = entities.owners.Include(o => o.ships).ToList();
                System.Diagnostics.Debug.WriteLine(shipToDelete);

                entities.ships.Remove(shipToDelete);
                entities.SaveChanges();

            }
        }
    }
}
