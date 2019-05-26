using Battle_Royal.Handlers;
using Smod2.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Royal.Commands
{
    class StartBattleRoyal : ICommandHandler
    {
        // Copied code from like every plugin ever lmao.

        private readonly BattleRoyal plugin;

        public StartBattleRoyal(BattleRoyal plugin) => this.plugin = plugin;


        // Command shit idk

        public string GetCommandDescription()
        {
            return "Start the battle royal! (Only use on waiting for players)";
        }

        public string GetUsage()
        {
            return "startbr";
        }

        // The big old MEAT

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (GameHandler.waiting)
            {
                // Starts shit
                GameHandler.st = true;
                return new string[] { "Battle Royal Round Enabled!" };
            }
            else
                return new string[] { "Sorry the round has already started and so,", "you cannot do this..." };
        }
    }

}
