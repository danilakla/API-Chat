namespace API_Chat.DTO
{
    public class CreateGropDto
    {
        public string SearchString { get; set; }
        public string Desription { get; set; }
        public string FromWhom { get; set; }=string.Empty;

        public string RoomName { get; set; }
    }
}
