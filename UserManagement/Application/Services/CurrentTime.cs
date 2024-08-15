using Application.Interfaces;
using Application.Interfaces.IServices;

namespace Application.Services
{
    public class CurrentTime : ICurrentTime
    {
        public DateTime GetCurrentTime() => DateTime.UtcNow;
    }
}
