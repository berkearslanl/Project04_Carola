namespace Carola.DtoLayer.Dtos.LocationDtos
{
    public class UpdateLocationDto
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string AuthorizedPerson { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string ImageUrl { get; set; }
    }
}
