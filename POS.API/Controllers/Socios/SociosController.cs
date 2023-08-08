using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Domain;
using Microsoft.EntityFrameworkCore;
using POS.API.Helpers;
using POS.API.Helpers.Utils;


namespace POS.API.Controllers.Socios;

[Route("api/socios")]
[ApiController]
public class SociosController : BaseController
{
    public POSDbContext _context;

    public SociosController(POSDbContext context)
    {
        this._context = context;
    }
    
    [HttpGet]
    [ClaimCheck("SETT_MANAGE_CITY")]
    public JsonResult actionIndex(int page = 1, string search = "")
    {
        try
        {
            int perPage = 20;
            page--;
            Utils.Meta meta = new Utils.Meta();
            Utils.Links links = new Utils.Links();
            Utils.Respuesta respuesta = new Utils.Respuesta();
            string filter = string.Empty;
            if (search != "")
            {
                filter = $" WHERE CustomerName LIKE '%{search}%' ";
            }
            Data.Socios[] query = _context.Socios.FromSqlRaw($"SELECT * FROM customers {filter}").ToArray();
            if (query.Count() == 0)
            {
                respuesta._meta = meta;
                respuesta._links = links;
                respuesta.Items = null;
                return new JsonResult(respuesta);
            }
            int totalPages = query.Count();
            int pageCount = 0;
            double x = (double) (totalPages / (decimal) perPage);
            if ((x % 10) > 0)
            {
                pageCount = (int) (Math.Truncate(x) + 1);
            }
            else
            {
                pageCount = (int) Math.Truncate(x);
            }
            if (page >= pageCount)
            {
                page = pageCount - 1;
            }
            meta.totalCount = totalPages;
            meta.perPage = perPage;
            meta.pageCount = pageCount;
            meta.currentPage = page;
            List<Utils.Pages> paginador = new List<Utils.Pages>();
            int p = 0;
            for (int i = 0; i < meta.pageCount; i++)
            {
                paginador.Add(new Utils.Pages {inicio = p, final = perPage});
                p = p + perPage;
            }
            links.first = $"?page=1&search={search}";
            links.last = $"?page={meta.pageCount}&search={search}";
            links.self = $"?page={meta.currentPage + 1}&search={search}";
            links.prev = $"?page={((meta.currentPage <= 0) ? 1 : meta.currentPage)}&search={search}";
            int inicio = paginador[meta.currentPage].inicio;
            int final = paginador[meta.currentPage].final;
            respuesta._meta = meta;
            respuesta._links = links;
            respuesta.Items = query.Skip(inicio).Take(final);
            return new JsonResult(respuesta);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("{id:Guid}")]
    [ClaimCheck("SETT_MANAGE_CITY")]
    public JsonResult actionView(Guid id)
    {
        Data.Socios query = _context.Socios.First(data => data.Id == id);
        return new JsonResult(query);
    }

    [HttpPost]
    [ClaimCheck("SETT_MANAGE_CITY")]
    public JsonResult actionCreate(Data.Socios model)
    {
        string userId =  HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        model.CreatedBy = new Guid(userId);
        model.ModifiedBy = new Guid(userId);
        this._context.Add(model);
        this._context.SaveChanges();
        return new JsonResult(model);
    }   
    
    [HttpDelete("{id:Guid}")]
    [ClaimCheck("SETT_MANAGE_CITY")]
    public JsonResult actionDelete(Guid id)
    {
        try
        {
            var model = new Data.Socios { Id = id };
            _context.Entry(model).State = EntityState.Deleted;
            _context.SaveChanges();
            return new JsonResult(1);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPut("{id:Guid}")]
    [ClaimCheck("SETT_MANAGE_CITY")]
    public JsonResult actionUpdate(Data.Socios param, Guid id)
    {
        string userId =  HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        Data.Socios model =  this._context.Socios.FirstOrDefault( x => x.Id == id);
        model.CustomerName = param.CustomerName;
        model.ContactPerson = param.ContactPerson;
        model.Email = param.Email;
        model.Fax = param.Fax;
        model.MobileNo = param.MobileNo;
        model.PhoneNo = param.PhoneNo;
        model.Website = param.Website;
        model.Description = param.Description;
        model.Url = param.Url;
        model.CustomerProfile = param.CustomerProfile;
        model.Address = param.Address;
        model.CountryName = param.CountryName;
        model.CityName = param.CityName;
        model.CreatedBy = new Guid(userId);
        this._context.Entry(model).State = EntityState.Modified;
        this._context.SaveChanges();
        return new JsonResult(model);
    }
    
}
