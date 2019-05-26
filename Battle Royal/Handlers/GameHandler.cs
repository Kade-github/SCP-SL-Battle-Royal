using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battle_Royal.Handlers
{
    class GameHandler : IEventHandlerWaitingForPlayers, IEventHandlerCheckRoundEnd, IEventHandlerRoundStart, IEventHandlerPlayerDie, IEventHandlerCheckEscape, IEventHandlerPlayerJoin
    {
        // Player cache thingy

        int playerCache = 0;

        // Sets if its on waiting for players.

        public static bool waiting = false;

        // End Condition: If the game should end. (FALSE = yes end, TRUE = dont end)

        private bool endCondition = false;

        // Player List: Every player in the battle royal.

        List<Player> Players = new List<Player>();

        // On and off boolean (FALSE = Off, TRUE = On)

        public static bool st = false;

        // Copied code from like every plugin ever lmao.

        private readonly BattleRoyal plugin;

        public GameHandler(BattleRoyal plugin) => this.plugin = plugin;


        #region Events
        // Register threads, other shit aswell.

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            // Clear that data, cuz it useless.
            Players.Clear();
            st = false;
            endCondition = false;
            plugin.Info("Cleared data for new round...");
            waiting = true;
        }


        // Round end checker

        public void OnCheckRoundEnd(CheckRoundEndEvent ev)
        {
            if (st)
                if (ev.Status != ROUND_END_STATUS.ON_GOING)
                    if (endCondition)
                        ev.Status = ROUND_END_STATUS.ON_GOING;
        }

        // Start the game

        public void OnRoundStart(RoundStartEvent ev)
        {
            if (st)
            {
                endCondition = true;
                foreach (Player p in plugin.Server.GetPlayers())
                {
                    Players.Add(p);
                    p.ChangeRole(Role.CLASSD, true, true, false, true);
                }
                plugin.Server.Map.Broadcast(3, "LET THE BATTLE ROYAL START!",true);
                StartCount();
            }
        }

        // Death event
        public void OnPlayerDie(PlayerDeathEvent ev)
        {
            if (st)
            {
                ev.Player.OverwatchMode = true;
                ev.Player.PersonalBroadcast(3, "You are now out of the game!", true);
            }
        }

        // Deny escapes

        public void OnCheckEscape(PlayerCheckEscapeEvent ev)
        {
            if (st)
                ev.AllowEscape = false;
        }

        // Anti cheat

        public void OnPlayerJoin(PlayerJoinEvent ev)
        {
            if (st)
            {
                ev.Player.OverwatchMode = true;
                ev.Player.PersonalBroadcast(3, "You joined durring the battle royal mode, so you cannot respawn!", true);
            }
        }


        #endregion

        #region Methods

        // Main thread, mostly checking players. And broadcasting how many are left.

        public void StartCount()
        {
            new Thread(() => {
                while (st)
                {
                    // End Condition:
                    switch (GetPlayers())
                    {
                        case 1:
                            string pName = getPlayer(Role.CLASSD).Name;
                            plugin.Server.Map.Broadcast(10, "<color=lime>" + pName + " has won the game!</color>", true);
                            endCondition = true;
                            st = false;
                            break;
                        default:
                            // To try not to spam the console, and the game. We will only do this if the player counter changed.
                            if (playerCache != GetPlayers())
                            {
                                playerCache = GetPlayers();
                                plugin.Server.Map.Broadcast(1, "<color=lime>" + GetPlayers() + " left.</color>", true);
                                Thread.Sleep(1000);
                            }
                            else
                                Thread.Sleep(1000);
                            break;
                    }
                }
            }).Start();
        }

        // Get player
        public Player getPlayer(Role r)
        {
            Player player = plugin.Server.GetPlayers(r).FirstOrDefault();
            return player;
        }

        // Get the players in the battle royal

        public int GetPlayers()
        {
            return Players.Count;
        }
        #endregion
    }
}
