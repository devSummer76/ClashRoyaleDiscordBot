using RoyaleWrapper;
using System;
using System.Threading.Tasks;

namespace Service {
    public class ApiService {

       private Client _clashRoyaleClient;

        public ApiService(string clashRoyale_ApiKey) {
            _clashRoyaleClient = new Client(clashRoyale_ApiKey);
        }


        public async Task<RoyaleWrapper.Models.Player> getPlayer(string playerTag) {
            return await _clashRoyaleClient.GetPlayerAsync(playerTag);
        }


    }
}
