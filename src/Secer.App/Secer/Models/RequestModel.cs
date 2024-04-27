namespace Secer.Models;

public class RequestModel
{
    public string IpAddress { get; set; } = default!;

    public string MethodName { get; set; } = default!;

    public DateTime RequestTime { get; set; } = DateTime.Now;
}
