using System;
using System.ComponentModel.DataAnnotations;

namespace POS.Data;

public class Expositoriesview
{
    [Key]
    public int idEvento { get; set; }
    
    public Guid evento { get; set; }
    
    public string CustomerName { get; set; }
    
    public string ContactPerson { get; set; }
    
    public string Email { get; set; }
    
    public string MobileNo { get; set; }
    
    public string PhoneNo { get; set; }
    
    public string Website { get; set; }
    
    public string Description { get; set; }
    
    public string Address { get; set; }
    
    public string CountryName { get; set; }
    
    public string CityName { get; set; }
    
    public Guid idCus { get; set; }
}