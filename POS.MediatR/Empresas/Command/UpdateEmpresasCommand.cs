using MediatR;
using POS.Data.Dto.Empresas;
using POS.Helper;
using POS.Helper.Enum;
using System;

namespace POS.MediatR.Empresas.Commands
{
    public class UpdateEmpresasCommand : IRequest<ServiceResponse<EmpresasDto>>
    {
        public Guid Id { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string UrlLogoData { get; set; }
        public bool IsLogoChanged { get; set; }
        public string Area { get; set; }
        public string ActividadPrincipal { get; set; }
        public string ActividadSecundaria { get; set; }
        public string Nit { get; set; }
        public string Direccion { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Correo { get; set; }
        public int Fax { get; set; }
        public string UrlWeb { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public string Departamento { get; set; }
        public Sector Sector { get; set; }
        public string Objeto { get; set; }
        public DateTime? FechaAniversario { get; set; }
        public DateTime? EstadoConstitucion { get; set; }
        public DateTime? FechaTestimonio { get; set; }
        public string EstadoPoder { get; set; }
        public int NroPoder { get; set; }
        public int NroMatricula { get; set; }
        public DateTime? FechaMatricula { get; set; }
        public int NroPadron { get; set; }
        public DateTime? EmisionPadron { get; set; }
        public DateTime? BalanceVisado { get; set; }
        public bool EsAfiliado { get; set; }
    }
}
