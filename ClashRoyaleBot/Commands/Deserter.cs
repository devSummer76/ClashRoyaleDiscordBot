using Discord;
using Discord.Commands;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyaleBot.Commands {
    class DeserterCmd : ModuleBase {

        private ApiService api;

        public DeserterCmd(ApiService apiService) {
            api = apiService;
        }

 

        [Command("deserter"), Summary("Returns every warday Derserter!")]
        public async Task Deserter()
        {
            string reply = "Hello deserter!";
            
            await ReplyAsync(reply);
        }

    }
}
