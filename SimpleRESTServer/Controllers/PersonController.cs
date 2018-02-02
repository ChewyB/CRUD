using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleRESTServer.Models;
using System.Collections;

namespace SimpleRESTServer.Controllers
{
    public class PersonController : ApiController
    {
        // GET: api/Person
        public ArrayList Get()
        {
            return new PersonPersistence().getPersons();
        }

        // GET: api/Person/5
        public Person Get(int id)
        {
            PersonPersistence pp = new PersonPersistence();
            Person p = pp.getPerson(id);

            return p;
        }

        // POST: api/Person
        public HttpResponseMessage Post([FromBody]Person value)
        {
            PersonPersistence pp = new PersonPersistence();
            long id;
            id = pp.savePerson(value);
            value.ID = id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("Person/{0}", id));
            return response;
        }

        // PUT: api/Person/5
        public HttpResponseMessage Put(long id, [FromBody]Person p)
        {
            PersonPersistence pp = new PersonPersistence();
            bool recordExisted = false;
            recordExisted = pp.updatePerson(id, p);

            HttpResponseMessage response;

            if (recordExisted)
            {
                //Send a response code
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        // DELETE: api/Person/5
        public HttpResponseMessage Delete(long id)
        {
            PersonPersistence pp = new PersonPersistence();
            bool recordExisted = false;
            recordExisted = pp.deletePerson(id);

            HttpResponseMessage response;

            if (recordExisted)
            {
                //Send a response code
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
    }
}
