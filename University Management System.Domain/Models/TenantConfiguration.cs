namespace University_Management_System.Domain.Models;

public class TenantConfiguration
{
    public string TenantId { get; set; }
    public string ConnectionString { get; set; }
    public string Schema { get; set; }
}