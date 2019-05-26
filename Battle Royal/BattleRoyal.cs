using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battle_Royal.Commands;
using Battle_Royal.Handlers;
using Smod2;
using Smod2.Attributes;

namespace Battle_Royal
{
    // Plugin details,
    // don't change or i will sue you (jk)
    [PluginDetails(
        author = "Kade",
        name = "Battle Royal",
        description = "A SCP:SL Battle Royal gamemode.",
        id = "kade.br",
        version = "1.0",
        SmodMajor = 3,
        SmodMinor = 4,
        SmodRevision = 0
        )]

    public class BattleRoyal : Plugin
    {
        public override void OnDisable()
        {
            // Send a message.
            Info("Disabled!");
        }

        public override void OnEnable()
        {
            // Ascii art and shit
            Info("-----------------------------------------------------------------------------------------------\nKades Ultimate Meme Gamemode\n _______    ______  ________  ________  __        ________        _______    ______  __      __   ______   __       "
 + "|       \\  /      \\|        \\|        \\|  \\      |        \\      |       \\  /      \\|  \\    /  \\ /      \\ |  \\      "
 + "| $$$$$$$\\|  $$$$$$\\\\$$$$$$$$ \\$$$$$$$$| $$      | $$$$$$$$      | $$$$$$$\\|  $$$$$$\\\\$$\\  /  $$|  $$$$$$\\| $$      "
 + "| $$__ / $$| $$__ | $$  | $$      | $$   | $$      | $$__ | $$__ | $$| $$  | $$ \\$$\\/  $$ | $$__ | $$| $$      "
 + "| $$    $$| $$    $$  | $$      | $$   | $$      | $$  \\         | $$    $$| $$  | $$  \\$$  $$  | $$    $$| $$      "
 + "| $$$$$$$\\| $$$$$$$$  | $$      | $$   | $$      | $$$$$         | $$$$$$$\\| $$  | $$   \\$$$$   | $$$$$$$$| $$      "
 + "| $$__/ $$| $$  | $$  | $$      | $$   | $$_____ | $$_____       | $$  | $$| $$__/ $$   | $$    | $$  | $$| $$_____ "
 + "| $$    $$| $$  | $$  | $$      | $$   | $$     \\| $$     \\      | $$  | $$ \\$$    $$   | $$    | $$  | $$| $$     \\"
  + "\\$$$$$$$  \\$$   \\$$   \\$$       \\$$    \\$$$$$$$$ \\$$$$$$$$       \\$$   \\$$  \\$$$$$$     \\$$     \\$$   \\$$ \\$$$$$$$$\n;)\n-----------------------------------------------------------------------------------------------");
        }

        public override void Register()
        {
            // The real meat, as since its not good to register shit on Register(),
            // it will be done in the GameHandler, on the waiting for players event.
            // By register I mean by like variables, timers, threads and stuff. Not commands or handlers.

            // GameHandler
            AddEventHandlers(new GameHandler(this));

            AddCommand("startbr", new StartBattleRoyal(this));

        }
    }
}
