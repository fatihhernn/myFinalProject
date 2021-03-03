namespace Core.Entities.Concrete
{
    public class UserOperationClaim : IEntity //kullanıcının operationları
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
