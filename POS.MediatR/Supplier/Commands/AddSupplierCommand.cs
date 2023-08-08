using POS.Data.Dto;
using POS.Helper;
using MediatR;
using System.Collections.Generic;
using System;
using POS.Helper.Enum;

namespace POS.MediatR.CommandAndQuery
{
    public class AddSupplierCommand : IRequest<ServiceResponse<SupplierDto>>
    {
        public string CodigoSN { get; set; }
        public string RazonSocial { get; set; }
        public string SupplierName { get; set; } // Nombre Comercial
        public string Description { get; set; } //Descripcion
        public string Area { get; set; }
        public string MainActivity { get; set; }
        public string SecondaryActivity { get; set; }
        public string Nit { get; set; }
        public string Direccion { get; set; }
        public string TipoSociedad { get; set; }
        public string PhoneNo { get; set; }
        public string Telefono2 { get; set; }
        public string Email { get; set; } //EmailAddress
        public int Fax { get; set; }
        public string Website { get; set; } //Website
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public string Departamento { get; set; }
        public Sector Sector { get; set; }
        public string Objeto { get; set; }
        public DateTime FechaAniversario { get; set; }
        public DateTime EstadoConstitucion { get; set; }
        public DateTime FechaTestimonio { get; set; }
        public string EstadoPoder { get; set; }
        public int NroPoder { get; set; }
        public int NroMatricula { get; set; }
        public DateTime FechaMatricula { get; set; }
        public int NroPadron { get; set; }
        public DateTime EmisionPadron { get; set; }
        public DateTime BalanceVisado { get; set; }
        public string LogoUrlData { get; set; }
        public string BannerUrlData { get; set; }
        public bool IsVerified { get; set; }
        public bool EsAfiliado { get; set; }
        public bool ActiveTopInterest { get; set; } = false;
        public bool ActiveTopPublicity { get; set; } = false;
        public string Coordinates { get; set; }
        public string ContactPerson { get; set; } //ContactPerson
        public List<string> ContactPersonList { get; set; } //ContactPerson
        public Guid IdRepresentanteLegal { get; set; }
    }
}
