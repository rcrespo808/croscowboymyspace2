using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using POS.Domain;
using Microsoft.EntityFrameworkCore;
using POS.API.Controllers;
using POS.API.Helpers;
using POS.API.Helpers.Utils;
using POS.Data;

namespace POS.API.Controllers.Usuarios;

[Route("usuarios")]
[ApiController]
public class UsuariosController : BaseController
{
    public POSDbContext _context;

    public UsuariosController(POSDbContext context)
    {
        this._context = context;
    }

    [HttpGet]
    [ClaimCheck("SETT_MANAGE_CITY")]
    public JsonResult actionIndex(int page = 1, string search = "")
    {
        try
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int perPage = 20;
            page--;
            Utils.Meta meta = new Utils.Meta();
            Utils.Links links = new Utils.Links();
            Utils.Respuesta respuesta = new Utils.Respuesta();
            string filter = string.Empty;
            if (search != "")
            {
                filter = $" WHERE UserName LIKE '%{search}%' ";
            }
            Data.Usuarios[] query = _context.Usuarios.FromSqlRaw($"SELECT * FROM users {filter}").ToArray();
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
        Data.Usuarios query = _context.Usuarios.First(data => data.Id == id);
        return new JsonResult(query);
    }

    [HttpPost]
    [ClaimCheck("SETT_MANAGE_CITY")]
    public JsonResult actionCreate(Data.Usuarios model)
    {
        string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
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
            var model = new Data.Usuarios { Id = id };
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
    public JsonResult actionUpdate(Data.Usuarios param, Guid id)
    {
        Data.Usuarios model =  this._context.Usuarios.FirstOrDefault( x => x.Id == id);
        model.FirstName = param.FirstName;
        model.LastName = param.LastName;
        model.UserName = param.UserName;
        model.Email = param.Email;
        model.PhoneNumber = param.PhoneNumber;
        model.Address = param.Address;
        model.ProfilePhoto = param.ProfilePhoto;
        this._context.Entry(model).State = EntityState.Modified;
        this._context.SaveChanges();
        return new JsonResult(model);
    }
}
