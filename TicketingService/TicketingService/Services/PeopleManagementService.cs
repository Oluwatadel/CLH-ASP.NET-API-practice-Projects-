namespace TicketingService.Services
{
    public class PeopleManagementService
    {

        public string? GetStaffCentre(string staffId)
        {
            return staffId switch
            {
                "bob" => "Abeokuta",
                "john" => "Lagos",
                _ => null
            };
        }
    }
}
