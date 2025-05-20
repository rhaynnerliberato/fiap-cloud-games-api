using System.ComponentModel.DataAnnotations;

namespace fiap_cloud_games_api.Models.Requests
{
    public class AplicarPromocaoRequest
    {
        [Range(1, 100)]
        public decimal Percentual { get; set; }
    }

}
