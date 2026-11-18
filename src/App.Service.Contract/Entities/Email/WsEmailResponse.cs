namespace App.Service.Contract.Entities.Email
{
    public class WsEmailResponse:WsEntity
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }
    }
}