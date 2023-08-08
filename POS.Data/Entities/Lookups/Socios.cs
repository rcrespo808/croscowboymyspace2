using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Data;

[Table("customers")]
public class Socios
{
    [Key]
    public Guid Id { get; set; }
    public string CustomerName { get; set; }	
    public string ContactPerson { get; set; }	
    public string Email { get; set; }	
    public string Fax { get; set; }	
    public string MobileNo { get; set; }	
    public string PhoneNo { get; set; }	
    public string Website { get; set; }	
    public string Description { get; set; }	
    public string Url { get; set; }	
    public string CustomerProfile { get; set; }	
    public string Address { get; set; }	
    public string CountryName { get; set; }	
    public string CityName { get; set; }
    public Guid CreatedBy  { get; set; }	
    public Guid ModifiedBy   { get; set; }	
    
        
}