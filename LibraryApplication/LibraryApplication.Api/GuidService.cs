namespace LibraryApplication.Api
{
    public class GuidService : IGuidService
    {
        public Guid Guid { get; }

        public GuidService()
        {
            Guid = Guid.NewGuid();
        }
    }
}
