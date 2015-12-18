using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities;
using Services;

namespace Api.Controllers
{
    public class KeyValueController : ApiController
    {
        private KeyValueServices _service;

        public KeyValueController()
        {
            _service = new KeyValueServices();
        }
        // GET: api/KeyValue
        public HttpResponseMessage Get()
        {
            var result = _service.GetAllKeyValueData().ToList();
            if (!result.Any())
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Key Value Data is found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/KeyValue/5
        public HttpResponseMessage Get(long id)
        {
            var result = _service.GetKeyValueDataById(id);
            if (result == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    string.Format("No Key Value data of id {0} has been found. ", id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/KeyValue
        [HttpPost]
        public HttpResponseMessage Post([FromBody]KeyValueEntity entity)
        {
            if (entity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cannot create empty key value object.");
            }
            _service.CreateKeyValueData(entity);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT: api/KeyValue/5
        public HttpResponseMessage Put(long id, [FromBody]KeyValueEntity entity)
        {
            if (entity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cannot create empty key value object.");
            }
            else if (id <= 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unexpected key value Id.");
            }
            var success = _service.UpdateKeyValueData(id, entity);

            if (success)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unexpected error.");
            }
        }

        // DELETE: api/KeyValue/5
        public HttpResponseMessage Delete(long id)
        {
            if (id <= 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unexpected key value Id.");
            }

            var success = _service.DeleteKeyValueData(id);
            if (success)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unexpected error.");
            }
        }
    }
}
